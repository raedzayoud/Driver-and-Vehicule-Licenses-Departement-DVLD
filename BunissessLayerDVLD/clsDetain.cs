using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsDetain
    {
        public enum Mode { Add = 1, Update = 2 };
        Mode _mode;
        public int DetainID {  get; set; }
        public int LicenseID { get; set; }
        public DateTime DebutDate { get; set; }
        public int FineFees { get; set; }

        public clsDetain()
        {
            LicenseID = -1;
            DebutDate=DateTime.Now;
            FineFees = -1;
            _mode=Mode.Add;
        }

        private bool AddNewDetain()
        {
            DetainID =DetainData.InsertData(LicenseID,DebutDate,FineFees);
            return LicenseID != -1;
        }

        public bool Save()
        {
            try
            {
                switch (_mode)
                {
                    case Mode.Add:
                        if (AddNewDetain())
                        {
                            _mode = Mode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        // Handle unexpected mode values
                        Console.WriteLine("Unsupported mode: " + _mode);
                        return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public static DataTable GetInfoDetainLicense(int idLicense)
        {
            return DetainData.GetInfoDetainLicense(idLicense);
        }

        public static DataTable GetAllDetain()
        {
            return DetainData.GetAllDetain();
        }

        public static bool IsFoundActive(int idLicense)
        {
            return DetainData.IsFoundActive(idLicense);
        }





    }
}
