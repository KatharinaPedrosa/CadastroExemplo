using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        public ICadastroService service { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public User LoginUser { get; set; } = new User();

        public bool LoginFail { get; set; } = false;

        public string LoginMessage { get; set; } = "";

        public bool TopRowHover { get; set; } = false;

        public string SidebarCssClass => Expanded ? "sidebar-expanded" : "sidebar-collapsed";

        public string ContentCssClass => Expanded ? "content-expanded" : "content-collapsed";

        public bool Expanded { get; set; } = false;

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            service.StateChange += ((s, e) => StateHasChanged());
            await service.CheckLoggedUser();
            Loading = false;
            StateHasChanged();
        }

        public async Task Login()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await service.Login(LoginUser.Login, LoginUser.PasswordHash);
                LoginFail = false;
                LoginMessage = "";
            }
            catch (System.Exception)
            {
                LoginMessage = "Usuário ou senha inválidos";
                LoginFail = true;
            }
            finally
            {
                Loading = false;
                StateHasChanged();
            }
        }

        public async Task LogOut()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await service.LogOut();
                NavManager.NavigateTo("/");
            }
            catch (System.Exception e)
            {
                service.HandleError(e);
            }
            finally
            {
                Loading = false;
                StateHasChanged();
            }
        }
    }
}