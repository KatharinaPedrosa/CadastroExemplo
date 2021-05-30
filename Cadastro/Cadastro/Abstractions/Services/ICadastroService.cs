using System;
using System.Threading.Tasks;
using Cadastro.Services;

namespace Cadastro.Abstractions.Services
{
    public interface ICadastroService
    {
        event EventHandler<StateChangeEventArgs> StateChange;

        event EventHandler<EventArgs> ReloadData;

        AppState State { get; }

        IUserService UserService { get; }

        IClientService ClientService { get; }

        Task CheckLoggedUser();

        Task<bool> Login(string login, string password);

        Task LogOut();

        void NotifyChange();

        void HandleError(Exception exception);

        void RequestReload();
    }
}