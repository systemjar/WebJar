using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebJar.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            var anonimous = new ClaimsIdentity();
            var usuarioprueba = new ClaimsIdentity(authenticationType: "test");
            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Jorge"),
                new Claim("LastName", "Alcántara"),
                new Claim(ClaimTypes.Name, "jar@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");
            var conta = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Jorge"),
                new Claim("LastName", "Alcántara"),
                new Claim(ClaimTypes.Name, "jconta@yopmail.com"),
                new Claim(ClaimTypes.Role, "Conta")
            },
            authenticationType: "test");
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioprueba)));
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
        }
    }
}