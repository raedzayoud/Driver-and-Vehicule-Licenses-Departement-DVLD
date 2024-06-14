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
    public partial class clsScheduleTest : Form
    {

        private enum enMode { add = 1, update = 2 ,RetakeEdit = 3, RetakeAdd = 4 }
        enMode mode;
        private int _idApp;
        private DateTime _dt;
        private int test;
        public clsScheduleTest(int idApp,DateTime dt,int TestAppointemnt, int choix=1)
        {
            InitializeComponent();
            if (choix == 1)
            {
                _idApp = idApp;
                mode=enMode.add;
            }
            else if(choix==-1)
            {
                _idApp = idApp;
                _dt = dt;
                mode = enMode.update;
                test = TestAppointemnt;
            }
            else if(choix==3)
            {
                _idApp = idApp;
                _dt = dt;
                mode = enMode.RetakeAdd;
                test = TestAppointemnt;
            }
            else if (choix == 4)
            {
                _idApp = idApp;
                _dt = dt;
                mode = enMode.RetakeEdit;
                test = TestAppointemnt;

            }
            else if (choix == 2)
            {
                _idApp = idApp;
                _dt = dt;
                test = TestAppointemnt;

            }

        }
        public clsScheduleTest(int idApp)
        {
            InitializeComponent();
            _idApp=idApp;
            mode = enMode.add;
        }

        private string GetLiccensetype()
        {
            int idLicese=0;
            string LicenseClass = "";
            clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(this._idApp, ref idLicese);
            clsLocalDrivingLicenseApplication.GetLicenseType(idLicese, ref LicenseClass);
            return LicenseClass;
        }

        private int GetIDUSER()
        {
            return clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).idUser;
        }

        private void _LoadData()
        {
            if (mode == enMode.add)
            {
                groupBox1.Enabled = false;
                label3.Text = _idApp.ToString();
                label4.Text = GetLiccensetype();
                label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
               // label8.Text = "0";
                dateTimePicker1.Text = clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).ApplicationDate.ToString();
                int fees = 0;
                clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
                label12.Text = fees.ToString();
                label19.Visible = false;
            }
            else if(mode==enMode.update)
            {
                groupBox1.Enabled = false;
                label3.Text = _idApp.ToString();
                label4.Text = GetLiccensetype();
                label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
               // label8.Text = "0";
                dateTimePicker1.Text =_dt.ToString();
                int fees = 0;
                clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
                label12.Text = fees.ToString();
                label19.Visible = false;
            }
            else if (mode == enMode.RetakeAdd)
            {
                label1.Text = "Schedule Retake Test";
                groupBox1.Enabled = true;
                label3.Text = _idApp.ToString();
                label4.Text = GetLiccensetype();
                label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
             //   label8.Text = "0";
                dateTimePicker1.Text = _dt.ToString();
                int fees = 0;
                clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
                label12.Text = fees.ToString();
                label19.Visible = false;
                int FeesReatake = clsApplicationTypes.GetFeesApplicationType(7);
                label14.Text=FeesReatake.ToString();
                label16.Text=(fees+FeesReatake).ToString();
            }
            else if (mode == enMode.RetakeEdit)
            {
                label1.Text = "Schedule Retake Test";
                groupBox1.Enabled = true;
                label3.Text = _idApp.ToString();
                label4.Text = GetLiccensetype();
                label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
               // label8.Text = "0";
                dateTimePicker1.Text = _dt.ToString();
                int fees = 0;
                clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
                label12.Text = fees.ToString();
                label19.Visible = false;
                int FeesReatake = clsSheduleTestAppointemets.GetFeesofRetakeTest(test);
                label14.Text = FeesReatake.ToString();
                label16.Text = (fees + FeesReatake).ToString();
                label18.Text=clsSheduleTestAppointemets.SelectMaxTestAppointementsID().ToString();
            }
            else
            {
                groupBox1.Enabled=true;
                label3.Text = _idApp.ToString();
                label4.Text = GetLiccensetype();
                label6.Text = clsUser.FindUserByID(GetIDUSER()).FullName;
            //    label8.Text = "1";
                dateTimePicker1.Enabled=false;
                int fees = 0;
                clsApplicationTypes.GetFeesApplication(_idApp, ref fees);
                label12.Text = fees.ToString();
                button1.Enabled=false;
                label19.Visible = true;
                int FeesReatake = 0;
                label14.Text = FeesReatake.ToString();
                label16.Text = (fees + FeesReatake).ToString();
                groupBox1.Enabled = false;
            }
        }

        private void clsScheduleTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(mode == enMode.RetakeAdd)
            {
                clsSheduleTestAppointemets s = new clsSheduleTestAppointemets();
                s.TestTypes = 1;
                s.AppointementsDate = dateTimePicker1.Value;
                s.isLocked = 0;
                s.PaidFees =Convert.ToInt16(label14.Text);
                s.LocalDrivingLicenseID = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(_idApp);
                s.idUser = clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).idUser;
                if (s.Save())
                {
                    int idLastTestAppointemnt =clsSheduleTestAppointemets.SelectMaxTestAppointementsID();
                    if (clsSheduleTestAppointemets.UpdateRetakeID(idLastTestAppointemnt))
                    {
                        label18.Text=idLastTestAppointemnt.ToString();
                        MessageBox.Show("Data Stored Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data is not Stored !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data is not Stored !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else if (mode == enMode.add )
            {
                clsSheduleTestAppointemets s = new clsSheduleTestAppointemets();
                s.TestTypes = 1;
                s.AppointementsDate = dateTimePicker1.Value;
                s.isLocked = 0;
                s.PaidFees =0;
                s.LocalDrivingLicenseID = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(_idApp);
                s.idUser = clsNewLicenseApplication.ClassNewwLicenseApplication(_idApp).idUser;
                if (s.Save())
                {
                    MessageBox.Show("Data Stored Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data is not Stored !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                DateTime dt = dateTimePicker1.Value;
                // call the method Update
                if (clsSheduleTestAppointemets.UpdateAppointemnt(dt, test))
                {
                    MessageBox.Show("Data Updated Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data is not Stored !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
