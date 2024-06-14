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

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class IssueDrivingLicense : Form
    {
        private int _idApp;
        public IssueDrivingLicense(int idApp)
        {
            InitializeComponent();
            _idApp = idApp;
        }

        private void IssueDrivingLicense_Load(object sender, EventArgs e)
        {
            DrivingLicense d=new DrivingLicense(_idApp);
            this.Controls.Add(d);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(this._idApp);
            DateTime appointmentDate = clsIssueDriving.GetAppointmentDate(idLocal);
            string note = textBox1.Text;
            clsIssueDriving s= new clsIssueDriving();
            s.note= note;
            s.idApp= _idApp;
            s.IssueDate= appointmentDate;
            s.isActive= true;
            DateTime newAppointmentDate = appointmentDate.AddYears(10);
            s.ExiprationDate= newAppointmentDate;
            s.IssueReason = "First Time";
            s.isDetainted = false;
            if (s.Save())
            {
                clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(_idApp));
                //Update Status to completed
                if (clsLocalDrivingLicenseApplication.updateStatustocompleted(_idApp,p.idPerson))
                {
                    MessageBox.Show("Data Stored Success.Your Licesne = " + s.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Data is not Stored Success", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
