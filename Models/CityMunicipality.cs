using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhilippineAddressApi.Models
{
    public class CityMunicipality
    {
        [Key]
        public Guid CityMunicipalityId { get; set;}
        [Required]
        public string CityMunicipalityCode { get; set; }
        [Required]
        public string CityMunicipalityName { get; set; }
        public string CityMunicipalityOldname { get; set; }
        public ICollection<Barangay> Barangays { get; set; } = new List<Barangay>();
        public Guid ProvinceDistrictId { get; set; } 
        [ForeignKey("ProvinceDistrictId")] 
        public ProvinceDistrict ProvinceDistrict { get; set; } 
    }
}