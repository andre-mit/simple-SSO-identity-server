using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer;

public class IdentityConfiguration
{
    public static List<TestUser> TestUsers =>
        new()
        {
            new()
            {
                SubjectId = "1144",
                Username = "andre",
                Password = "nemsey",
                Claims =
                {
                    new(JwtClaimTypes.Name, "André Augusto"),
                    new(JwtClaimTypes.GivenName, "André"),
                    new(JwtClaimTypes.FamilyName, "Augusto"),
                    new(JwtClaimTypes.WebSite, "https://andre-mit.dev"),
                }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("myApi.read"),
            new ApiScope("myApi.write"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("myApi")
            {
                Scopes = new List<string>{ "myApi.read","myApi.write" },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "cwm.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "myApi.read" }
            },
        };
}