using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Application.Interfaces;

public interface IChampionnatRepository
{
    Task<IReadOnlyList<Championnat>> ObtenirTousAsync(CancellationToken ct = default);

    Task<Championnat?> ObtenirParSlugAsync(string slug, CancellationToken ct = default);

    /// <summary>
    /// Recherche par texte libre (nom, description, organisateur) et/ou par discipline.
    /// Les deux critères sont optionnels et cumulables.
    /// </summary>
    Task<IReadOnlyList<Championnat>> RechercherAsync(
        string? texte,
        string? disciplineSlug,
        CancellationToken ct = default);
}
