using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Mappers;
using Backend.Models;
using Backend.Domain;

namespace Backend.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<USER, User>();
            CreateMap<User, USER>();
        }
    }
}