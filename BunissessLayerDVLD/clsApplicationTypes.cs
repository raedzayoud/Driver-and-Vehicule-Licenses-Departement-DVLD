using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;
namespace BunissessLayerDVLD
{
    public class clsApplicationTypes
    {
        public int ID {  get; set; }

        public string Title { get; set; }
        public int Fees { get; set; }

        private clsApplicationTypes(int id,string Title,int Fees) {
            ID = id;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();

        } 
      
        public static clsApplicationTypes GetAllInformationOfApplication(int id)
        {
            string title = "";
            int fees=0;
            if(clsApplicationTypesData.GetApplication(id,ref title,ref fees))
            {
                return new clsApplicationTypes(id,title, fees);
            }
            else
            {
                return null;
            }
        }

        public static bool isUpdate(int id,string  title,int fees)
        {
            return clsApplicationTypesData.UpdateDataApplicationTypes(id, title, fees);
        }
        
        public static void GetFeesApplication(int id,ref int Fees)
        {
            clsApplicationTypesData.GetFeesApplication(id, ref Fees);
        }
    
        public static int GetFeesApplicationType(int id)
        {
            return clsApplicationTypesData.GetFeesApplicationTypes(id);
        }

    }
}
