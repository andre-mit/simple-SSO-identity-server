using IdentityModel.Client;

namespace WebClient.Service;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}