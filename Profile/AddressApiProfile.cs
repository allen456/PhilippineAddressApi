using AutoMapper;
using PhilippineAddressApi.Dtos;
using PhilippineAddressApi.Models;

namespace PhilippineAddressApi.Profiles
{
    public class AddressApiProfile : Profile
    {
        public AddressApiProfile()
        {
            CreateMap<Barangay, BarangayReadDto>();
            CreateMap<BarangayCreateDto, Barangay>();
            CreateMap<BarangayUpdateDto, Barangay>();
            CreateMap<Barangay, BarangayUpdateDto>();
            
            CreateMap<CityMunicipality, CityMunicipalityReadDto>();
            CreateMap<CityMunicipalityCreateDto, CityMunicipality>();
            CreateMap<CityMunicipalityUpdateDto, CityMunicipality>();
            CreateMap<CityMunicipality, CityMunicipalityUpdateDto>();
            
            CreateMap<ProvinceDistrict, ProvinceDistrictReadDto>();
            CreateMap<ProvinceDistrictCreateDto, ProvinceDistrict>();
            CreateMap<ProvinceDistrictUpdateDto, ProvinceDistrict>();
            CreateMap<ProvinceDistrict, ProvinceDistrictUpdateDto>();
            
            CreateMap<Region, RegionReadDto>();
            CreateMap<RegionCreateDto, Region>();
            CreateMap<RegionUpdateDto, Region>();
            CreateMap<Region, RegionUpdateDto>();
        }
    }
}