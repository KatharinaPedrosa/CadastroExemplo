using System;

namespace Cadastro.Domain.Entities
{
    public class ClientEntity
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