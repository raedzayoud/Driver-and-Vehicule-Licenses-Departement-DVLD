using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;
namespace BunissessLayerDVLD
{
    public class clsTestType
    {
        public int ID {  get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Fees { get; set; }


        private clsTestType(int id,string title,int fees, string Description) {
            this.ID = id;
            this.Title = title;
            this.Description = Description;
            this.Fees = fees;        
        }

        public static DataTable GetDataofTest()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static clsTestType GetAllInformationOfApplication(int id)
        {
            string title = "";
            int fees = 0;
            string Description = "";
            if (clsTestTypeData.GetTestTypes(id, ref title, ref fees,ref Description))
            {
                return new clsTestType(id, title, fees,Description);
            }
            else
            {
                return null;
            }
        }

        public static bool isUpdate(int id, string title, int fees,string Description)
        {
            return clsTestTypeData.UpdateDataTestTypes(id,title,fees,Description);
        }



    }
}
