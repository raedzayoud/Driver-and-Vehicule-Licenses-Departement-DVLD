using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsReleaseApp
    {
        public enum Mode { Add = 1, Update = 2 };
        Mode _mode;
        public int DetainID { get; set; }
        public bool isRelease { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseAppID {  get; set; }
       
       public clsReleaseApp() {
            DetainID = -1;
            isRelease = false;
            ReleaseDate= DateTime.Now;
            _mode= Mode.Add;
            ReleaseAppID = -1;
       }

        private bool AddNewReleaseApp()
        {
            ReleaseAppID = ReleaseData.InsertData(DetainID, isRelease, ReleaseDate);
            return ReleaseAppID != -1;
        }

        public bool Save()
        {
            try
            {
                switch (_mode)
                {
                    case Mode.Add:
                        if (AddNewReleaseApp())
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

        public static DataTable GetReleaseofDetain(int DetainID)
        {
            return ReleaseData.GetAllRelease(DetainID);
        }



    }
}
