using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhilippineAddressApi.Dtos
{
    public class ProvinceDistrictUpdateDto
    {
        [Required]
        public string ProvinceDistrictCode { get; set; }
        [Required]
        public string ProvinceDistrictName { get; set; }
        public string ProvinceDistrictOldName { get; set; }
        [Required]
        public Guid RegionId { get; set; } 
    }
}