using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkiAPI.Models;
using ParkiAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkiAPI.Controllers
{
    [Route("api/[controller]")] //changeable
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private INationalParkRepository nationalParkRepository;
        private readonly IMapper mapper;

        public NationalParksController(
            INationalParkRepository nationalParkRepository,
            IMapper mapper
            )
        {
            this.nationalParkRepository = nationalParkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = this.nationalParkRepository.GetNationalParks();

            var objDto = new List<NationalParkDto>();

            foreach (var obje in objList) // by this loop we mapping 
            {                             // our tables to Dto tables
                objDto.Add(this.mapper.Map<NationalParkDto>(obje));
            }
            return Ok(objDto); 
        }

        [HttpGet("{nationalParkId:int}")]
        public IActionResult GetNationalPark(int nationalParkId) // gettingById singular item
        {
            var obje = this.nationalParkRepository.GetNationalPark(nationalParkId);

            if (obje == null)
            {
                return NotFound();
            }
            var objDto = this.mapper.Map<NationalParkDto>(obje);
            return Ok(objDto);
        }
    }
}
