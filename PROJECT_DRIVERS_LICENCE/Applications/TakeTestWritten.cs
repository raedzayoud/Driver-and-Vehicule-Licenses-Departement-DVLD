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
    public partial class TakeTestWritten : Form
    {
        private int _idApp;
        private int testAppointemntsID;
        public TakeTestWritten(int idApp, int testAppointemntsID)
        {
            InitializeComponent();
            this._idApp = idApp;
            this.testAppointemntsID= testAppointemntsID;
        }
        private string GetLiccensetype()
        {
            int idLicese = 0;
            string LicenseClass = "";
            clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(this._idApp, ref idLicese);
            clsLocalDrivingLicenseApplication.GetLicenseType(idLicese, ref LicenseClass);
            return LicenseClass;
        }

        private int GetIDUSER()
        {
            return clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).idUser;
        }

        private void _Load()
        {
            label3.Text = _idApp.ToString();
            label4.Text = GetLiccensetype();
            label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
            //   label8.Text = "0";
            label18.Text = clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).ApplicationDate.ToShortDateString();
            int fees = 0;
            clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
            label12.Text = fees.ToString();
        }

        private void TakeTestWritten_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            if (radioButton1.Checked)
            {
                result = radioButton1.Text;
            }
            else
            {
                result = radioButton2.Text;
            }
            string note = textBox1.Text;
            if (clsWrittenTest.updateResultNote(testAppointemntsID, note, result))
            {
                if(radioButton1.Checked)
                {    // update Passed TEST=2
                    clsWrittenTest.UpdatePassedTestVision(_idApp);

                    if (clsStreetTest.UpdateTestStreet(_idApp))
                    {
                        MessageBox.Show("Data Stored Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                }
            else
            {
                MessageBox.Show("Data Stored Successfully !");
            }
        }

    }

}
