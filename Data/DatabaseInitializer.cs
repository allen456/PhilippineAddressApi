using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PhilippineAddressApi.Library;
using PhilippineAddressApi.Models;
namespace PhilippineAddressApi.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(ApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Barangays.Any())
            {
                return;
            }

            var tempreglist = new List<Region>();
            var temprovlist = new List<ProvinceDistrict>();
            var tempcitylist = new List<CityMunicipality>();
            var tempbara = new List<Barangay>();

            using (var reader = new StreamReader("PSGC_3Q_2020_Publication.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    var psgcrow = reader.ReadLine().Split(","); 
                    var regcode = psgcrow[0].Substring(0, 2) + "0000000";
                    var provdistcode = psgcrow[0].Substring(0, 4) + "00000";
                    var citymunisubmunicode = psgcrow[0].Substring(0, 6) + "000";
                    if(ShareLib.CheckIfRegion(psgcrow[0]))
                    {
                        Region newreg = new Region
                        {
                            RegionId = Guid.NewGuid(),
                            RegionCode = psgcrow[0],
                            RegionName = psgcrow[1]
                        };
                        tempreglist.Add(newreg);
                    }
                    if(ShareLib.CheckIfProviceDistrict(psgcrow[0]))
                    {
                        ProvinceDistrict newprov = new ProvinceDistrict
                        {
                            ProvinceDistrictId = Guid.NewGuid(),
                            ProvinceDistrictCode = psgcrow[0],
                            ProvinceDistrictName = psgcrow[1],
                            ProvinceDistrictOldName = psgcrow[2],
                            Region = tempreglist.Where(w => w.RegionCode == regcode).First()
                        };
                        temprovlist.Add(newprov);
                    }
                    if(ShareLib.CheckIfCityMunicipalitySub(psgcrow[0]))
                    {
                        CityMunicipality newcitmun = new CityMunicipality
                        {
                            CityMunicipalityId = Guid.NewGuid(),
                            CityMunicipalityCode = psgcrow[0],
                            CityMunicipalityName = psgcrow[1],
                            CityMunicipalityOldname = psgcrow[2],
                            ProvinceDistrict = temprovlist.Where(w => w.ProvinceDistrictCode == provdistcode).First()
                        };
                        tempcitylist.Add(newcitmun);
                    }
                    if(ShareLib.CheckIfBarangay(psgcrow[0]))
                    {
                        Barangay newbrg = new Barangay
                        {
                            BarangayId = Guid.NewGuid(),
                            BarangayCode = psgcrow[0],
                            BarangayName = psgcrow[1],
                            BarangayOldname = psgcrow[2],
                            CityMunicipality = tempcitylist.Where(w => w.CityMunicipalityCode == citymunisubmunicode).First()
                        };
                        tempbara.Add(newbrg);
                    }
                }
            }
            context.Regions.AddRange(tempreglist);
            context.ProvincesDistricts.AddRange(temprovlist);
            context.CitiesMunicipalities.AddRange(tempcitylist);
            context.Barangays.AddRange(tempbara);
            Console.WriteLine("CSV Imported");
            context.SaveChanges();
        }
    }
}