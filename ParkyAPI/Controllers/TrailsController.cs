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
    [Route("api/Trails")]  //access using API/Controller name

    // API forward slash the controller name the controller name is national parks.
    //So if you type API forward slash trails that will be the default road for all of the action methods here and you can see it inherits from controller base.You can change it to inherit just from the controller.

    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : Controller
    {
        private ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailsController(IMapper mapper,ITrailRepository trailRepository)
        { 
            _mapper = mapper;
            _trailRepository = trailRepository;
        }





        /// <summary>
        /// Get list of all Trails
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<TrailDto>))]
        public IActionResult GetAllTrails()
        {
            var Traildata = _trailRepository.GetTrails();
            //we should never exposed our domain model to outiside word...
            // we should only exposed DTOS(another approach)using mapper.

            var objdto = new List<TrailDto>();

            foreach (var obj in Traildata)
            {
                objdto.Add(_mapper.Map<TrailDto>(obj));  //here we are mapping our domain class


            }
            //return Ok(Traildata);    //here we are returining list from our domain class models.Trails.

            return Ok(objdto);   //here we are returining list from our dto object.
        }




        /// <summary>
        /// Get Individual Trail
        /// </summary>
        /// <param name="TrailId">the id of trail</param>
        /// <returns></returns>

        [HttpGet("{TrailId:int}",Name ="GetTrail")]// routingS
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetSingleTrail(int TrailId)
        {

            var singletrail = _trailRepository.GetTrail(TrailId);
            if (singletrail == null)
            {
                return NotFound();
            }

            var singletrailDto = _mapper.Map<TrailDto>(singletrail);
            return Ok(singletrailDto);

        }

       



        [HttpPost]
        [ProducesResponseType(201, Type = typeof(List<TrailDto>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailCreate)
        {
            if (trailCreate == null)
            {
                return BadRequest(ModelState); //model state typically contains all of the errors if any are encountered. 
            }

            if (_trailRepository.TrailExists(trailCreate.Name))   //model validation.
            {
               //next what they will do is check...if this is a duplicate entry.
                ModelState.AddModelError("", "Trail with same name already exist");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newtrailobj = _mapper.Map<Trail>(trailCreate);  //here we are converting our incoming dto object into a our domain model class trail

            if (!_trailRepository.CreateTrail(newtrailobj))  //model validation.
            {
                ModelState.AddModelError("", $"something went wrong for record {newtrailobj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail",new { TrailId= newtrailobj.Id}, newtrailobj);
            //we used createdatroute rather than ok()...postman now showing 201 created after posting a data...!!
        }




        [HttpPatch("{TrailId:int}", Name = "UpdateTrail")]// routingS
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int TrailId, [FromBody] TrailupdateDto trailupdateDto)
        {
            if (trailupdateDto == null || TrailId != trailupdateDto.Id)
            {
                return BadRequest(ModelState); //model state typically contains all of the errors if any are encountered. 
            }


            var newtrailobj = _mapper.Map<Trail>(trailupdateDto);  //here we are converting our incoming dto object into a our domain model class

            if (!_trailRepository.UpdateTrail(newtrailobj))  //model validation.
            {
                ModelState.AddModelError("", $"something went wrong while updating a record for {newtrailobj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{TrailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int TrailId)
        {


            if (!_trailRepository.TrailExists(TrailId)){
                return BadRequest(ModelState);
            }

            var getTrailToDelete = _trailRepository.GetTrail(TrailId);

            if (!_trailRepository.DeleteTrail(getTrailToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong while Deleteting a record {getTrailToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}