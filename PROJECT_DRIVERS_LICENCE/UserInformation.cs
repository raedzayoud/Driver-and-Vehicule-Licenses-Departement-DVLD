using BunissessLayerDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE
{
    public partial class UserInformation : UserControl
    {
        private int _idPerson;
        private int _idUser;
        public UserInformation(int idPerson,int idUser)
        {
            InitializeComponent();
            this._idPerson = idPerson;
            this._idUser = idUser;
        }

        

        private void UserInformation_Load(object sender, EventArgs e)
        {
            PersonInformation p=new PersonInformation(_idPerson);
            this.Controls.Add(p);
            clsUser c=clsUser.FindUserByID(_idUser);
            if (c != null)
            {
                label3.Text = c.idUser.ToString();
                label5.Text = c.username;
                label7.Text=c.Bit.ToString();
            }
            

        }
    }
}
