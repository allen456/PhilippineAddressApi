using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace PhilippineAddressApi.Dtos
{
    public class RegionCreateDto
    {
        [Required]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
    }
}