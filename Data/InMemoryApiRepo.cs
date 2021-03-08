using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhilippineAddressApi.Models;
namespace PhilippineAddressApi.Data
{
    public class InMemoryApiRepo : IAddressApiRepo
    {
        private readonly ApiContext _context;

        public InMemoryApiRepo(ApiContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Import(List<Region> reg, List<ProvinceDistrict> prv, List<CityMunicipality> cmy, List<Barangay> brg){
            _context.Barangays.RemoveRange(_context.Barangays.ToList());
            _context.CitiesMunicipalities.RemoveRange(_context.CitiesMunicipalities.ToList());
            _context.ProvincesDistricts.RemoveRange(_context.ProvincesDistricts.ToList());
            _context.Regions.RemoveRange(_context.Regions.ToList());
            _context.SaveChanges();
            _context.Regions.AddRange(reg);
            _context.ProvincesDistricts.AddRange(prv);
            _context.CitiesMunicipalities.AddRange(cmy);
            _context.Barangays.AddRange(brg);
        }

        public IEnumerable<Region> Export()
        {
            return _context.Regions
            .Include(w => w.ProvincesDistricts)
            .ThenInclude(w => w.CitiesMunicipalities)
            .ThenInclude(w => w.Barangays)
            .Select(w => new Region
            {
                RegionId = w.RegionId,
                RegionCode = w.RegionCode,
                RegionName = w.RegionName,
                ProvincesDistricts = w.ProvincesDistricts.OrderBy(e => e.ProvinceDistrictCode).Select(e => new ProvinceDistrict
                {
                    ProvinceDistrictId = e.ProvinceDistrictId,
                    ProvinceDistrictCode = e.ProvinceDistrictCode,
                    ProvinceDistrictName = e.ProvinceDistrictName,
                    ProvinceDistrictOldName = e.ProvinceDistrictOldName,
                    RegionId = e.RegionId,
                    CitiesMunicipalities = e.CitiesMunicipalities.OrderBy(e => e.CityMunicipalityCode).Select(z => new CityMunicipality
                    {
                        CityMunicipalityId = z.CityMunicipalityId,
                        CityMunicipalityCode = z.CityMunicipalityCode,
                        CityMunicipalityName = z.CityMunicipalityName,
                        CityMunicipalityOldname = z.CityMunicipalityOldname,
                        ProvinceDistrictId = z.ProvinceDistrictId,
                        Barangays = z.Barangays.OrderBy(e => e.BarangayCode).Select(t => new Barangay
                        {
                            BarangayId = t.BarangayId,
                            BarangayCode = t.BarangayCode,
                            BarangayName = t.BarangayName,
                            BarangayOldname = t.BarangayOldname,
                            CityMunicipalityId = t.CityMunicipalityId
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public void CreateBarangay(Barangay brg)
        {
            if(brg == null)
            {
                throw new ArgumentNullException(nameof(brg));
            }
            brg.BarangayId = Guid.NewGuid();
            _context.Barangays.Add(brg);
        }

        public void CreateCityMunicipality(CityMunicipality cmy)
        {
            if(cmy == null)
            {
                throw new ArgumentNullException(nameof(cmy));
            }
            cmy.CityMunicipalityId = Guid.NewGuid();
            _context.CitiesMunicipalities.Add(cmy);
        }

        public void CreateProvinceDistrict(ProvinceDistrict prv)
        {
            if(prv == null)
            {
                throw new ArgumentNullException(nameof(prv));
            }
            prv.ProvinceDistrictId = Guid.NewGuid();
            _context.ProvincesDistricts.Add(prv);
        }

        public void CreateRegion(Region reg)
        {
            if(reg == null)
            {
                throw new ArgumentNullException(nameof(reg));
            }
            reg.RegionId = Guid.NewGuid();
            _context.Regions.Add(reg);
        }

        public void DeleteBarangay(Barangay brg)
        {
            if(brg == null)
            {
                throw new ArgumentNullException(nameof(brg));
            }
            _context.Barangays.Remove(brg);
        }

        public void DeleteCityMunicipality(CityMunicipality cmy)
        {
            if(cmy == null)
            {
                throw new ArgumentNullException(nameof(cmy));
            }
            _context.CitiesMunicipalities.Remove(cmy);
        }

        public void DeleteProvinceDistrict(ProvinceDistrict prv)
        {
            if(prv == null)
            {
                throw new ArgumentNullException(nameof(prv));
            }
            _context.ProvincesDistricts.Remove(prv);
        }

        public void DeleteRegion(Region reg)
        {
            if(reg == null)
            {
                throw new ArgumentNullException(nameof(reg));
            }
            _context.Regions.Remove(reg);
        }

        public IEnumerable<Barangay> GetAllBarangays()
        {
            return _context.Barangays
            .Select(w => new Barangay
            {
                BarangayId = w.BarangayId,
                BarangayCode = w.BarangayCode,
                BarangayName = w.BarangayName,
                BarangayOldname = w.BarangayOldname,
                CityMunicipalityId = w.CityMunicipalityId
            })
            .ToList();
        }

        public IEnumerable<Barangay> GetAllBarangays(Guid Id)
        {
            return _context.Barangays
            .Where(w => w.CityMunicipalityId == Id)
            .Select(w => new Barangay
            {
                BarangayId = w.BarangayId,
                BarangayCode = w.BarangayCode,
                BarangayName = w.BarangayName,
                BarangayOldname = w.BarangayOldname,
                CityMunicipalityId = w.CityMunicipalityId
            })
            .ToList();
        }

        public IEnumerable<CityMunicipality> GetAllCitiesMunicipalities()
        {
            return _context.CitiesMunicipalities
            .Select(w => new CityMunicipality
            {
                CityMunicipalityId = w.CityMunicipalityId,
                CityMunicipalityCode = w.CityMunicipalityCode,
                CityMunicipalityName = w.CityMunicipalityName,
                CityMunicipalityOldname = w.CityMunicipalityOldname,
                ProvinceDistrictId = w.ProvinceDistrictId
            })
            .ToList();
        }

        public IEnumerable<CityMunicipality> GetAllCitiesMunicipalities(Guid Id)
        {
            return _context.CitiesMunicipalities
            .Where(w => w.ProvinceDistrictId == Id)
            .Select(w => new CityMunicipality
            {
                CityMunicipalityId = w.CityMunicipalityId,
                CityMunicipalityCode = w.CityMunicipalityCode,
                CityMunicipalityName = w.CityMunicipalityName,
                CityMunicipalityOldname = w.CityMunicipalityOldname,
                ProvinceDistrictId = w.ProvinceDistrictId
            })
            .ToList();
        }

        public IEnumerable<ProvinceDistrict> GetAllProvincesDistricts()
            {
                return _context.ProvincesDistricts
                .Select(w => new ProvinceDistrict
                {
                    ProvinceDistrictId = w.ProvinceDistrictId,
                    ProvinceDistrictCode = w.ProvinceDistrictCode,
                    ProvinceDistrictName = w.ProvinceDistrictName,
                    ProvinceDistrictOldName = w.ProvinceDistrictOldName,
                    RegionId = w.RegionId
                })
                .ToList();
        }

        public IEnumerable<ProvinceDistrict> GetAllProvincesDistricts(Guid Id)
        {
            return _context.ProvincesDistricts
            .Where(w => w.RegionId == Id)
            .Select(w => new ProvinceDistrict
            {
                ProvinceDistrictId = w.ProvinceDistrictId,
                ProvinceDistrictCode = w.ProvinceDistrictCode,
                ProvinceDistrictName = w.ProvinceDistrictName,
                ProvinceDistrictOldName = w.ProvinceDistrictOldName,
                RegionId = w.RegionId
            })
            .ToList();
        }

        public IEnumerable<Region> GetAllRegions()
        {
            return _context.Regions
            .Select(w => new Region
            {
                RegionId = w.RegionId,
                RegionCode = w.RegionCode,
                RegionName = w.RegionName
            })
            .ToList();
        }

        public Barangay GetBarangayById(Guid id)
        {
            return _context.Barangays
            .Select(w => new Barangay
            {
                BarangayId = w.BarangayId,
                BarangayCode = w.BarangayCode,
                BarangayName = w.BarangayName,
                BarangayOldname = w.BarangayOldname,
                CityMunicipalityId = w.CityMunicipalityId
            })
            .FirstOrDefault(w => w.BarangayId == id);
        }

        public CityMunicipality GetCityMunicipalityById(Guid id)
        {
            return _context.CitiesMunicipalities
            .Include(w => w.Barangays)
            .Select(w => new CityMunicipality
            {
                CityMunicipalityId = w.CityMunicipalityId,
                CityMunicipalityCode = w.CityMunicipalityCode,
                CityMunicipalityName = w.CityMunicipalityName,
                CityMunicipalityOldname = w.CityMunicipalityOldname,
                ProvinceDistrictId = w.ProvinceDistrictId,
                Barangays = w.Barangays.OrderBy(e => e.BarangayCode).Select(e => new Barangay
                {
                    BarangayId = e.BarangayId,
                    BarangayCode = e.BarangayCode,
                    BarangayName = e.BarangayName,
                    BarangayOldname = e.BarangayOldname,
                    CityMunicipalityId = e.CityMunicipalityId
                })
                .ToList()
            })
            .FirstOrDefault(w => w.CityMunicipalityId == id);
        }

        public ProvinceDistrict GetProvinceDistrictById(Guid id)
        {
            return _context.ProvincesDistricts
            .Include(w => w.CitiesMunicipalities)
            .Select(w => new ProvinceDistrict
            {
                ProvinceDistrictId = w.ProvinceDistrictId,
                ProvinceDistrictCode = w.ProvinceDistrictCode,
                ProvinceDistrictName = w.ProvinceDistrictName,
                ProvinceDistrictOldName = w.ProvinceDistrictOldName,
                RegionId = w.RegionId,
                CitiesMunicipalities = w.CitiesMunicipalities.OrderBy(e => e.CityMunicipalityCode).Select(e => new CityMunicipality
                {
                    CityMunicipalityId = e.CityMunicipalityId,
                    CityMunicipalityCode = e.CityMunicipalityCode,
                    CityMunicipalityName = e.CityMunicipalityName,
                    CityMunicipalityOldname = e.CityMunicipalityOldname,
                    ProvinceDistrictId = e.ProvinceDistrictId
                })
                .ToList()
            })
            .FirstOrDefault(w => w.ProvinceDistrictId == id);
        }

        public Region GetRegionById(Guid id)
        {
            return _context.Regions
            .Include(w => w.ProvincesDistricts)
            .Select(w => new Region
            {
                RegionId = w.RegionId,
                RegionCode = w.RegionCode,
                RegionName = w.RegionName,
                ProvincesDistricts = w.ProvincesDistricts.OrderBy(e => e.ProvinceDistrictCode).Select(e => new ProvinceDistrict
                {
                    ProvinceDistrictId = e.ProvinceDistrictId,
                    ProvinceDistrictCode = e.ProvinceDistrictCode,
                    ProvinceDistrictName = e.ProvinceDistrictName,
                    ProvinceDistrictOldName = e.ProvinceDistrictOldName,
                    RegionId = e.RegionId
                })
                .ToList()
            })
            .FirstOrDefault(w => w.RegionId == id);
        }

        public void UpdateBarangay(Barangay brg)
        {
            // Done by automapper
        }

        public void UpdateCityMunicipality(CityMunicipality cmy)
        {
            // Done by automapper
        }

        public void UpdateProvinceDistrict(ProvinceDistrict prv)
        {
            // Done by automapper
        }

        public void UpdateRegion(Region reg)
        {
            // Done by automapper
        }

    }
}