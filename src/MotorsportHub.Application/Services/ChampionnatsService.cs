using MotorsportHub.Application.Interfaces;
using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Application.Services;

/// <summary>
/// Cas d'usage du catalogue des championnats. Consommé aujourd'hui par le front
/// Blazor, demain également par l'API HTTP.
/// </summary>
public class ChampionnatsService(
    IChampionnatRepository championnats,
    IDisciplineRepository disciplines)
{
    public Task<IReadOnlyList<Championnat>> ObtenirTousAsync(CancellationToken ct = default) =>
        championnats.ObtenirTousAsync(ct);

    public Task<Championnat?> ObtenirParSlugAsync(string slug, CancellationToken ct = default) =>
        championnats.ObtenirParSlugAsync(slug, ct);

    public Task<IReadOnlyList<Championnat>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default) =>
        championnats.RechercherAsync(texte, disciplineSlug, ct);

    public Task<IReadOnlyList<Discipline>> ObtenirDisciplinesAsync(CancellationToken ct = default) =>
        disciplines.ObtenirToutesAsync(ct);

    public async Task<IReadOnlyList<DisciplineAvecNombre>> CompterParDisciplineAsync(
        CancellationToken ct = default)
    {
        var tous = await championnats.ObtenirTousAsync(ct);

        return tous
            .GroupBy(c => c.Discipline)
            .OrderBy(g => g.Key.Ordre)
            .Select(g => new DisciplineAvecNombre(g.Key, g.Count()))
            .ToList();
    }
}

public record DisciplineAvecNombre(Discipline Discipline, int Nombre);
