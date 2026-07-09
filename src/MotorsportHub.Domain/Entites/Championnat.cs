namespace MotorsportHub.Domain.Entites;

public class Championnat
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? AnneeCreation { get; set; }
    public string? SiteWeb { get; set; }
    public StatutChampionnat Statut { get; set; } = StatutChampionnat.Actif;

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; } = null!;

    public ICollection<Organisateur> Organisateurs { get; set; } = new List<Organisateur>();
}
