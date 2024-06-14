using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsSheduleTestAppointemets
    {
        enum _enMode { Add = 1, Update = 2 };
        _enMode mode;

        public int TestTypes {  get; set; }
       
        public int LocalDrivingLicenseID {  get; set; }

        public DateTime AppointementsDate {  get; set; }

       public int PaidFees {  get; set; }

       public int isLocked {  get; set; }

       public int idUser {  get; set; }

        public clsSheduleTestAppointemets()
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
            switch(mode)
            {
                case _enMode.Add:
                    {
                        if (AddNewTestAppointemets(TestTypes, LocalDrivingLicenseID, AppointementsDate, PaidFees, idUser, isLocked))
                        {
                            return true;
                        }
                        break;
                    }
            }
            return false;
        }

        private bool AddNewTestAppointemets(int TestTypes, int LocalDrivingLicenseID, DateTime AppointementsDate, int PaidFees, int UserID, int isLocked)
        {
           return clsShdeduleDataTestAppointemets.AddNewAppointements(TestTypes,LocalDrivingLicenseID,AppointementsDate,PaidFees,UserID,isLocked);
        }
    
        public static int GeetFees(int TestType)
        {
            return clsShdeduleDataTestAppointemets.GetFees(TestType);
        }

        public static DataTable GetAllAppointentsTestType1(int idLicense,int idApp)
        {
            return clsShdeduleDataTestAppointemets.GetAllAppointement(idLicense,idApp);
        }

        public static bool isFoundorNot(int idApp)
        {
            return clsShdeduleDataTestAppointemets.isFound(idApp);

        }

        public static bool UpdateAppointemnt(DateTime AppointementDate, int TestAppointementsID)
        {
            return clsShdeduleDataTestAppointemets.UpdateApponitemnts(AppointementDate,TestAppointementsID);
        }

        public static bool UpdateResultNote(int testAppointmentID, string note, string results)
        {
            return clsShdeduleDataTestAppointemets.updateResultNote(testAppointmentID, note, results);
        }

        public static bool GetResult(int testAppointements,ref string Result)
        {
            return clsShdeduleDataTestAppointemets.GetResult(testAppointements,ref Result);
        }

        public static bool GetLocaked(int testAppointements)
        {
            return clsShdeduleDataTestAppointemets.GETLocaked(testAppointements);
        }

        public static string GetResult(int idApp)
        {
            return clsShdeduleDataTestAppointemets.GetResult(idApp);
        }

        public static DataTable GetResults(int idApp)
        {
            return clsShdeduleDataTestAppointemets.GetResults(idApp);
        }

        public static int GetFeesofRetakeTest(int idTestAppointement)
        {
            return clsShdeduleDataTestAppointemets.GetFeesofRetakeTest(idTestAppointement);
        }

        public static bool UpdateRetakeID(int idTestAppointement)
        {
            return clsShdeduleDataTestAppointemets.updateRetakeTestID(idTestAppointement);
        }

        public static int SelectMaxTestAppointementsID()
        {
            return clsShdeduleDataTestAppointemets.SelectLastTestAppointementsID();
        }

        public static int SelectMaxTestAppointementsIDByLocalID(int LocalID)
        {
            return clsShdeduleDataTestAppointemets.SelectLastTestAppointementsIDBYTESTAPOINTEMENTS(LocalID);
        }

        public static int GETLocalDrivingID(int idApp)
        {
            return clsShdeduleDataTestAppointemets.GetLocalIDByAppID(idApp);
        }

        public static int GetPassedTestByAppID(int idApp)
        {
            return clsShdeduleDataTestAppointemets.GetPassedTest(idApp);
        }

        public static bool UpdatePassedTest(int idApp)
        {
            return clsShdeduleDataTestAppointemets.UpdatePassedTestVision(idApp);
        }

    }
}
