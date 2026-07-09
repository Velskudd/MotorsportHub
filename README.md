# 🏁 MotorsportHub

Annuaire des championnats de sport automobile disputés en France : circuit, rallye,
montagne, tout-terrain, karting, drift, camion et courses historiques.

Le site est développé avec **Blazor** (.NET 8, rendu interactif côté serveur).

## 🚀 Lancer le projet dans un Codespace

1. Sur GitHub, cliquez sur **Code → Codespaces → Create codespace on main**
   (ou sur la branche de votre choix).
2. Le conteneur de développement (défini dans `.devcontainer/devcontainer.json`)
   s'ouvre avec le SDK .NET 8 préinstallé et exécute automatiquement `dotnet restore`.
3. Dans le terminal du Codespace, lancez :

   ```bash
   dotnet watch --project src/MotorsportHub
   ```

4. Le port **5080** est automatiquement transféré : GitHub ouvre un aperçu du site
   dans l'éditeur (ou cliquez sur la notification / l'onglet **Ports** pour ouvrir
   l'URL dans le navigateur).

`dotnet watch` recharge l'application à chaque modification de fichier (hot reload).

## 💻 Lancer le projet en local

Prérequis : [SDK .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
dotnet run --project src/MotorsportHub
```

Puis ouvrez <http://localhost:5080>.

## 📁 Structure du projet

```
MotorsportHub.sln
.devcontainer/
  devcontainer.json          # Configuration Codespace (.NET 8, port 5080)
src/MotorsportHub/
  Program.cs                 # Point d'entrée ASP.NET Core / Blazor
  Components/
    App.razor                # Document HTML racine
    Routes.razor             # Routeur Blazor
    Layout/MainLayout.razor  # Mise en page (barre de navigation, pied de page)
    Pages/
      Home.razor             # Accueil : vue d'ensemble par discipline
      Championships.razor    # Liste avec recherche et filtres par catégorie
      ChampionshipDetail.razor # Fiche détaillée d'un championnat
      About.razor            # À propos
  Models/Championship.cs     # Modèle et catégories de championnats
  Services/ChampionshipService.cs # Données des championnats + recherche
  wwwroot/app.css            # Styles du site
```

## 📝 Ajouter ou modifier un championnat

Les données sont pour l'instant embarquées dans
`src/MotorsportHub/Services/ChampionshipService.cs` : ajoutez simplement une
entrée `new Championship(...)` à la liste. Une évolution possible est de
déplacer ces données vers une base de données (SQLite + EF Core, par exemple).
