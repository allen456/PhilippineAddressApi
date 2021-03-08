using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhilippineAddressApi.Models;

namespace PhilippineAddressApi.Dtos
{
    public class BarangayReadDto
    {
        public Guid BarangayId { get; set;}
        public string BarangayCode { get; set; }
        public string BarangayName { get; set; }
        public string BarangayOldname { get; set; }
        public Guid CityMunicipalityId { get; set; } 
    }
}