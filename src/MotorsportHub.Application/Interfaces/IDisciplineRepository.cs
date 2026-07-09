using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Application.Interfaces;

public interface IDisciplineRepository
{
    Task<IReadOnlyList<Discipline>> ObtenirToutesAsync(CancellationToken ct = default);
}
