using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public ICadastroService service { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public bool Loading { get; set; } = true;

        public List<Client> Clients { get; set; } = new List<Client>();

        protected override async Task OnInitializedAsync()
        {
            service.StateChange += ((s, e) => StateHasChanged());
            service.StateChange += (async (s, e) => await LoadClients());
            service.State.CurrentLocation = "Início";
            await LoadClients();
        }

        public void EditClient(int id)
        {
            try
            {
                NavManager.NavigateTo($"/ClientEdit/{id}");
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        public async Task RemoveClient(int id)
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                await service.ClientService.Delete(id, service.State.LoggedUser.Token);
                await LoadClients();
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        private async Task LoadClients()
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                if (service.State.LoggedUser != null)
                {
                    Clients = await service.ClientService.Get(service.State.LoggedUser.Token);
                }
                Loading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }
    }
}