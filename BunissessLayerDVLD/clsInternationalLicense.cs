using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsInternationalLicense
    {
        public static bool FoundInternationalLicense(int idApp)
        {
            return InternationLicenseData.FoundInternationalLicense(idApp);
        }

        public static int InsertInternationalLicense(int idApp,DateTime issueDate,DateTime ExpirationDate)
        {
            return InternationLicenseData.InsertInternationalLicense(idApp,issueDate,ExpirationDate);
        }

        public static DataTable GetAllInternationalLicenseSameUser(int idUser)
        {
            return InternationLicenseData.GetAllInternationalLicenseSameUser(idUser);
        }

        public static int GetInternationalLicense(int idApp)
        {
            return InternationLicenseData.GetInternationalLicense(idApp);
        }

        public static DataTable GetAllIntLicense()
        {
            return InternationLicenseData.GetAllInternationalLicense();

        }



        }
}
