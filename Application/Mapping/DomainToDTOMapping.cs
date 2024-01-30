using AutoMapper;
using Orange_Portfolio_BackEnd.Domain.DTOs;
using Orange_Portfolio_BackEnd.Domain.Models;

namespace Orange_Portfolio_BackEnd.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
