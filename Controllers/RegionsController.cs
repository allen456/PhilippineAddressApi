using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhilippineAddressApi.Models;
using PhilippineAddressApi.Data;
using AutoMapper;
using PhilippineAddressApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Cors;

namespace PhilippineAddressApi.Controllers
{
    //api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public RegionsController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //GET api/regions
        [HttpGet]
        public ActionResult <IEnumerable<RegionReadDto>> GetAllRegions()
        {
            var returndata = _repo.GetAllRegions();
            return Ok(_mapper.Map<IEnumerable<RegionReadDto>>(returndata)); 
        }

        //GET api/regions/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}", Name="GetRegionById")]
        public ActionResult <RegionReadDto> GetRegionById(Guid id)
        {
            var returndata = _repo.GetRegionById(id);
            return Ok(_mapper.Map<RegionReadDto>(returndata));
        }
        
        //POST api/regions
        [HttpPost]
        public ActionResult <RegionReadDto> CreateRegion(RegionCreateDto regionCreateDto)
        {
            var receievedata = _mapper.Map<Region>(regionCreateDto);
            _repo.CreateRegion(receievedata);
            _repo.SaveChanges();
            var newdata = _mapper.Map<RegionReadDto>(receievedata);
            return CreatedAtRoute(nameof(GetRegionById), new {Id = newdata.RegionId}, newdata);
        }

        //PUT api/regions/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public ActionResult UpdateRegion(Guid id, RegionUpdateDto regionUpdateDto)
        {
            var existingdata = _repo.GetRegionById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _mapper.Map(regionUpdateDto, existingdata);
            _repo.UpdateRegion(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
        
        //PATCH api/regions/00000000-0000-0000-0000-000000000000
        [HttpPatch("{id}")]
        public ActionResult PartialRegionUpdate(Guid id, JsonPatchDocument<RegionUpdateDto> patchDoc)
        {
            var existingdata = _repo.GetRegionById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            var regionToPatch = _mapper.Map<RegionUpdateDto>(existingdata);
            patchDoc.ApplyTo(regionToPatch, ModelState);
            if(!TryValidateModel(regionToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(regionToPatch, existingdata);
            _repo.UpdateRegion(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/regions/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public ActionResult DeleteRegion(Guid id)
        {
            var existingdata = _repo.GetRegionById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _repo.DeleteRegion(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}