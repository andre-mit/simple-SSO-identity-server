using IdentityModel.Client;

namespace WebClient.Service;

public class TokenService : ITokenService
{
    private DiscoveryDocumentResponse _discoveryDocument { get; set; }

    public TokenService()
    {
        using var client = new HttpClient();
        _discoveryDocument = client.GetDiscoveryDocumentAsync("https://localhost:7138/.well-known/openid-configuration")
            .Result;
    }

    public async Task<TokenResponse> GetToken(string scope)
    {
        using (var client = new HttpClient())
        {
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = "cwm.client",
                Scope = scope,
                ClientSecret = "secret"
            });
            if (tokenResponse.IsError)
            {
                throw new Exception("Token Error");
            }

            return tokenResponse;
        }
    }
}