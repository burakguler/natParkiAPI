using AutoMapper;
using ParkiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkiAPI.ParkiMapper
{
    public class ParkiMapping : Profile
    {
        public ParkiMapping()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
