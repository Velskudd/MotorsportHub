namespace MotorsportHub.Domain.Entites;

public class Organisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? SiteWeb { get; set; }

    public ICollection<Plateau> Plateaux { get; set; } = new List<Plateau>();
}
