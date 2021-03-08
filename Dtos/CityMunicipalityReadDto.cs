using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhilippineAddressApi.Models;

namespace PhilippineAddressApi.Dtos
{
    public class CityMunicipalityReadDto
    {
        public Guid CityMunicipalityId { get; set;}
        public string CityMunicipalityCode { get; set; }
        public string CityMunicipalityName { get; set; }
        public string CityMunicipalityOldname { get; set; }
        public ICollection<Barangay> Barangays { get; set; } = new List<Barangay>();
        public Guid ProvinceDistrictId { get; set; } 
    }
}