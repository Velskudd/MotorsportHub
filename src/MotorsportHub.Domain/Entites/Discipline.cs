namespace MotorsportHub.Domain.Entites;

public class Discipline
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;

    /// <summary>Ordre d'affichage dans les listes et filtres.</summary>
    public int Ordre { get; set; }

    public ICollection<Plateau> Plateaux { get; set; } = new List<Plateau>();
}
