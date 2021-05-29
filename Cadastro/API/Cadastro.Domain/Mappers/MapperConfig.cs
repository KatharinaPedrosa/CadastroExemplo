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
        }

        private void ConfigureUser()
        {
            CreateMap<User, UserEntity>();
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
    }
}