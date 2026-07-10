using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Infrastructure.Donnees;

/// <summary>
/// Jeu de données embarqué. Lorsque le projet passera sur une base de données,
/// cette classe servira de source pour le seed EF Core.
/// </summary>
public static class DonneesInitiales
{
    public static IReadOnlyList<Discipline> Disciplines { get; }
    public static IReadOnlyList<Organisateur> Organisateurs { get; }
    public static IReadOnlyList<Plateau> Plateaux { get; }

    static DonneesInitiales()
    {
        var circuit = new Discipline { Id = 1, Slug = "circuit", Nom = "Circuit", Emoji = "🏎️", Ordre = 1 };
        var rallye = new Discipline { Id = 2, Slug = "rallye", Nom = "Rallye", Emoji = "🌲", Ordre = 2 };
        var montagne = new Discipline { Id = 3, Slug = "montagne", Nom = "Montagne", Emoji = "⛰️", Ordre = 3 };
        var toutTerrain = new Discipline { Id = 4, Slug = "tout-terrain", Nom = "Tout-terrain", Emoji = "🏜️", Ordre = 4 };
        var karting = new Discipline { Id = 5, Slug = "karting", Nom = "Karting", Emoji = "🏁", Ordre = 5 };
        var drift = new Discipline { Id = 6, Slug = "drift", Nom = "Drift", Emoji = "💨", Ordre = 6 };
        var camion = new Discipline { Id = 7, Slug = "camion", Nom = "Camion", Emoji = "🚚", Ordre = 7 };
        var historique = new Discipline { Id = 8, Slug = "historique", Nom = "Historique", Emoji = "🕰️", Ordre = 8 };

        Disciplines = new List<Discipline>
        {
            circuit, rallye, montagne, toutTerrain, karting, drift, camion, historique
        };

        var ffsa = new Organisateur { Id = 1, Nom = "FFSA", SiteWeb = "https://www.ffsa.org" };
        var sro = new Organisateur { Id = 2, Nom = "SRO Motorsports Group" };
        var ffsaAcademy = new Organisateur { Id = 3, Nom = "FFSA Academy", SiteWeb = "https://www.ffsa.org" };
        var porscheFrance = new Organisateur { Id = 4, Nom = "Porsche France" };
        var alpineRacing = new Organisateur { Id = 5, Nom = "Alpine Racing" };
        var renaultSport = new Organisateur { Id = 6, Nom = "Renault Sport Racing" };
        var ultimateCup = new Organisateur { Id = 7, Nom = "Ultimate Cup Organisation" };
        var iameFrance = new Organisateur { Id = 8, Nom = "IAME France" };
        var fdc = new Organisateur { Id = 9, Nom = "French Drift Championship" };
        var hvmRacing = new Organisateur { Id = 10, Nom = "HVM Racing" };

        Organisateurs = new List<Organisateur>
        {
            ffsa, sro, ffsaAcademy, porscheFrance, alpineRacing,
            renaultSport, ultimateCup, iameFrance, fdc, hvmRacing
        };

        var plateaux = new List<Plateau>();
        var idSuivant = 1;

        void Ajouter(
            string slug,
            string nom,
            Discipline discipline,
            int? anneeCreation,
            string description,
            string? siteWeb,
            params Organisateur[] organisateurs)
        {
            var plateau = new Plateau
            {
                Id = idSuivant++,
                Slug = slug,
                Nom = nom,
                Description = description,
                AnneeCreation = anneeCreation,
                SiteWeb = siteWeb,
                Statut = StatutPlateau.Actif,
                DisciplineId = discipline.Id,
                Discipline = discipline
            };

            foreach (var organisateur in organisateurs)
            {
                plateau.Organisateur = organisateur;
                organisateur.Plateaux.Add(plateau);
            }

            discipline.Plateaux.Add(plateau);
            plateaux.Add(plateau);
        }

        // ── Circuit ─────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-ffsa-gt",
            "Championnat de France FFSA GT (GT4 France)",
            circuit,
            1997,
            "Le championnat national de Grand Tourisme, disputé avec des voitures de catégorie GT4 " +
            "(Alpine, Porsche, Mercedes-AMG, Audi, BMW...) sur les grands circuits français comme " +
            "Le Castellet, Magny-Cours, Nogaro ou Lédenon.",
            "https://www.ffsa.org",
            ffsa, sro);

        Ajouter(
            "championnat-de-france-ffsa-tourisme",
            "Championnat de France FFSA Tourisme (TC France)",
            circuit,
            null,
            "Le championnat de France des voitures de tourisme, avec des berlines de course proches " +
            "de la réglementation TCR. Il partage généralement ses meetings avec le FFSA GT.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "championnat-de-france-f4",
            "Championnat de France F4",
            circuit,
            2010,
            "La formule de promotion monoplace française, gérée par la FFSA Academy au Mans. " +
            "Véritable tremplin vers le haut niveau, elle a révélé Pierre Gasly, Esteban Ocon, " +
            "Anthoine Hubert, Théo Pourchaire ou Isack Hadjar.",
            "https://www.ffsa.org",
            ffsaAcademy);

        Ajouter(
            "porsche-carrera-cup-france",
            "Porsche Carrera Cup France",
            circuit,
            1987,
            "La coupe monotype la plus relevée de l'Hexagone : toutes les voitures sont des Porsche " +
            "911 GT3 Cup identiques. Les courses ont lieu en lever de rideau de grands événements " +
            "en France et en Europe.",
            null,
            porscheFrance);

        Ajouter(
            "alpine-elf-cup-series",
            "Alpine Elf Cup Series",
            circuit,
            2018,
            "Coupe monotype disputée avec l'Alpine A110 Cup, héritière de l'Alpine Elf Europa Cup. " +
            "Elle met en avant la marque dieppoise sur les circuits français et européens.",
            null,
            alpineRacing);

