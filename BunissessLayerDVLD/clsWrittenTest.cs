using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunissessLayerDVLD
{
    public class clsWrittenTest
    {
        public static bool UpdateTestTypeto2()
        {
            return clsWrittenTestData.UpdateTestTypeto2();
        }

        public static DataTable GetAllTestAppointementsofTest2(int idApp,int idLicesne)
        {
            return clsWrittenTestData.GetAllTestAppointementsofTest2(idApp,idLicesne);
        }

        public static bool UpdatePaidFeesto20ofAppllication()
        {
            return clsWrittenTestData.UpdatePaidFeesto20ofAppllication();
        }

        public static bool UpdateResulttofail(int idApp)
        {
            return clsWrittenTestData.UpdateResulttofail(idApp);
        }

        public static bool DeleteTestAppointements(int idLocal)
        {
            return clsWrittenTestData.DeleteTestAppointemnts(idLocal);
        }

        public static bool GetTestType(int LocalID,ref int TestType)
        {
            return clsWrittenTestData.GetTestType(LocalID,ref TestType);
        }
        
        public static bool UpdatePassedTestVision(int _idApp)
        {
            return clsWrittenTestData.UpdatePassedTestVision(_idApp);
        }

        public static bool updateResultNote(int testAppointmentID, string note, string results)
        {
            return clsWrittenTestData.updateResultNote(testAppointmentID,note,results);
        }



    }
}
