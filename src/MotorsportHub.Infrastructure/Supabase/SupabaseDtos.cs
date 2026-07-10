using System.Text.Json.Serialization;
using MotorsportHub.Domain.Entites;

namespace MotorsportHub.Infrastructure.Supabase;

// Formes JSON renvoyées par l'API REST de Supabase (PostgREST, colonnes en snake_case).

internal sealed class DisciplineDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("slug")] public string Slug { get; set; } = string.Empty;
    [JsonPropertyName("nom")] public string Nom { get; set; } = string.Empty;
    [JsonPropertyName("emoji")] public string Emoji { get; set; } = string.Empty;
    [JsonPropertyName("ordre")] public int Ordre { get; set; }

    public Discipline VersEntite() => new()
    {
        Id = Id,
        Slug = Slug,
        Nom = Nom,
        Emoji = Emoji,
        Ordre = Ordre
    };
}

internal sealed class OrganisateurDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("nom")] public string Nom { get; set; } = string.Empty;
    [JsonPropertyName("site_web")] public string? SiteWeb { get; set; }

    public Organisateur VersEntite() => new()
    {
        Id = Id,
        Nom = Nom,
        SiteWeb = SiteWeb
    };
}

internal sealed class PlateauDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("slug")] public string Slug { get; set; } = string.Empty;
    [JsonPropertyName("nom")] public string Nom { get; set; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("annee_creation")] public int? AnneeCreation { get; set; }
    [JsonPropertyName("site_web")] public string? SiteWeb { get; set; }
    [JsonPropertyName("statut")] public string Statut { get; set; } = "actif";
    [JsonPropertyName("discipline")] public DisciplineDto Discipline { get; set; } = new();
    [JsonPropertyName("organisateur")] public OrganisateurDto Organisateur { get; set; } = new();
}
