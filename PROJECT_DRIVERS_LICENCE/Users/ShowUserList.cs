using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.Users
{
    public partial class ShowUserList : Form
    {
        private int idUser;
        private int idPerson;
        public ShowUserList(int idUser, int idPerson)
        {
            InitializeComponent();
            this.idUser = idUser;
            this.idPerson = idPerson;
        }

        private void ShowUserList_Load(object sender, EventArgs e)
        {
            UserInformation userInformation=new UserInformation(idUser, idPerson);
            this.Controls.Add(userInformation);
        }
    }
}
