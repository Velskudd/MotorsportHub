using MotorsportHub.Application.Interfaces;
using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Application.Services;

/// <summary>
/// Cas d'usage du catalogue des plateaux. Consommé aujourd'hui par le front
/// Blazor, demain également par l'API HTTP.
/// </summary>
public class PlateauxService(
    IPlateauRepository plateaux,
    IDisciplineRepository disciplines)
{
    public Task<IReadOnlyList<Plateau>> ObtenirTousAsync(CancellationToken ct = default) =>
        plateaux.ObtenirTousAsync(ct);

    public Task<Plateau?> ObtenirParSlugAsync(string slug, CancellationToken ct = default) =>
        plateaux.ObtenirParSlugAsync(slug, ct);

    public Task<IReadOnlyList<Plateau>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default) =>
        plateaux.RechercherAsync(texte, disciplineSlug, ct);

    public Task<IReadOnlyList<Discipline>> ObtenirDisciplinesAsync(CancellationToken ct = default) =>
        disciplines.ObtenirToutesAsync(ct);

    public async Task<IReadOnlyList<DisciplineAvecNombre>> CompterParDisciplineAsync(
        CancellationToken ct = default)
    {
        var tous = await plateaux.ObtenirTousAsync(ct);

        return tous
            .GroupBy(c => c.Discipline)
            .OrderBy(g => g.Key.Ordre)
            .Select(g => new DisciplineAvecNombre(g.Key, g.Count()))
            .ToList();
    }
}

public record DisciplineAvecNombre(Discipline Discipline, int Nombre);
