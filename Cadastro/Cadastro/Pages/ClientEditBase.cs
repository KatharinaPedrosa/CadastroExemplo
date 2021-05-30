using System;
using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Pages
{
    public class ClientEditBase : ComponentBase
    {
        [Inject]
        public ICadastroService service { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public int Id { get; set; } = 0;

        public Client Client { get; set; } = new Client() { Address = new Address() };

        public bool EditMode { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            service.StateChange += ((s, e) => StateHasChanged());
            service.StateChange += (async (s, e) => await LoadClient());
            if (Id == 0)
            {
                service.State.CurrentLocation = "Adicionar Cliente";
            }
            else
            {
                service.State.CurrentLocation = "Editar Cliente";
            }
            await LoadClient();
        }

        public async Task SaveClient()
        {
            if (service.State.LoggedUser == null) return;
            bool result;
            if (Id == 0)
            {
                result = await service.ClientService.Add(Client, service.State.LoggedUser.Token);
            }
            else
            {
                result = await service.ClientService.Update(Client, service.State.LoggedUser.Token);
            }
            if (result)
            {
                NavManager.NavigateTo("/");
            }
            else
            {
                service.HandleError(new Exception("Erro ao salvar"));
            }
        }

        private async Task LoadClient()
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                if (Id == 0)
                {
                    Client = new Client() { Address = new Address() };
                    EditMode = false;
                }
                else
                {
                    Client = await service.ClientService.Get(Id, service.State.LoggedUser.Token);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }
    }
}