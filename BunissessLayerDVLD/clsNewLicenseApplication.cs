using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;
using static BunissessLayerDVLD.clsPerson;

namespace BunissessLayerDVLD
{
    public class clsNewLicenseApplication
    {
        public enum _enMode { Add=0, Update=1}
        _enMode _mode;

        public clsNewLicenseApplication()
        {
            idApplication = -1;
            ApplicationDate = DateTime.Now;
            idApplicationType = -1;
            idUser = -1;
            Status = "";    
            _mode=_enMode.Add;
        }

        private clsNewLicenseApplication(int idApplication,ref DateTime applicationDate,ref int idApplicationType,ref int idUser,ref string status)
        {
            this.idApplication = idApplication;
            ApplicationDate = applicationDate;
            this.idLicense = idLicense;
            this.idApplicationType = idApplicationType;
            this.idUser = idUser;
            Status = status;
        }

        public int Fees {  get; set; }

        public int idApplication {  get; set; }

        public  DateTime ApplicationDate { get; set; }

        public int idLicense { get; set; }

        public int idApplicationType { get; set; }

        public int idUser { get;set; }

        public string Status { get; set;}

        public static int GetFees(int fees)
        {
            clsNewLicenseApplicationData.GetFeesofApplicationTypes(ref fees); 
            
            return fees;
        }

        public static bool isUserOrNot(int idPerson)
        {
            return clsNewLicenseApplicationData.IsUserOrNot(idPerson);

        }

        public static DataTable GetLicenseClass()
        {
            return clsNewLicenseApplicationData.GetAllLicenseClass();
        }

        public  bool AddNewLocalLicense()
        {
            idApplication = clsNewLicenseApplicationData.AddNewLocalLicenses(ApplicationDate, idApplicationType, idUser, Status);
            return idApplication != -1;
        }

        public static void GetIDOfLicenseClass(ref int id, string LicenseType)
        {
           clsNewLicenseApplicationData.GetIDOfLicenseClass(ref id, LicenseType);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case _enMode.Add:
                    {
                        if (AddNewLocalLicense())
                        {
                            _mode = _enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                //case Mode.Update:
                //    {
                //        return UpdatePerson();
                //    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static DataTable GetIdLicenseClass(int idUser)
        {
            return clsNewLicenseApplicationData.GetAllLicenseByUser(idUser);
        }

        public static DataTable GetallStatus(int idUser)
        {
            return clsNewLicenseApplicationData.GetallStatus(idUser);
        }

        public static DataTable SearchData(string s,string Data)
        {
            return clsNewLicenseApplication.SearchData(s, Data);
        }

        public static clsNewLicenseApplication ClassNewwLicenseApplication(int idApplication)
        {
            DateTime ApplicationDate = DateTime.Now;
            int idApplicationType = 0;
            int idUser = 0; string status = "";
            if (clsNewLicenseApplicationData.GETApplicationByIDApp(idApplication,ref ApplicationDate, ref idApplicationType,ref idUser,ref status))
            {
                return new clsNewLicenseApplication(idApplication,ref ApplicationDate,ref idApplicationType,ref idUser,ref status);
            }
            else
            {
                return null;
            }
        }



    }
}
