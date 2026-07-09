using MotorsportHub.Application.Interfaces;
using MotorsportHub.Domain.Entites;
using MotorsportHub.Infrastructure.Donnees;

namespace MotorsportHub.Infrastructure.Depots;

/// <summary>
/// Implémentation en mémoire, à remplacer par un repository EF Core
/// lorsque la base de données sera branchée.
/// </summary>
public class InMemoryChampionnatRepository : IChampionnatRepository
{
    public Task<IReadOnlyList<Championnat>> ObtenirTousAsync(CancellationToken ct = default) =>
        Task.FromResult(DonneesInitiales.Championnats);

    public Task<Championnat?> ObtenirParSlugAsync(string slug, CancellationToken ct = default) =>
        Task.FromResult(DonneesInitiales.Championnats.FirstOrDefault(
            c => string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase)));

    public Task<IReadOnlyList<Championnat>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default)
    {
        IEnumerable<Championnat> resultat = DonneesInitiales.Championnats;

        if (!string.IsNullOrWhiteSpace(disciplineSlug))
        {
            resultat = resultat.Where(c => c.Discipline.Slug == disciplineSlug);
        }

        if (!string.IsNullOrWhiteSpace(texte))
        {
            resultat = resultat.Where(c =>
                c.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                c.Organisateurs.Any(o => o.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase)));
        }

        IReadOnlyList<Championnat> liste = resultat
            .OrderBy(c => c.Discipline.Ordre)
            .ThenBy(c => c.Nom)
            .ToList();

        return Task.FromResult(liste);
    }
}
