using MotorsportHub.Application.Interfaces;
using MotorsportHub.Domain.Entites;
using MotorsportHub.Infrastructure.Donnees;

namespace MotorsportHub.Infrastructure.Depots;

public class InMemoryDisciplineRepository : IDisciplineRepository
{
    public Task<IReadOnlyList<Discipline>> ObtenirToutesAsync(CancellationToken ct = default)
    {
        IReadOnlyList<Discipline> disciplines = DonneesInitiales.Disciplines
            .OrderBy(d => d.Ordre)
            .ToList();

        return Task.FromResult(disciplines);
    }
}
