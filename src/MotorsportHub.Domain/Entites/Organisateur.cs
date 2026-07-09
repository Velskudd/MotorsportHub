namespace MotorsportHub.Domain.Entites;

public class Organisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? SiteWeb { get; set; }

    public ICollection<Championnat> Championnats { get; set; } = new List<Championnat>();
}
