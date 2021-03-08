using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhilippineAddressApi.Models
{
    public class Barangay
    {
        [Key]
        public Guid BarangayId { get; set;}
        [Required]
        public string BarangayCode { get; set; }
        [Required]
        public string BarangayName { get; set; }
        public string BarangayOldname { get; set; }
        public Guid CityMunicipalityId { get; set; } 
        [ForeignKey("CityMunicipalityId")] 
        public CityMunicipality CityMunicipality { get; set; } 
    }
}