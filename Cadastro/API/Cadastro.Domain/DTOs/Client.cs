using System;

namespace Cadastro.Domain.DTOs
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public string Occupation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}