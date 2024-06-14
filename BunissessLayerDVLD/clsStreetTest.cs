using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public  class clsStreetTest
    {
        enum _enMode { Add = 1, Update = 2 };
        _enMode mode;

        public int TestTypes { get; set; }

        public int LocalDrivingLicenseID { get; set; }

        public DateTime AppointementsDate { get; set; }

        public int PaidFees { get; set; }

        public int isLocked { get; set; }

        public int idUser { get; set; }

        public clsStreetTest()
        {
            this.TestTypes = -1;
            this.isLocked = -1;
            this.PaidFees = -1;
            this.idUser = -1;
            this.LocalDrivingLicenseID = -1;
            this.AppointementsDate = DateTime.Now;
            mode = _enMode.Add;
        }

        public bool Save()
        {
            switch (mode)
            {
                case _enMode.Add:
                    {
                        if (AddNewTestAppointemets(TestTypes, LocalDrivingLicenseID, AppointementsDate, PaidFees, idUser, isLocked))
                        {
                            return true;
                        }
                        break;
                    }
                //case _enMode.Update:
                //    {

                //    }
            }
            return false;
        }

        public static DataTable GetAllTestAppointementsofTest3(int idApp, int idLicense)
        {
            return clsStreetData.GetAllTestAppointementsofTest3(idApp,idLicense);
        }


        private bool AddNewTestAppointemets(int TestTypes, int LocalDrivingLicenseID, DateTime AppointementsDate, int PaidFees, int UserID, int isLocked)
        {
            return clsShdeduleDataTestAppointemets.AddNewAppointements(TestTypes, LocalDrivingLicenseID, AppointementsDate, PaidFees, UserID, isLocked);
        }


        // i make this function to replace with 35 Fees in StreetTestAppointelents
        public static int GetFeesTestType3()
        {
            return clsStreetData.GetFeesTestType3();

        }

        public static bool InsertDataAppointements(int TestTypes, int LocalDrivingLicenseID, DateTime AppointementsDate, int PaidFees, int UserID, int isLocked)
        {
            return clsStreetData.InsertDataAppointements(TestTypes,LocalDrivingLicenseID,AppointementsDate,PaidFees,UserID,isLocked);
        }

        public static bool UpdatePassedTestVision(int idApp)
        {
            return clsStreetData.UpdatePassedTestVision(idApp);
        }

        public static bool UpdateTestStreet(int idApp)
        {
            return clsStreetData.UpdateTestStreet(idApp);
        }


    }
}
