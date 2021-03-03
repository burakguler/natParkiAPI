using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkiAPI.Models;
using ParkiAPI.Repository.IRepository;
using System.Collections.Generic;

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

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) // if dto table/form null return bad reques
            {
                return BadRequest(ModelState);
            }

            if (this.nationalParkRepository.NationalParkExists(nationalParkDto.name))
            {                           // if this national does exists return error message down below and return status code 404         
                ModelState.AddModelError("", "National Park Exists!"); 
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)    // if model state is not valid return bad request error
            {
                return BadRequest(ModelState);
            }

            // if everythings fine continue to work
            var nationalParkobje = this.mapper.Map<NationalPark>(nationalParkDto);
            if (!this.nationalParkRepository.CreateNationalPark(nationalParkobje))
            {
               ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkobje.name}");

               return StatusCode(500, ModelState);
            }

            return Ok();
        }

    }
}
