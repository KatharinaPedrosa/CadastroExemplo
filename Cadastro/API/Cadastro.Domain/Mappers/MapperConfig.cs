using AutoMapper;
using Cadastro.Domain.DTOs;
using Cadastro.Domain.Entities;

namespace Cadastro.Domain.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            ConfigureClient();
            ConfigureUser();
            ConfigureAddress();
        }

        private void ConfigureUser()
        {
            CreateMap<User, UserEntity>()
                .ForMember(
                    dest => dest.IsAdmin,
                    opt => opt.MapFrom(v => false));
            CreateMap<UserEntity, User>()
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Token,
                    opt => opt.Ignore());
        }

        private void ConfigureClient()
        {
            CreateMap<Client, ClientEntity>()
                .ReverseMap();
        }

        private void ConfigureAddress()
        {
            CreateMap<Address, AddressEntity>()
                .ReverseMap();
        }
    }
}