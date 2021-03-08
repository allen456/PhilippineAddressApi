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
    //api/barangays
    [Route("api/[controller]")]
    [ApiController]
    public class BarangaysController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public BarangaysController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //GET api/barangays
        [HttpGet]
        public ActionResult <IEnumerable<BarangayReadDto>> GetAllBarangays()
        {
            var returndata = _repo.GetAllBarangays();
            return Ok(_mapper.Map<IEnumerable<BarangayReadDto>>(returndata)); 
        }

        //GET api/barangays/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}", Name="GetBarangayById")]
        public ActionResult <BarangayReadDto> GetBarangayById(Guid id)
        {
            var returndata = _repo.GetBarangayById(id);
            return Ok(_mapper.Map<BarangayReadDto>(returndata));
        }
        
        //POST api/barangays
        [HttpPost]
        public ActionResult <BarangayReadDto> CreateBarangay(BarangayCreateDto barangayCreateDto)
        {
            var receievedata = _mapper.Map<Barangay>(barangayCreateDto);
            _repo.CreateBarangay(receievedata);
            _repo.SaveChanges();
            var newdata = _mapper.Map<BarangayReadDto>(receievedata);
            return CreatedAtRoute(nameof(GetBarangayById), new {Id = newdata.BarangayId}, newdata);
        }

        //PUT api/barangays/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public ActionResult UpdateBarangay(Guid id, BarangayUpdateDto barangayUpdateDto)
        {
            var existingdata = _repo.GetBarangayById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _mapper.Map(barangayUpdateDto, existingdata);
            _repo.UpdateBarangay(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
        
        //PATCH api/barangays/00000000-0000-0000-0000-000000000000
        [HttpPatch("{id}")]
        public ActionResult PartialBarangayUpdate(Guid id, JsonPatchDocument<BarangayUpdateDto> patchDoc)
        {
            var existingdata = _repo.GetBarangayById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            var barangayToPatch = _mapper.Map<BarangayUpdateDto>(existingdata);
            patchDoc.ApplyTo(barangayToPatch, ModelState);
            if(!TryValidateModel(barangayToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(barangayToPatch, existingdata);
            _repo.UpdateBarangay(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/barangays/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public ActionResult DeleteBarangay(Guid id)
        {
            var existingdata = _repo.GetBarangayById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _repo.DeleteBarangay(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}