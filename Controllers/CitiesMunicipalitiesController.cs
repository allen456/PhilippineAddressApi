using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhilippineAddressApi.Models;
using PhilippineAddressApi.Data;
using AutoMapper;
using PhilippineAddressApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace PhilippineAddressApi.Controllers
{
    //api/citiesmunicipalities
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesMunicipalitiesController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public CitiesMunicipalitiesController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //GET api/citiesmunicipalities
        [HttpGet]
        public ActionResult <IEnumerable<CityMunicipalityReadDto>> GetAllCitiesMunicipalities()
        {
            var returndata = _repo.GetAllCitiesMunicipalities();
            return Ok(_mapper.Map<IEnumerable<CityMunicipalityReadDto>>(returndata)); 
        }

        //GET api/citiesmunicipalities/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}", Name="GetCityMunicipalityById")]
        public ActionResult <CityMunicipalityReadDto> GetCityMunicipalityById(Guid id)
        {
            var returndata = _repo.GetCityMunicipalityById(id);
            return Ok(_mapper.Map<CityMunicipalityReadDto>(returndata));
        }
        
        //POST api/citiesmunicipalities
        [HttpPost]
        public ActionResult <CityMunicipalityReadDto> CreateCityMunicipality(CityMunicipalityCreateDto citymunicipalityCreateDto)
        {
            var receievedata = _mapper.Map<CityMunicipality>(citymunicipalityCreateDto);
            _repo.CreateCityMunicipality(receievedata);
            _repo.SaveChanges();
            var newdata = _mapper.Map<CityMunicipalityReadDto>(receievedata);
            return CreatedAtRoute(nameof(GetCityMunicipalityById), new {Id = newdata.CityMunicipalityId}, newdata);
        }

        //PUT api/citiesmunicipalities/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public ActionResult UpdateCityMunicipality(Guid id, CityMunicipalityUpdateDto citymunicipalityUpdateDto)
        {
            var existingdata = _repo.GetCityMunicipalityById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _mapper.Map(citymunicipalityUpdateDto, existingdata);
            _repo.UpdateCityMunicipality(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
        
        //PATCH api/citiesmunicipalities/00000000-0000-0000-0000-000000000000
        [HttpPatch("{id}")]
        public ActionResult PartialCityMunicipalityUpdate(Guid id, JsonPatchDocument<CityMunicipalityUpdateDto> patchDoc)
        {
            var existingdata = _repo.GetCityMunicipalityById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            var citymunicipalityToPatch = _mapper.Map<CityMunicipalityUpdateDto>(existingdata);
            patchDoc.ApplyTo(citymunicipalityToPatch, ModelState);
            if(!TryValidateModel(citymunicipalityToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(citymunicipalityToPatch, existingdata);
            _repo.UpdateCityMunicipality(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/citiesmunicipalities/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public ActionResult DeleteCityMunicipality(Guid id)
        {
            var existingdata = _repo.GetCityMunicipalityById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _repo.DeleteCityMunicipality(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}