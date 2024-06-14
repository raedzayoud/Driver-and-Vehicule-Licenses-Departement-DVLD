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
    public class clsLocalDrivingLicenseApplication
    {
        public static DataTable GetAllData()
        {
            return LocalDrivingLicenseApplicationData.GetAllLocalDriving(); 
        }

        public static void GetLicenseType(int id,ref string  LicenseType)
        {
            LocalDrivingLicenseApplicationData.GetLicenseClassById(id,ref LicenseType);

        }

        public static void GetLicenseType1(int id, ref string LicenseType) { 
        
            LocalDrivingLicenseApplicationData.GetLicenseClassById1(id, ref LicenseType);

        }

        public static bool ModifyStatus(int idApplication) {
        
            return LocalDrivingLicenseApplicationData.ModifySatatus(idApplication);
        
        }

        public static DataTable SearchData(string s,string Data)
        {
            return LocalDrivingLicenseApplicationData.SearchApplication(s,Data);
        }

        public static DataTable GetAllLocalLicense(int idApp)
        {
            return LocalDrivingLicenseApplicationData.GetAllLocalLicense(idApp);
        }

        public static bool AddLocal(int idApp,int LicenseID)
        {
            return LocalDrivingLicenseApplicationData.AddLocal(idApp, LicenseID);

        }

        public static DataTable GetAllDataofLocal()
        {
            return LocalDrivingLicenseApplicationData.GetAllLocalDrivingLicense();
        }

        public static DataTable GetApplication(int idApp)
        {
            return LocalDrivingLicenseApplicationData.GetApplication(idApp);
        }

        public static void GetIDLicenseByIDApp(int idApp,ref int idLicense)
        {
            LocalDrivingLicenseApplicationData.GetIdofLicenseByIdApp(idApp,ref idLicense);

        }
        
        public static int GetLocalDrivingApplicationByIdApp(int idApp)
        {
            return LocalDrivingLicenseApplicationData.GetLocalDrivingIDByIdApp(idApp);
        }
        //first Delete 
        public static bool DeleteApplicationfromTestAppointements(int idLocal)
        {
            return LocalDrivingLicenseApplicationData.DeleteApplicationfromTestAppointements(idLocal);
        }
        //Second Delete 
        public static bool DeleteApplicationfromLocal(int idLocal)
        {
            return LocalDrivingLicenseApplicationData.DeleteApplicationfromLocal(idLocal);
        }
        //Third Delete
        public static bool DeleteApplication(int idApp)
        {
            return LocalDrivingLicenseApplicationData.DeleteApplication(idApp);
        }

        //Update Status to completed
        public static bool updateStatustocompleted(int idApp,int idPerson)
        {
            return LocalDrivingLicenseApplicationData.UpdateStatusToCompleted(idApp,idPerson);
        }

        //Get Status 
        public static string GetStatusById(int idApp)
        {
            return LocalDrivingLicenseApplicationData.GetStatusById(idApp);
        }

        public static int GetIdPersonByIDUSERByIDApp(int idApp)
        {
            return LocalDrivingLicenseApplicationData.GetIDPersonByIdApp(idApp);
        }


    }
}
