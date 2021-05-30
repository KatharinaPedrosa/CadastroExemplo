using System;
using Cadastro.Domain.Abstraction.Entities;

namespace Cadastro.Domain.Entities
{
    public class ClientEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public string Occupation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AddressEntity Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}