using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;

namespace BunissessLayerDVLD
{
    public  class clsLogin
    {
        public static bool isCorrect(string username,string pass)
        {
            return clsLoginData.FindByUsernamePassword(username, pass);
        }

        public static DataTable FindId(string username,string pass)
        {
            return clsLoginData.FindIDByUsernameandPassword(username, pass);
        }
       
    }
}
