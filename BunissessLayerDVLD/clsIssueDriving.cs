using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BunissessLayerDVLD.clsPerson;

namespace BunissessLayerDVLD
{
    public class clsIssueDriving
    {
        public enum Mode { Add = 1, Update = 2 };
        Mode _mode;
        public int LicenseID { get; set; }
        public string note {  get; set; }
        public int idApp {  get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExiprationDate { get; set; }
       
        public bool isActive {  get; set; }

        public string IssueReason {  get; set; }

        public bool isDetainted {  get; set; }


        public clsIssueDriving()
        {
             note = "";
             idApp = -1;
             IssueDate=DateTime.Now;
             ExiprationDate=DateTime.Now;
             IssueReason = "";
             isActive = false;
             isDetainted=false;
            _mode = Mode.Add;
        }

        private bool AddNewLicense()
        {
            LicenseID = clsLicenseData.InsertData(note,idApp,IssueDate,ExiprationDate,isActive,IssueReason,isDetainted);
            return LicenseID != -1;
        }

        public bool Save()
        {
            try
            {
                switch (_mode)
                {
                    case Mode.Add:
                        if (AddNewLicense())
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

        public static int GetLicenseID(int idApp)
        {
            return clsLicenseData.GetLicenseID(idApp);
        }

        public static DateTime GetAppointmentDate(int idLocal)
        {
            return clsLicenseData.GetAppointmentDate(idLocal);
        }

        public static bool DeleteLicenseFromApplication(int idApp)
        {
            return clsLicenseData.DeleteLicenseFromApplication(idApp); 
        }

        public static DataTable GetAllLicenseSameUser(int idUser)
        {
            return clsLicenseData.GetAllLicenseSameUser(idUser);
        }

        public static DataTable GetAllLicense()
        {
            return clsLicenseData.GetAllLicense();
        }

        public static DataTable GetLicenseByAppId(int appId)
        {
            return clsLicenseData.GetLicenseByAppId(appId);
        }
        public static DataTable GetLicenseByAppIdAnyActive(int appId)
        {
            return clsLicenseData.GetLicenseByAppIdAnyActive(appId);
        }

        public static bool UpdateisActivetoFalse(int idApp)
        {
            return clsLicenseData.UpdateisActivetoFalse(idApp);
        }

        public static DataTable GetLicenseByIdLicenseID(int idLicense)
        {
            return clsLicenseData.GetLicenseByLicenseID(idLicense);

        }

        public static bool UpdateDetaintotrue(int idLicense)
        {
            return clsLicenseData.UpdateDetaintotrue(idLicense);
        }
        public static bool UpdateDetaintofalse(int idLicense)
        {
            return clsLicenseData.UpdateDetaintofalse(idLicense);
        }





    }
}
