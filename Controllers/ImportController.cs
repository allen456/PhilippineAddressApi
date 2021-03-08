using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhilippineAddressApi.Models;
using PhilippineAddressApi.Data;
using AutoMapper;
using PhilippineAddressApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using System.IO;
using PhilippineAddressApi.Library;
using Microsoft.AspNetCore.Cors;

namespace PhilippineAddressApi.Controllers
{
    //api/import
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public ImportController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        
        //POST api/import
        [HttpPost]
        public ActionResult UploadFile(IFormFile psgc)
        {
            var tempreglist = new List<Region>();
            var temprovlist = new List<ProvinceDistrict>();
            var tempcitylist = new List<CityMunicipality>();
            var tempbara = new List<Barangay>();
            using (var reader = new StreamReader(psgc.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var psgcrow = reader.ReadLine().Split(","); 
                    if(psgcrow.Length != 3)
                    {
                        return BadRequest();
                    }
                    if(psgcrow[0].Length != 9)
                    {
                        return BadRequest();
                    }
                    var regcode = psgcrow[0].Substring(0, 2) + "0000000";
                    var provdistcode = psgcrow[0].Substring(0, 4) + "00000";
                    var citymunisubmunicode = psgcrow[0].Substring(0, 6) + "000";
                    if(ShareLib.CheckIfRegion(psgcrow[0]))
                    {
                        Region newreg = new Region
                        {
                            RegionId = Guid.NewGuid(),
                            RegionCode = psgcrow[0],
                            RegionName = psgcrow[1]
                        };
                        tempreglist.Add(newreg);
                    }
                    if(ShareLib.CheckIfProviceDistrict(psgcrow[0]))
                    {
                        ProvinceDistrict newprov = new ProvinceDistrict
                        {
                            ProvinceDistrictId = Guid.NewGuid(),
                            ProvinceDistrictCode = psgcrow[0],
                            ProvinceDistrictName = psgcrow[1],
                            ProvinceDistrictOldName = psgcrow[2],
                            Region = tempreglist.Where(w => w.RegionCode == regcode).First()
                        };
                        temprovlist.Add(newprov);
                    }
                    if(ShareLib.CheckIfCityMunicipalitySub(psgcrow[0]))
                    {
                        CityMunicipality newcitmun = new CityMunicipality
                        {
                            CityMunicipalityId = Guid.NewGuid(),
                            CityMunicipalityCode = psgcrow[0],
                            CityMunicipalityName = psgcrow[1],
                            CityMunicipalityOldname = psgcrow[2],
                            ProvinceDistrict = temprovlist.Where(w => w.ProvinceDistrictCode == provdistcode).First()
                        };
                        tempcitylist.Add(newcitmun);
                    }
                    if(ShareLib.CheckIfBarangay(psgcrow[0]))
                    {
                        Barangay newbrg = new Barangay
                        {
                            BarangayId = Guid.NewGuid(),
                            BarangayCode = psgcrow[0],
                            BarangayName = psgcrow[1],
                            BarangayOldname = psgcrow[2],
                            CityMunicipality = tempcitylist.Where(w => w.CityMunicipalityCode == citymunisubmunicode).First(),
                            CityMunicipalityId = tempcitylist.Where(w => w.CityMunicipalityCode == citymunisubmunicode).First().CityMunicipalityId
                        };
                        tempbara.Add(newbrg);
                    }
                }
            }
            _repo.Import(tempreglist, temprovlist, tempcitylist, tempbara);
            _repo.SaveChanges();
            return Ok();
        }

    }
}