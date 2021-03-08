using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PhilippineAddressApi.Library
{
    public static class ShareLib
    {
        public static bool CheckIfBarangay(string code)
        {
            return(code.Substring(6) != "000" && code.Substring(4) != "00000" && code.Substring(2) != "0000000");
        }
        public static bool CheckIfCityMunicipalitySub(string code)
        {
            return(code.Substring(6) == "000" && code.Substring(4) != "00000" && code.Substring(2) != "0000000");
        }
        public static bool CheckIfProviceDistrict(string code)
        {
            return(code.Substring(4) == "00000" && code.Substring(2) != "0000000");
        }
        public static bool CheckIfRegion(string code)
        {
            return(code.Substring(2) == "0000000");
        }
    }
}
