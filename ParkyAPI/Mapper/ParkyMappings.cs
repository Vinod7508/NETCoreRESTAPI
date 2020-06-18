using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Dtos;
using ParkyAPI.Models;

namespace ParkyAPI.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            // here we are mappping application domain class with and Dto and vice versa.
            CreateMap<NPark, NParkDto>().ReverseMap();  
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailupdateDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
        }


    }
}
