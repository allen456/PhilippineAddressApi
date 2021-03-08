using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhilippineAddressApi.Models;

namespace PhilippineAddressApi.Dtos
{
    public class ProvinceDistrictReadDto
    {
        public Guid ProvinceDistrictId { get; set;}
        public string ProvinceDistrictCode { get; set; }
        public string ProvinceDistrictName { get; set; }
        public string ProvinceDistrictOldName { get; set; }
        public ICollection<CityMunicipality> CitiesMunicipalities { get; set; } = new List<CityMunicipality>();
        public Guid RegionId { get; set; } 
    }
}