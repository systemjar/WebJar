//using Blazored.Modal.Services;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Authorization;

//namespace WebJar.Frontend.Shared
//{
//    public partial class AuthLinks
//    {
//        private string? photoUser;
//        private string? nameUser;

//        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
//        [CascadingParameter] private IModalService Modal { get; set; } = default!;

//        protected override async Task OnParametersSetAsync()
//        {
//            var authenticationState = await AuthenticationStateTask;
//            var claims = authenticationState.User.Claims.ToList();
//            var nameUser = claims[0];
//            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
//            if (photoClaim is not null)
//            {
//                photoUser = photoClaim.Value;
//            }
//        }

//        private void ShowModal()
//        {
//            Modal.Show<Login>();
//        }
//    }
//}