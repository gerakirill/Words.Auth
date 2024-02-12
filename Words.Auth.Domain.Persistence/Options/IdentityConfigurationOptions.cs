namespace Words.Auth.Domain.Persistence.Options;

public class IdentityConfigurationOptions
{
    public const string IdentityConfigurationName = "IdentityOptions";

    public string[] Scopes { get; set; } = [];

    public Client[] Clients { get; set; } = [];

    public string[] Resources { get; set; } = [];

    public ApiResource[] ApiResources { get; set; } = [];
}

public class Client
{
    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string[] AllowedGrantTypes { get; set; } = [];

    public bool RequireClientSecret { get; set; } = false;

    public string[] ClientSecrets { get; set; } = [];

    public string[] AllowedScopes { get; set; } = [];
}

public class ApiResource
{
    public string Name { get; set; } = string.Empty;

    public string[] AllowedScopes { get; set; } = [];
}