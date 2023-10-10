
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace FrontenAdministracion.Auth
{
    public class UserAuth
    {

        private readonly ILocalStorageService _localStorageService;
        public UserAuth(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        private bool ValidateTokenExpiration(string token)
        {
            List<Claim> claims = JwParser.ParseClaimsFromJwt(token).ToList();
            if (claims?.Count == 0)
            {
                return false;
            }
            string expirationSeconds = claims.Where(_ => _.Type.ToLower() == "exp").Select(_ => _.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(expirationSeconds))
            {
                return false;
            }

            var exprationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expirationSeconds));
            if (exprationDate < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
        public async Task<string> GetTokenAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            if (ValidateTokenExpiration(token))
            {
                return token;
            }
            else
            {
                await Logout();
                return "";
            }
        }
        private async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");
            await _localStorageService.RemoveItemAsync("userId");
        }
    }

    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            string token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                return anonymous;
            }
            var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwParser.ParseClaimsFromJwt(token), "Authentication"));
            var loginUser = new AuthenticationState(userClaimPrincipal);
            return loginUser;
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


    }
}
