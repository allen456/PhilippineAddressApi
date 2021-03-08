using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhilippineAddressApi.Models;

namespace PhilippineAddressApi.Dtos
{
    public class RegionReadDto
    {
        public Guid RegionId { get; set;}
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public ICollection<ProvinceDistrict> ProvincesDistricts { get; set; } = new List<ProvinceDistrict>();
    }
}