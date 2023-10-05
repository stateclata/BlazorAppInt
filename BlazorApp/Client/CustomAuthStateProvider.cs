using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;


namespace BlazorApp.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //get authentication token authToken from localstorage 
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");


            //create empty claims identity in case the authToken is empty
            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null; //unauthorized state of user

            if (!string.IsNullOrEmpty(authToken))
            {
                //if authToken is not empty we try to parse it and get the claims
                //set the default authorization header to the Bearer string
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    if (NotExpiredToken(identity))
                    {
                        _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
                catch
                {
                    //if something went wrong we remove the authToken and again set an empty identity
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            //create a user with identity either empty of with the authToken
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }


        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }


        //method that checks whether the jwt token has expired
        private static bool NotExpiredToken(ClaimsIdentity? identity)
        {
            if (identity == null) return false;
            var user = new ClaimsPrincipal(identity);
            var exp = user.FindFirst("exp");
            if (exp == null) return false;
            var expDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp.Value));
            if (DateTime.Now > expDateTime)
                return false;
            return true;
        }
    }
}
