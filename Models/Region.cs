using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PhilippineAddressApi.Models
{
    public class Region
    {
        [Key]
        public Guid RegionId { get; set;}
        [Required]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
        public ICollection<ProvinceDistrict> ProvincesDistricts { get; set; } = new List<ProvinceDistrict>();
    }
}