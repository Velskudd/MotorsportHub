using MotorsportHub.Models;

namespace MotorsportHub.Services;

public class ChampionshipService
{
    private static readonly IReadOnlyList<Championship> Championnats = new List<Championship>
    {
        // ── Circuit ─────────────────────────────────────────────────────────
        new(
            "championnat-de-france-ffsa-gt",
            "Championnat de France FFSA GT (GT4 France)",
            ChampionshipCategory.Circuit,
            "FFSA / SRO Motorsports Group",
            1997,
            "Le championnat national de Grand Tourisme, disputé avec des voitures de catégorie GT4 " +
            "(Alpine, Porsche, Mercedes-AMG, Audi, BMW...) sur les grands circuits français comme " +
            "Le Castellet, Magny-Cours, Nogaro ou Lédenon.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-ffsa-tourisme",
            "Championnat de France FFSA Tourisme (TC France)",
            ChampionshipCategory.Circuit,
            "FFSA",
            null,
            "Le championnat de France des voitures de tourisme, avec des berlines de course proches " +
            "de la réglementation TCR. Il partage généralement ses meetings avec le FFSA GT.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-f4",
            "Championnat de France F4",
            ChampionshipCategory.Circuit,
            "FFSA Academy",
            2010,
            "La formule de promotion monoplace française, gérée par la FFSA Academy au Mans. " +
            "Véritable tremplin vers le haut niveau, elle a révélé Pierre Gasly, Esteban Ocon, " +
            "Anthoine Hubert, Théo Pourchaire ou Isack Hadjar.",
            "https://www.ffsa.org"),
        new(
            "porsche-carrera-cup-france",
            "Porsche Carrera Cup France",
            ChampionshipCategory.Circuit,
            "Porsche France",
            1987,
            "La coupe monotype la plus relevée de l'Hexagone : toutes les voitures sont des Porsche " +
            "911 GT3 Cup identiques. Les courses ont lieu en lever de rideau de grands événements " +
            "en France et en Europe.",
            null),
        new(
            "alpine-elf-cup-series",
            "Alpine Elf Cup Series",
            ChampionshipCategory.Circuit,
            "Alpine Racing",
            2018,
            "Coupe monotype disputée avec l'Alpine A110 Cup, héritière de l'Alpine Elf Europa Cup. " +
            "Elle met en avant la marque dieppoise sur les circuits français et européens.",
            null),
        new(
            "clio-cup-series",
            "Clio Cup Series",
            ChampionshipCategory.Circuit,
            "Renault Sport Racing",
            null,
            "Coupe monotype historique du sport automobile français, disputée avec la Renault Clio Cup. " +
            "Formule d'accès réputée pour son excellent rapport coût/plaisir et ses pelotons très disputés.",
            null),
        new(
            "ultimate-cup-series",
            "Ultimate Cup Series",
            ChampionshipCategory.Circuit,
            "Ultimate Cup Organisation",
            2019,
            "Série française d'endurance et de sprint ouverte aux prototypes (LMP3, CN...), aux GT " +
            "et aux monoplaces, avec des meetings sur les grands circuits français et européens.",
            null),

        // ── Rallye ──────────────────────────────────────────────────────────
        new(
            "championnat-de-france-des-rallyes",
            "Championnat de France des Rallyes (asphalte)",
            ChampionshipCategory.Rallye,
            "FFSA",
            1967,
            "Le championnat de France « asphalte », dont le calendrier s'appuie sur des épreuves " +
            "mythiques comme le Rallye du Touquet, le Rallye du Var, Antibes ou le Rouergue. " +
            "Sébastien Loeb et Sébastien Ogier y ont fait leurs armes.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-des-rallyes-terre",
            "Championnat de France des Rallyes Terre",
            ChampionshipCategory.Rallye,
            "FFSA",
            null,
            "Le pendant « terre » du championnat de France des rallyes, disputé sur des spéciales " +
            "en terre et gravier (Terre des Causses, Terre de Vaucluse, Cardabelles...). " +
            "Une excellente école de pilotage sur surface glissante.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-rallycross",
            "Championnat de France de Rallycross",
            ChampionshipCategory.Rallye,
            "FFSA",
            1977,
            "Courses en sprint sur circuits mixtes terre/asphalte (Lohéac, Dreux, Lessay...), avec " +
            "des divisions Supercar, Super 1600 et Division 3/4. Le meeting de Lohéac attire chaque " +
            "année des dizaines de milliers de spectateurs.",
            "https://www.ffsa.org"),

        // ── Montagne ────────────────────────────────────────────────────────
        new(
            "championnat-de-france-de-la-montagne",
            "Championnat de France de la Montagne",
            ChampionshipCategory.Montagne,
            "FFSA",
            null,
            "Le championnat des courses de côte : des tracés en montée contre le chronomètre " +
            "(Mont-Dore, Turckheim, Saint-Ursanne côté français...). Il oppose des monoplaces, " +
            "des protos et des voitures fermées de production.",
            "https://www.ffsa.org"),

        // ── Tout-terrain ────────────────────────────────────────────────────
        new(
            "championnat-de-france-des-rallyes-tout-terrain",
            "Championnat de France des Rallyes Tout-Terrain",
            ChampionshipCategory.ToutTerrain,
            "FFSA",
            null,
            "Rallyes disputés hors asphalte avec buggys et 4x4 de type cross-country " +
            "(Plaines et Vallées, Gatinais, Dunes et Marais...). Une discipline qui prépare " +
            "aux grands rallyes-raids comme le Dakar.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-autocross-sprint-car",
            "Championnat de France d'Autocross et Sprint Car",
            ChampionshipCategory.ToutTerrain,
            "FFSA",
            null,
            "Courses en peloton sur circuits en terre fermés, avec des buggys monoplaces (Sprint Car) " +
            "et des voitures de tourisme préparées (Autocross). Spectacle garanti et épreuves très " +
            "populaires dans l'Ouest et le Sud-Ouest.",
            "https://www.ffsa.org"),
        new(
            "championnat-de-france-endurance-tout-terrain",
            "Championnat de France d'Endurance Tout-Terrain",
            ChampionshipCategory.ToutTerrain,
            "FFSA",
            null,
            "Épreuves d'endurance hors route de plusieurs heures, façon « Baja » à la française, " +
            "dont les 24 Heures Tout-Terrain de France sont l'épreuve phare.",
            "https://www.ffsa.org"),

        // ── Karting ─────────────────────────────────────────────────────────
        new(
            "championnat-de-france-karting",
            "Championnat de France de Karting",
            ChampionshipCategory.Karting,
            "FFSA Karting",
            null,
            "Les championnats de France de karting couvrent toutes les catégories, des Minimes et " +
            "Cadets jusqu'aux KZ à boîte de vitesses. C'est la porte d'entrée du sport automobile : " +
            "la quasi-totalité des pilotes professionnels français en sont issus.",
            "https://www.ffsa.org"),
        new(
            "iame-series-france",
            "IAME Series France",
            ChampionshipCategory.Karting,
            "IAME France",
            null,
            "Série monomarque de karting disputée avec les moteurs IAME (X30). L'une des séries " +
            "clients les plus fournies de France, avec une finale internationale très convoitée.",
            null),

        // ── Drift ───────────────────────────────────────────────────────────
        new(
            "championnat-de-france-de-drift",
            "Championnat de France de Drift",
            ChampionshipCategory.Drift,
            "French Drift Championship / FFSA",
            null,
            "Le championnat national de drift : les pilotes sont jugés sur l'angle, la vitesse et " +
            "le style de leurs passages en glisse, en runs individuels puis en battles à deux voitures.",
            null),

        // ── Camion ──────────────────────────────────────────────────────────
        new(
            "championnat-de-france-camions",
            "Championnat de France Camions",
            ChampionshipCategory.Camion,
            "FFSA",
            null,
            "Courses de tracteurs de course de plus de 1 000 chevaux sur circuits, dont le " +
            "Grand Prix Camions de Nogaro et le Grand Prix de France Camions au Castellet " +
            "sont les rendez-vous majeurs.",
            "https://www.ffsa.org"),

        // ── Historique ──────────────────────────────────────────────────────
        new(
            "historic-tour",
            "Historic Tour — Championnat de France Historique des Circuits",
            ChampionshipCategory.Historique,
            "FFSA / HVM Racing",
            null,
            "Le championnat de France des voitures historiques de compétition (VHC) sur circuit : " +
            "monoplaces, protos et tourisme des années 1960 à 1990 se retrouvent sur des plateaux " +
            "dédiés lors des meetings Historic Tour.",
            "https://www.ffsa.org"),
    };

    public IReadOnlyList<Championship> Tous => Championnats;

    public Championship? ParSlug(string slug) =>
        Championnats.FirstOrDefault(c => string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Championship> Rechercher(string? texte, ChampionshipCategory? categorie)
    {
        IEnumerable<Championship> resultat = Championnats;

        if (categorie is not null)
        {
            resultat = resultat.Where(c => c.Categorie == categorie);
        }

        if (!string.IsNullOrWhiteSpace(texte))
        {
            resultat = resultat.Where(c =>
                c.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(texte, StringComparison.OrdinalIgnoreCase) ||
                c.Organisateur.Contains(texte, StringComparison.OrdinalIgnoreCase));
        }

        return resultat.OrderBy(c => c.Categorie).ThenBy(c => c.Nom).ToList();
    }

    public IReadOnlyDictionary<ChampionshipCategory, int> CompteParCategorie() =>
        Championnats
            .GroupBy(c => c.Categorie)
            .ToDictionary(g => g.Key, g => g.Count());
}
