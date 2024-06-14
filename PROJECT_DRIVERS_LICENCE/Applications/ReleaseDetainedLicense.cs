﻿using BunissessLayerDVLD;
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
    public partial class ReleaseDetainedLicense : Form
    {
        private int LicenseID;
        private int idApp;
        public ReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
            ShowLicense l = new ShowLicense(u.idUser, p.idPerson, "", idApp);
            l.ShowDialog();
        }

        void Load()
        {
            DataTable dt = clsIssueDriving.GetLicenseByIdLicenseID(LicenseID);

            // Check if the DataTable is not empty
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; // Access the first row

                clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
                clsUser u = clsUser.FindUserByIDPerson(p.idPerson);

                label52.Text = row["LicenseID"].ToString();
                label53.Text = u.FullName;
                label51.Text = p.NationalNo;
                label50.Text = p.Gender;
                label46.Text = p.DateofBirth.ToString("dd/MM/yyyy");
                label42.Text = row["isActive"].ToString();

                int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(idApp);
                string LicenseType = "";
                int idLicense = 0;
                clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(idApp, ref idLicense);
                clsLocalDrivingLicenseApplication.GetLicenseType(idLicense, ref LicenseType);
                label54.Text = LicenseType;

                label49.Text = Convert.ToDateTime(row["IssueDate"]).ToString("dd/MM/yyyy");
                label45.Text = Convert.ToDateTime(row["ExpirationDate"]).ToString("dd/MM/yyyy");
                label44.Text = "NO";

                try
                {
                    if (!string.IsNullOrEmpty(p.ImagePath) && System.IO.File.Exists(p.ImagePath))
                    {
                        pictureBox33.Image = Image.FromFile(p.ImagePath);
                    }
                    else
                    {
                        MessageBox.Show("Image path is invalid or file does not exist.");
                        // Optionally, set a default image or handle the case when the image path is invalid
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading the image: " + ex.Message);
                    // Optionally, set a default image or handle the exception
                }
            }

            else
            {
                MessageBox.Show("No license information found for the given application ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadApplication()
        {
            if (idApp != 0)
            {
                DataTable dt = clsIssueDriving.GetLicenseByIdLicenseID(LicenseID);
                DataTable dt1 = clsDetain.GetInfoDetainLicense(LicenseID);

                // Check if the DataTable dt is not empty
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0]; // Access the first row

                    clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
                    clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
                    label32.Text = p.DateofBirth.ToString("dd/MM/yyyy");
                    label67.Text = p.FirstName + " " + p.SecondName;
                    label30.Text = row["LicenseID"].ToString();
                    label6.Text = clsApplicationTypes.GetFeesApplicationType(5).ToString();
                   
                    // Check if the DataTable dt1 is not empty
                    if (dt1.Rows.Count > 0)
                    {
                         DataRow row1 = dt1.Rows[0]; // Access the first row

                        label4.Text = row1["FineFees"].ToString();
                        label33.Text = row1["DetainID"].ToString();
                    }
                    else
                    {
                        label33.Text = "0";
                        label4.Text = "0"; // Default to "0" if no fine fees information is available
                    }
                    label8.Text= (Convert.ToInt16(label4.Text)+clsApplicationTypes.GetFeesApplicationType(5)).ToString();

                }
                else
                {
                    MessageBox.Show("No license information found for the given application ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(textBox2.Text, out LicenseID))
            {
                MessageBox.Show("Please enter a valid License ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable test = clsIssueDriving.GetLicenseByIdLicenseID(LicenseID);
            bool test1 = false;
            if (test.Rows.Count > 0) // Check if there are any rows in the DataTable
            {
                DataRow row = test.Rows[0]; // Get the first row
                if (row["isDetainted"] != DBNull.Value && Convert.ToInt32(row["isDetainted"]) == 0) // Check if "isDetainted" column value is 1
                {
                    MessageBox.Show("Selected another one, this License is not Detainted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button3.Enabled = false;
                    test1 = true;
                }
            }

            DataTable dt = clsIssueDriving.GetAllLicense();
            DataTable dt1 = null;

            // Use a flag to track if the LicenseID was found
            bool found = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["LicenseID"]) == LicenseID)
                {
                    found = true;
                    idApp = Convert.ToInt32(dr["AppID"]);
                    dt1 = clsIssueDriving.GetLicenseByIdLicenseID(LicenseID);
                    // groupBox5.Enabled = false;

                    break;
                }
            }

            if (found)
            {

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    DataRow row = dt1.Rows[0]; // Get the first row

                    label2.Text = row["IssueReason"].ToString();
                    linkLabel1.Enabled = false;
                    if (!test1)
                        button3.Enabled = true;
                    Load();

                }

            }
            else
            {
                MessageBox.Show("License ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadApplication();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            LicenseHistory licenseHistory = new LicenseHistory(p.idPerson, idApp);
            licenseHistory.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt1 = clsDetain.GetInfoDetainLicense(LicenseID);
            clsReleaseApp c = new clsReleaseApp();
            if (dt1.Rows.Count > 0)
            {
                DataRow row1 = dt1.Rows[0]; // Access the first row
                c.DetainID =Convert.ToInt32(row1["DetainID"]);
            }
            c.isRelease = true;
            c.ReleaseDate=DateTime.Now;
            if (c.Save())
            {
                if (clsIssueDriving.UpdateDetaintofalse(LicenseID))
                {
                    MessageBox.Show("Release Strored Successufully with "+c.ReleaseAppID,"Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    groupBox5.Enabled = false;
                    linkLabel1.Enabled = true;
                    button3.Enabled = false;
                    label10.Text=c.ReleaseAppID.ToString();
                }
            }
        
       
        }
    }
}
