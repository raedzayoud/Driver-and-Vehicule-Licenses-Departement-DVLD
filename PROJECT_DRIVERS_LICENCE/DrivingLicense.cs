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
    public partial class DrivingLicense : UserControl
    {
        private int idUser;
        private int idApp;
        public DrivingLicense()
        {
            InitializeComponent();
        }
        public DrivingLicense(int idApp)
        {
            InitializeComponent();
            this.idApp = idApp;
            Load();
        }

        void Load()
        {
            string License = "";
            int LicneseID = 0;
            DataTable dt = clsLocalDrivingLicenseApplication.GetAllLocalLicense(idApp);
            // to fix this bug we need to call the idLicense from idApp and call the License Type
            clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(idApp, ref LicneseID);
            clsLocalDrivingLicenseApplication.GetLicenseType(LicneseID, ref License);
            label2.Text = idApp.ToString();

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; // Assuming there's only one row returned
                label9.Text = Convert.ToDateTime(row["ApplicationDate"]).ToString("dd/MM/yyyy");
                label22.Text = Convert.ToDateTime(row["ApplicationDate"]).ToString("dd/MM/yyyy");
                int idUser = Convert.ToInt32(row["idUser"]);
                label10.Text = row["Status"].ToString();
                label24.Text = clsUser.FindUserByID(idUser).username;
                label18.Text = "New Local Driving License Service";
            }
            else
            {
                label9.Text = "No application found";
            }
            // this coloum of PassedTest
            label5.Text = clsSheduleTestAppointemets.GetPassedTestByAppID(idApp).ToString() + "/3";
            label6.Text = License;
            int fees = 0;
            clsApplicationTypes.GetFeesApplication(idApp, ref fees);
            label16.Text = fees.ToString();
            DataTable dt1 = clsLocalDrivingLicenseApplication.GetApplication(idApp);
            if (dt1.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; // Assuming there's only one row returne
                string name = clsUser.FindUserByID(Convert.ToInt32(row["idUser"])).FullName;
                label20.Text = name;
                label13.Text = row["idUser"].ToString();
                idUser = Convert.ToInt32(label13.Text);
            }


        }

        private void DrivingLicense_Load(object sender, EventArgs e)
        {
            Load();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int idperson = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp);
            People.ShowClinetList p = new People.ShowClinetList(idperson);
            p.ShowDialog();
        }

        
    }
}
