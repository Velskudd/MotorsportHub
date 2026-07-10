using System.Net.Http.Json;
using MotorsportHub.Application.Interfaces;
using MotorsportHub.Domain.Entites;
using MotorsportHub.Infrastructure.Donnees;

namespace MotorsportHub.Infrastructure.Supabase;

/// <summary>
/// Accès au catalogue via l'API REST générée par Supabase (PostgREST).
/// Les données sont chargées une seule fois puis servies depuis la mémoire.
/// En cas d'échec réseau (projet Supabase en pause, hors-ligne...), repli
/// silencieux sur les données embarquées : le site reste utilisable.
/// </summary>
public class SupabaseCatalogue(HttpClient http) : IPlateauRepository, IDisciplineRepository
{
    private Task<(IReadOnlyList<Discipline> Disciplines, IReadOnlyList<Plateau> Plateaux)>? _chargement;

    private Task<(IReadOnlyList<Discipline> Disciplines, IReadOnlyList<Plateau> Plateaux)> Donnees =>
        _chargement ??= ChargerAsync();

    public async Task<IReadOnlyList<Plateau>> ObtenirTousAsync(CancellationToken ct = default) =>
        (await Donnees).Plateaux;

    public async Task<Plateau?> ObtenirParSlugAsync(string slug, CancellationToken ct = default) =>
        (await Donnees).Plateaux.FirstOrDefault(
            p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public async Task<IReadOnlyList<Plateau>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default)
    {
        IEnumerable<Plateau> resultat = (await Donnees).Plateaux;

        if (!string.IsNullOrWhiteSpace(disciplineSlug))
        {
            resultat = resultat.Where(p => p.Discipline.Slug == disciplineSlug);
        }

        if (!string.IsNullOrWhiteSpace(texte))
        {
            resultat = resultat.Where(p =>
                p.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                p.Organisateur.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase));
        }

        return resultat
            .OrderBy(p => p.Discipline.Ordre)
            .ThenBy(p => p.Nom)
            .ToList();
    }

    public async Task<IReadOnlyList<Discipline>> ObtenirToutesAsync(CancellationToken ct = default) =>
        (await Donnees).Disciplines;

    private async Task<(IReadOnlyList<Discipline>, IReadOnlyList<Plateau>)> ChargerAsync()
    {
        try
        {
            var disciplinesDto = await http.GetFromJsonAsync<List<DisciplineDto>>(
                "disciplines?select=*&order=ordre") ?? [];

            var plateauxDto = await http.GetFromJsonAsync<List<PlateauDto>>(
                "plateaux?select=*,discipline:disciplines(*),organisateur:organisateurs(*)") ?? [];

            // Partager les instances Discipline/Organisateur entre plateaux :
            // le service groupe par référence (GroupBy sur l'entité).
            var disciplines = disciplinesDto
                .Select(d => d.VersEntite())
                .ToDictionary(d => d.Id);
            var organisateurs = new Dictionary<int, Organisateur>();

            var plateaux = new List<Plateau>();
            foreach (var dto in plateauxDto)
            {
                if (!disciplines.TryGetValue(dto.Discipline.Id, out var discipline))
                {
                    discipline = dto.Discipline.VersEntite();
                    disciplines[discipline.Id] = discipline;
                }

                if (!organisateurs.TryGetValue(dto.Organisateur.Id, out var organisateur))
                {
                    organisateur = dto.Organisateur.VersEntite();
                    organisateurs[organisateur.Id] = organisateur;
                }

                var plateau = new Plateau
                {
                    Id = dto.Id,
                    Slug = dto.Slug,
                    Nom = dto.Nom,
                    Description = dto.Description,
                    AnneeCreation = dto.AnneeCreation,
                    SiteWeb = dto.SiteWeb,
                    Statut = string.Equals(dto.Statut, "disparu", StringComparison.OrdinalIgnoreCase)
                        ? StatutPlateau.Disparu
                        : StatutPlateau.Actif,
                    DisciplineId = discipline.Id,
                    Discipline = discipline,
                    Organisateur = organisateur
                };

                discipline.Plateaux.Add(plateau);
                organisateur.Plateaux.Add(plateau);
                plateaux.Add(plateau);
            }

            return (disciplines.Values.OrderBy(d => d.Ordre).ToList(), plateaux);
        }
        catch
        {
            // Repli sur les données embarquées. Le résultat est mémorisé pour
            // la session en cours : recharger la page retentera la base.
            return (DonneesInitiales.Disciplines, DonneesInitiales.Plateaux);
        }
    }
}
