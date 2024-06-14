using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;

namespace BunissessLayerDVLD
{
    public  class clsCountry
    {
        public int IDCountries { get; set; }
        public string CountryName { get; set; }

        public clsCountry(int id,string name) { 
            IDCountries = id;
            CountryName = name;
        }
        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }

        public static bool GetIDOftheCountry(string s, ref  int id )
        {
            return clsCountriesData.GetIDOftheCountry(s, ref id);
        }
        public static clsCountry FindCountryById(int id)
        {
            string s = "";
            if(clsCountriesData.FindCountryNameByID(id, ref s))
            {
                return new clsCountry(id,s);
            }
            return new  clsCountry(id, s);
        }
    }
}
