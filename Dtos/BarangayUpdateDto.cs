using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhilippineAddressApi.Dtos
{
    public class BarangayUpdateDto
    {
        [Required]
        public string BarangayCode { get; set; }
        [Required]
        public string BarangayName { get; set; }
        public string BarangayOldname { get; set; }
        [Required]
        public Guid CityMunicipalityId { get; set; } 
    }
}