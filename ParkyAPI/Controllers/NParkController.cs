using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]  //access using API/Controller name

    // API forward slash the controller name the controller name is national parks.
    //So if you type API forward slash national parks that will be the default road for all of the action methods here and you can see it inherits from controller base.You can change it to inherit just from the controller.


    [ApiController]
    public class NParkController : Controller
    {
        private INParkRepository _nParkRepository;
        private readonly IMapper _mapper;

        public NParkController(INParkRepository nParkRepository, IMapper mapper)
        {
            _nParkRepository = nParkRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nparkdata = _nParkRepository.GetAllNParks();
            //we should never exposed our domain model to outiside word...
            // we should only exposed DTOS(another approach)using mapper.

            var objdto = new List<NParkDto>();

            foreach (var obj in nparkdata)
            {
                objdto.Add(_mapper.Map<NParkDto>(obj));  //here we are mapping our domain class


            }
            //return Ok(nparkdata);    //here we are returining list from our domain class models.nparks.

            return Ok(objdto);   //here we are returining list from our dto object.
        }



        //get single National Park
        [HttpGet("{NParkId:int}")]// routingS
        public IActionResult GetSingleNationalParks(int NParkId)
        {

            var singlepark = _nParkRepository.GetPark(NParkId);
            if (singlepark == null)
            {
                return NotFound();
            }

            var singleparkDto = _mapper.Map<NParkDto>(singlepark);
            return Ok(singleparkDto);

        }
         



    }
}