        Ajouter(
            "clio-cup-series",
            "Clio Cup Series",
            circuit,
            null,
            "Coupe monotype historique du sport automobile français, disputée avec la Renault Clio Cup. " +
            "Formule d'accès réputée pour son excellent rapport coût/plaisir et ses pelotons très disputés.",
            null,
            renaultSport);

        Ajouter(
            "ultimate-cup-series",
            "Ultimate Cup Series",
            circuit,
            2019,
            "Série française d'endurance et de sprint ouverte aux prototypes (LMP3, CN...), aux GT " +
            "et aux monoplaces, avec des meetings sur les grands circuits français et européens.",
            null,
            ultimateCup);

        // ── Rallye ──────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-des-rallyes",
            "Championnat de France des Rallyes (asphalte)",
            rallye,
            1967,
            "Le championnat de France « asphalte », dont le calendrier s'appuie sur des épreuves " +
            "mythiques comme le Rallye du Touquet, le Rallye du Var, Antibes ou le Rouergue. " +
            "Sébastien Loeb et Sébastien Ogier y ont fait leurs armes.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "championnat-de-france-des-rallyes-terre",
            "Championnat de France des Rallyes Terre",
            rallye,
            null,
            "Le pendant « terre » du championnat de France des rallyes, disputé sur des spéciales " +
            "en terre et gravier (Terre des Causses, Terre de Vaucluse, Cardabelles...). " +
            "Une excellente école de pilotage sur surface glissante.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "championnat-de-france-rallycross",
            "Championnat de France de Rallycross",
            rallye,
            1977,
            "Courses en sprint sur circuits mixtes terre/asphalte (Lohéac, Dreux, Lessay...), avec " +
            "des divisions Supercar, Super 1600 et Division 3/4. Le meeting de Lohéac attire chaque " +
            "année des dizaines de milliers de spectateurs.",
            "https://www.ffsa.org",
            ffsa);

        // ── Montagne ────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-de-la-montagne",
            "Championnat de France de la Montagne",
            montagne,
            null,
            "Le championnat des courses de côte : des tracés en montée contre le chronomètre " +
            "(Mont-Dore, Turckheim...). Il oppose des monoplaces, des protos et des voitures " +
            "fermées de production.",
            "https://www.ffsa.org",
            ffsa);

        // ── Tout-terrain ────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-des-rallyes-tout-terrain",
            "Championnat de France des Rallyes Tout-Terrain",
            toutTerrain,
            null,
            "Rallyes disputés hors asphalte avec buggys et 4x4 de type cross-country " +
            "(Plaines et Vallées, Gatinais, Dunes et Marais...). Une discipline qui prépare " +
            "aux grands rallyes-raids comme le Dakar.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "championnat-de-france-autocross-sprint-car",
            "Championnat de France d'Autocross et Sprint Car",
            toutTerrain,
            null,
            "Courses en peloton sur circuits en terre fermés, avec des buggys monoplaces (Sprint Car) " +
            "et des voitures de tourisme préparées (Autocross). Spectacle garanti et épreuves très " +
            "populaires dans l'Ouest et le Sud-Ouest.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "championnat-de-france-endurance-tout-terrain",
            "Championnat de France d'Endurance Tout-Terrain",
            toutTerrain,
            null,
            "Épreuves d'endurance hors route de plusieurs heures, façon « Baja » à la française, " +
            "dont les 24 Heures Tout-Terrain de France sont l'épreuve phare.",
            "https://www.ffsa.org",
            ffsa);

        // ── Karting ─────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-karting",
            "Championnat de France de Karting",
            karting,
            null,
            "Les championnats de France de karting couvrent toutes les catégories, des Minimes et " +
            "Cadets jusqu'aux KZ à boîte de vitesses. C'est la porte d'entrée du sport automobile : " +
            "la quasi-totalité des pilotes professionnels français en sont issus.",
            "https://www.ffsa.org",
            ffsa);

        Ajouter(
            "iame-series-france",
            "IAME Series France",
            karting,
            null,
            "Série monomarque de karting disputée avec les moteurs IAME (X30). L'une des séries " +
            "clients les plus fournies de France, avec une finale internationale très convoitée.",
            null,
            iameFrance);

        // ── Drift ───────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-de-drift",
            "Championnat de France de Drift",
            drift,
            null,
            "Le championnat national de drift : les pilotes sont jugés sur l'angle, la vitesse et " +
            "le style de leurs passages en glisse, en runs individuels puis en battles à deux voitures.",
            null,
            fdc, ffsa);

        // ── Camion ──────────────────────────────────────────────────────────
        Ajouter(
            "championnat-de-france-camions",
            "Championnat de France Camions",
            camion,
            null,
            "Courses de tracteurs de course de plus de 1 000 chevaux sur circuits, dont le " +
            "Grand Prix Camions de Nogaro et le Grand Prix de France Camions au Castellet " +
            "sont les rendez-vous majeurs.",
            "https://www.ffsa.org",
            ffsa);

        // ── Historique ──────────────────────────────────────────────────────
        Ajouter(
            "historic-tour",
            "Historic Tour — Championnat de France Historique des Circuits",
            historique,
            null,
            "Le championnat de France des voitures historiques de compétition (VHC) sur circuit : " +
            "monoplaces, protos et tourisme des années 1960 à 1990 se retrouvent sur des plateaux " +
            "dédiés lors des meetings Historic Tour.",
            "https://www.ffsa.org",
            ffsa, hvmRacing);

        Plateaux = plateaux;
    }
}
