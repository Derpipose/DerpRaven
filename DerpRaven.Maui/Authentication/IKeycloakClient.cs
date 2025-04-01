using Duende.IdentityModel.OidcClient;

namespace DerpRaven.Maui.Authentication
{
    public interface IKeycloakClient
    {
        OktaClientConfiguration Configuration { get; }
        string? IdentityToken { get; }

        Task<LoginResult> LoginAsync(CancellationToken cancellationToken = default);
        Task<LogoutResult> LogoutAsync(string idToken, CancellationToken cancellationToken = default);
    }
}