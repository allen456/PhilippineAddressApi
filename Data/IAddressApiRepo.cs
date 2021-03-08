using System;
using System.Collections.Generic;
using PhilippineAddressApi.Models;
namespace PhilippineAddressApi.Data
{
    public interface IAddressApiRepo
    {
        bool SaveChanges();
        void  Import(List<Region> reg, List<ProvinceDistrict> prv, List<CityMunicipality> cmy, List<Barangay> brg);
        IEnumerable<Region> Export();

        IEnumerable<Region> GetAllRegions();
        Region GetRegionById(Guid id);
        void CreateRegion(Region reg);
        void UpdateRegion(Region reg);
        void DeleteRegion(Region reg);
        
        IEnumerable<ProvinceDistrict> GetAllProvincesDistricts();
        ProvinceDistrict GetProvinceDistrictById(Guid id);
        void CreateProvinceDistrict(ProvinceDistrict prv);
        void UpdateProvinceDistrict(ProvinceDistrict prv);
        void DeleteProvinceDistrict(ProvinceDistrict prv);

        
        IEnumerable<CityMunicipality> GetAllCitiesMunicipalities();
        CityMunicipality GetCityMunicipalityById(Guid id);
        void CreateCityMunicipality(CityMunicipality cmy);
        void UpdateCityMunicipality(CityMunicipality cmy);
        void DeleteCityMunicipality(CityMunicipality cmy);

        
        IEnumerable<Barangay> GetAllBarangays();
        Barangay GetBarangayById(Guid id);
        void CreateBarangay(Barangay brg);
        void UpdateBarangay(Barangay brg);
        void DeleteBarangay(Barangay brg);
    }
}