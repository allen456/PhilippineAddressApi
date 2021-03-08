using PhilippineAddressApi.Models;
using Microsoft.EntityFrameworkCore;
namespace PhilippineAddressApi.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<CityMunicipality> CitiesMunicipalities { get; set; }
        public DbSet<ProvinceDistrict> ProvincesDistricts { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}