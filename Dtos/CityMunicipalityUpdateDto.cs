using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhilippineAddressApi.Dtos
{
    public class CityMunicipalityUpdateDto
    {
        [Required]
        public string CityMunicipalityCode { get; set; }
        [Required]
        public string CityMunicipalityName { get; set; }
        public string CityMunicipalityOldname { get; set; }
        [Required]
        public Guid ProvinceDistrictId { get; set; } 
    }
}