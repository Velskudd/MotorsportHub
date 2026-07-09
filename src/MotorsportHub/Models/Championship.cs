namespace MotorsportHub.Models;

public enum ChampionshipCategory
{
    Circuit,
    Rallye,
    Montagne,
    ToutTerrain,
    Karting,
    Drift,
    Camion,
    Historique
}

public static class ChampionshipCategoryExtensions
{
    public static string Libelle(this ChampionshipCategory categorie) => categorie switch
    {
        ChampionshipCategory.Circuit => "Circuit",
        ChampionshipCategory.Rallye => "Rallye",
        ChampionshipCategory.Montagne => "Montagne",
        ChampionshipCategory.ToutTerrain => "Tout-terrain",
        ChampionshipCategory.Karting => "Karting",
        ChampionshipCategory.Drift => "Drift",
        ChampionshipCategory.Camion => "Camion",
        ChampionshipCategory.Historique => "Historique",
        _ => categorie.ToString()
    };

    public static string Emoji(this ChampionshipCategory categorie) => categorie switch
    {
        ChampionshipCategory.Circuit => "🏎️",
        ChampionshipCategory.Rallye => "🌲",
        ChampionshipCategory.Montagne => "⛰️",
        ChampionshipCategory.ToutTerrain => "🏜️",
        ChampionshipCategory.Karting => "🏁",
        ChampionshipCategory.Drift => "💨",
        ChampionshipCategory.Camion => "🚚",
        ChampionshipCategory.Historique => "🕰️",
        _ => "🏆"
    };
}

public record Championship(
    string Slug,
    string Nom,
    ChampionshipCategory Categorie,
    string Organisateur,
    int? AnneeCreation,
    string Description,
    string? SiteWeb);
