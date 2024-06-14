using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsDriver
    {
        public int LicenseID { get; set; }
        public string note {  get; set; }
        public int idApp {  get; set; }

        private clsDriver(int licenseid,string note,int idApp) { 
            this.LicenseID = licenseid;
            this.note = note;
            this.idApp = idApp;
        }

        public static DataTable GetAllDriver()
        {
            return clsDriverData.GetAllDriver();
        }


    }
}
