---
title: MotorsportHub
emoji: 🏁
colorFrom: red
colorTo: gray
sdk: docker
app_port: 8080
pinned: false
---

# 🏁 MotorsportHub

Annuaire des championnats de sport automobile disputés en France : circuit, rallye,
montagne, tout-terrain, karting, drift, camion et courses historiques.

Le site est développé avec **Blazor** (.NET 10, rendu interactif côté serveur), selon une
**architecture en oignon** : le domaine au centre, sans dépendance ; les cas d'usage et les
ports (interfaces) dans la couche Application ; les implémentations techniques dans
Infrastructure ; et le front Blazor en périphérie. Un projet API pourra s'ajouter plus tard
à côté du front, en consommant la même couche Application.

## 🚀 Lancer le projet dans un Codespace

1. Sur GitHub, cliquez sur **Code → Codespaces → Create codespace on main**
   (ou sur la branche de votre choix).
2. Le conteneur de développement (défini dans `.devcontainer/devcontainer.json`)
   s'ouvre avec le SDK .NET 10 préinstallé et exécute automatiquement `dotnet restore`.
3. Dans le terminal du Codespace, lancez :

   ```bash
   dotnet watch --project src/MotorsportHub.Web
   ```

4. Le port **5080** est automatiquement transféré : GitHub ouvre un aperçu du site
   dans l'éditeur (ou cliquez sur la notification / l'onglet **Ports** pour ouvrir
   l'URL dans le navigateur).

`dotnet watch` recharge l'application à chaque modification de fichier (hot reload).

## 💻 Lancer le projet en local

Prérequis : [SDK .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0)

```bash
dotnet run --project src/MotorsportHub.Web
```

Puis ouvrez <http://localhost:5080>.

## 📁 Structure du projet

```
MotorsportHub.sln
.devcontainer/
  devcontainer.json                 # Configuration Codespace (.NET 10, port 5080)
src/
  MotorsportHub.Domain/             # ── Cœur : entités, aucune dépendance
    Entites/                        #    Plateau, Discipline, Organisateur, StatutPlateau
  MotorsportHub.Application/        # ── Cas d'usage et ports
    Interfaces/                     #    IPlateauRepository, IDisciplineRepository
    Services/PlateauxService.cs     #    Consommé par le front (et l'API plus tard)
  MotorsportHub.Infrastructure/     # ── Implémentations techniques
    Donnees/DonneesInitiales.cs     #    Jeu de données (futur seed EF Core)
    Depots/                         #    Repositories en mémoire (futur EF Core)
  MotorsportHub.Web/                # ── Front Blazor (présentation)
    Program.cs                      #    Composition root (câblage des couches)
    Components/                     #    Pages et layout
    wwwroot/app.css                 #    Styles du site
```

La règle de dépendance pointe vers l'intérieur : `Web → Infrastructure → Application → Domain`.
Le front ne connaît que les entités du Domain et les services de l'Application.

## 📝 Ajouter ou modifier un plateau (championnat)

Chaque championnat est représenté par l'entité `Plateau`. Les données sont pour
l'instant embarquées dans
`src/MotorsportHub.Infrastructure/Donnees/DonneesInitiales.cs` : ajoutez une entrée
via le helper `Ajouter(...)`. Lorsque la base de données arrivera (SQLite ou
PostgreSQL + EF Core), cette classe deviendra le seed, et les repositories en
mémoire seront remplacés par des repositories EF Core — sans toucher au front.

## 🚢 Déploiement gratuit

Le dépôt contient un `Dockerfile` multi-étapes prêt pour deux hébergeurs gratuits.

### Hugging Face Spaces (sans carte bancaire)

Le bloc YAML en tête de ce README est la configuration du Space
(`sdk: docker`, `app_port: 8080`).

1. Créez un compte sur [huggingface.co](https://huggingface.co), puis
   **New Space** → SDK **Docker** → template **Blank** → matériel **CPU basic (free)**.
2. Créez un jeton d'accès en écriture : **Settings → Access Tokens → New token (Write)**.
3. Poussez le dépôt vers le Space (le jeton sert de mot de passe) :

   ```bash
   git remote add space https://huggingface.co/spaces/<votre-pseudo>/motorsporthub
   git push space <votre-branche>:main
   ```

4. Le Space se construit (~3-5 min) puis le site est servi sur
   `https://<votre-pseudo>-motorsporthub.hf.space`. Le Space s'endort après
   48 h sans visite et se réveille à la première requête.

### Render (carte demandée à l'inscription pour vérification)

`render.yaml` décrit le service : **New → Blueprint** sur le repo (lit la branche
par défaut), ou **New → Web Service** en choisissant la branche manuellement
(ajouter alors la variable d'environnement `PORT=8080`). Mise en veille après
15 min d'inactivité sur le plan gratuit.

## 🧭 Évolutions prévues

- Entités `Circuit`, `Pilote`, `Voiture`, `Saison`, `Épreuve` dans le Domain
- Projet `MotorsportHub.Api` (minimal API) consommant la couche Application
- Base de données via EF Core dans Infrastructure
