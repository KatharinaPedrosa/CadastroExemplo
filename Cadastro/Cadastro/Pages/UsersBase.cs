using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Abstractions.Helpers;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Pages
{
    public class UsersBase : ComponentBase
    {
        [Inject]
        public ICadastroService service { get; set; }

        [Inject]
        public IHashHelper HashHelper { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public List<User> Users { get; set; }

        public User User { get; set; } = new User();

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            service.StateChange += ((s, e) => StateHasChanged());
            service.StateChange += (async (s, e) => await LoadUsers());
            service.State.CurrentLocation = "Usuários";
            await LoadUsers();
        }

        public async Task SaveUser()
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                Loading = true;
                bool result;
                User.PasswordHash = await HashHelper.GetMD5(User.PasswordHash);
                if (User.Id == 0)
                {
                    result = await service.UserService.Add(User, service.State.LoggedUser.Token);
                }
                else
                {
                    result = await service.UserService.Update(User, service.State.LoggedUser.Token);
                }
                Users = await service.UserService.Get(service.State.LoggedUser.Token);
                await LoadUsers();
                Loading = false;
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        public async Task DeleteUser(int id)
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                Loading = true;
                var result = await service.UserService.Delete(id, service.State.LoggedUser.Token);
                Users = await service.UserService.Get(service.State.LoggedUser.Token);
                await LoadUsers();
                Loading = false;
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        public void SelectUser(int id)
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                User = Users.FirstOrDefault(u => u.Id == id);
                if (User == null)
                {
                    NewUser();
                }
                else
                {
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        public void NewUser()
        {
            try
            {
                User = new User();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }

        private async Task LoadUsers()
        {
            if (service.State.LoggedUser == null) return;
            try
            {
                Loading = true;
                Users = await service.UserService.Get(service.State.LoggedUser.Token);
                NewUser();
                Loading = false;
            }
            catch (Exception ex)
            {
                service.HandleError(ex);
            }
        }
    }
}