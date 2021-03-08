using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhilippineAddressApi.Models
{
    public class ProvinceDistrict
    {
        [Key]
        public Guid ProvinceDistrictId { get; set;}
        [Required]
        public string ProvinceDistrictCode { get; set; }
        [Required]
        public string ProvinceDistrictName { get; set; }
        public string ProvinceDistrictOldName { get; set; }
        public ICollection<CityMunicipality> CitiesMunicipalities { get; set; } = new List<CityMunicipality>();
        public Guid RegionId { get; set; } 
        [ForeignKey("RegionId")] 
        public Region Region { get; set; } 
    }
}