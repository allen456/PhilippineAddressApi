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
    //api/provincesdistricts
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesDistrictsController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public ProvincesDistrictsController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //GET api/provincesdistricts
        [HttpGet]
        public ActionResult <IEnumerable<ProvinceDistrictReadDto>> GetAllProvinceDistrict()
        {
            var returndata = _repo.GetAllProvincesDistricts();
            return Ok(_mapper.Map<IEnumerable<ProvinceDistrictReadDto>>(returndata)); 
        }

        //GET api/provincesdistricts/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}", Name="GetProvinceDistrictById")]
        public ActionResult <ProvinceDistrictReadDto> GetProvinceDistrictById(Guid id)
        {
            var returndata = _repo.GetProvinceDistrictById(id);
            return Ok(_mapper.Map<ProvinceDistrictReadDto>(returndata));
        }
        
        //POST api/provincesdistricts
        [HttpPost]
        public ActionResult <ProvinceDistrictReadDto> CreateProvinceDistrict(ProvinceDistrictCreateDto provinceCreateDto)
        {
            var receievedata = _mapper.Map<ProvinceDistrict>(provinceCreateDto);
            _repo.CreateProvinceDistrict(receievedata);
            _repo.SaveChanges();
            var newdata = _mapper.Map<ProvinceDistrictReadDto>(receievedata);
            return CreatedAtRoute(nameof(GetProvinceDistrictById), new {Id = newdata.ProvinceDistrictId}, newdata);
        }

        //PUT api/provincesdistricts/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public ActionResult UpdateProvinceDistrict(Guid id, ProvinceDistrictUpdateDto provinceUpdateDto)
        {
            var existingdata = _repo.GetProvinceDistrictById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _mapper.Map(provinceUpdateDto, existingdata);
            _repo.UpdateProvinceDistrict(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
        
        //PATCH api/provincesdistricts/00000000-0000-0000-0000-000000000000
        [HttpPatch("{id}")]
        public ActionResult PartialProvinceUpdate(Guid id, JsonPatchDocument<ProvinceDistrictUpdateDto> patchDoc)
        {
            var existingdata = _repo.GetProvinceDistrictById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            var provinceToPatch = _mapper.Map<ProvinceDistrictUpdateDto>(existingdata);
            patchDoc.ApplyTo(provinceToPatch, ModelState);
            if(!TryValidateModel(provinceToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(provinceToPatch, existingdata);
            _repo.UpdateProvinceDistrict(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/provincesdistricts/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public ActionResult DeleteProvinceDistrict(Guid id)
        {
            var existingdata = _repo.GetProvinceDistrictById(id);
            if(existingdata == null)
            {
                return NotFound();
            }
            _repo.DeleteProvinceDistrict(existingdata);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}