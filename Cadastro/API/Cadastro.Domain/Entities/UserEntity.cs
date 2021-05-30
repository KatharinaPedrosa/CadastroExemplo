using Cadastro.Domain.Abstraction.Entities;

namespace Cadastro.Domain.Entities
{
    public class UserEntity : IEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }
    }
}