using System;
using System.Threading.Tasks;
using Cadastro.Abstractions.Helpers;
using Cadastro.Abstractions.Services;
using Cadastro.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace Cadastro.Services
{
    public class CadastroService : ICadastroService
    {
        private const string LOGGED_USER_KEY = "LoggedUser";

        private readonly IHashHelper hashHelper;
        private readonly ILocalStorageHelper localStorageHelper;
        private readonly NavigationManager navManager;

        public AppState State { get; set; }

        public event EventHandler<StateChangeEventArgs> StateChange;

        public event EventHandler<EventArgs> ReloadData;

        public IUserService UserService { get; }

        public IClientService ClientService { get; }

        public CadastroService(
            IUserService userService,
            IClientService clientService,
            IHashHelper hashHelper,
            ILocalStorageHelper localStorageHelper,
            NavigationManager navManager)
        {
            UserService = userService;
            ClientService = clientService;
            this.localStorageHelper = localStorageHelper;
            this.hashHelper = hashHelper;
            this.navManager = navManager;

            State = new AppState();
            State.StateChange += RaiseNotifyChange;
        }

        public async Task CheckLoggedUser()
        {
            State.LoggedUser = await localStorageHelper.GetItem<User>(LOGGED_USER_KEY);
        }

        public async Task<bool> Login(string login, string password)
        {
            var passwordHash = await hashHelper.GetMD5(password);
            State.LoggedUser = await UserService.Login(login, passwordHash);
            if (State.LoggedUser != null)
            {
                await localStorageHelper.SetItem(LOGGED_USER_KEY, State.LoggedUser);
                return true;
            }
            return false;
        }

        public async Task LogOut()
        {
            if (State.LoggedUser != null)
            {
                State.LoggedUser = null;
                await localStorageHelper.RemoveItem(LOGGED_USER_KEY);
            }
        }

        public void NotifyChange()
        {
            RaiseNotifyChange(this, new StateChangeEventArgs("Cadastro", this));
        }

        private void RaiseNotifyChange(object sender, StateChangeEventArgs e)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(sender, e);
        }

        public void HandleError(Exception exception)
        {
            State.CurrentError = exception;
            navManager.NavigateTo("/error");
        }

        public void RequestReload()
        {
            EventHandler<EventArgs> handler = ReloadData;
            handler?.Invoke(this, new EventArgs());
        }
    }
}