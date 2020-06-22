using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    //[Route("api/[controller]")]  //access using API/Controller nameq

    [Route("api/v{version:apiVersion}/Npark")]
    [ApiVersion("2.0")]

    // API forward slash the controller name the controller name is national parks.
    //So if you type API forward slash national parks that will be the default road for all of the action methods here and you can see it inherits from controller base.You can change it to inherit just from the controller.

    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]  //bunding api controlleer call within associate document/specification.
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NParkV2Controller : Controller
    {
        private INParkRepository _nParkRepository;
        private readonly IMapper _mapper;

        public NParkV2Controller(INParkRepository nParkRepository, IMapper mapper)
        {
            _nParkRepository = nParkRepository;
            _mapper = mapper;
        }




        /// <summary>
        /// Get list of all national parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NParkDto>))]
        public IActionResult GetNationalParks()
        {
            var obj = _nParkRepository.GetAllNParks().FirstOrDefault();
            return Ok(_mapper.Map<NParkDto>(obj));
        }

    }

}