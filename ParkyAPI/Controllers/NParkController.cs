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




    [Route("api/v{version:apiVersion}/Npark")]
    //[Route("api/[controller]")]  //access using API/Controller name
    // API forward slash the controller name the controller name is national parks.
    //So if you type API forward slash national parks that will be the default road for all of the action methods here and you can see it inherits from controller base.You can change it to inherit just from the controller.

    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]  //bunding api controlleer call within associate document/specification.
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NParkController : Controller
    {
        private INParkRepository _nParkRepository;
        private readonly IMapper _mapper;

        public NParkController(INParkRepository nParkRepository, IMapper mapper)
        {
            _nParkRepository = nParkRepository;
            _mapper = mapper;
        }





        /// <summary>
        /// Get list of all national parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<NParkDto>))]
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




        /// <summary>
        /// Get Individual national park
        /// </summary>
        /// <param name="NParkId">the id of national park</param>
        /// <returns></returns>

        [HttpGet("{NParkId:int}",Name ="GetNationalPark")]// routingS
        [ProducesResponseType(200, Type = typeof(List<NParkDto>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

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

       



        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<NParkDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NParkDto nParkDto)
        {
            if (nParkDto == null)
            {
                return BadRequest(ModelState); //model state typically contains all of the errors if any are encountered. 
            }

            if (_nParkRepository.NparkExist(nParkDto.Name))   //model validation.
            {
               //next what they will do is check...if this is a duplicate entry.
                ModelState.AddModelError("", "National park with same name already exist");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newparkobj = _mapper.Map<NPark>(nParkDto);  //here we are converting our incoming dto object into a our domain model class

            if (!_nParkRepository.CreateNpark(newparkobj))  //model validation.
            {
                ModelState.AddModelError("", $"something went wrong for record {newparkobj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark",new { version=HttpContext.GetRequestedApiVersion().ToString(),NParkId= newparkobj.Id}, newparkobj);
            //we used createdatroute rather than ok()...postman now showing 201 created after posting a data...!!
        }




        [HttpPatch("{NParkId:int}", Name = "UpdateNationalPark")]// routingS
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNpark(int NParkId, [FromBody] NParkDto nParkDto)
        {
            if (nParkDto == null || NParkId != nParkDto.Id)
            {
                return BadRequest(ModelState); //model state typically contains all of the errors if any are encountered. 
            }


            var newparkobj = _mapper.Map<NPark>(nParkDto);  //here we are converting our incoming dto object into a our domain model class

            if (!_nParkRepository.UpdateNpark(newparkobj))  //model validation.
            {
                ModelState.AddModelError("", $"something went wrong while updating a record for {newparkobj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{NparkId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int NparkId)
        {


            if (!_nParkRepository.NparkExist(NparkId)){
                return BadRequest(ModelState);
            }

            var getNationalParkToDelete = _nParkRepository.GetPark(NparkId);

            if (!_nParkRepository.DeleteNpark(getNationalParkToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong while Deleteting a record {getNationalParkToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}