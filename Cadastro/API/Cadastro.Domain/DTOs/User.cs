namespace Cadastro.Domain.DTOs
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string NewPasswordHash { get; set; }

        public string Token { get; set; }
    }
}