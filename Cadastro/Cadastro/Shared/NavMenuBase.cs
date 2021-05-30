using Cadastro.Abstractions.Services;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Shared
{
    public class NavMenuBase : ComponentBase
    {
        public bool collapseNavMenu = true;

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        [Inject]
        public ICadastroService service { get; set; }

        protected override void OnInitialized()
        {
            service.StateChange += ((s, e) => StateHasChanged());
        }

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}