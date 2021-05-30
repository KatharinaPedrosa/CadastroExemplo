using Cadastro.Domain.Abstraction.Entities;

namespace Cadastro.Domain.Entities
{
    public class AddressEntity : IEntity
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public ClientEntity Client { get; set; }
    }
}