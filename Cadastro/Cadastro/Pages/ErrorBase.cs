using System;
using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Pages
{
    public class ErrorBase : ComponentBase
    {
        [Parameter]
        public Exception exception { get; set; }

        [Inject]
        public ICadastroService service { get; set; }

        public bool Collapsed { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            service.StateChange += ((s, e) => StateHasChanged());
            service.State.CurrentLocation = "Erro";
        }

        public void ToggleCollapseState()
        {
            Collapsed = !Collapsed;
        }
    }
}