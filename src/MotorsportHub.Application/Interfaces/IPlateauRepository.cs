using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Application.Interfaces;

public interface IPlateauRepository
{
    Task<IReadOnlyList<Plateau>> ObtenirTousAsync(CancellationToken ct = default);

    Task<Plateau?> ObtenirParSlugAsync(string slug, CancellationToken ct = default);

    /// <summary>
    /// Recherche par texte libre (nom, description, organisateur) et/ou par discipline.
    /// Les deux critères sont optionnels et cumulables.
    /// </summary>
    Task<IReadOnlyList<Plateau>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default);
}
