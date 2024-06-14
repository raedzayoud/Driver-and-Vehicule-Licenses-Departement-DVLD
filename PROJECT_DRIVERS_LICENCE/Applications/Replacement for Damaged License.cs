using BunissessLayerDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class Replacement_for_Damaged_License : Form
    {
        private int idApp;
        private int LicenseID;
        public Replacement_for_Damaged_License()
        {
            InitializeComponent();

            // Ensure the radio button is initialized properly
            InitializeRadioButton();
        }

        private void InitializeRadioButton()
        {
            // Ensure radioButton1 is not null before setting its Checked property
            if (radioButton1 != null)
            {
                radioButton1.Checked = true;
            }
            else
            {
                MessageBox.Show("radioButton1 is not initialized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

                // Check if the DataTable is not empty
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0]; // Access the first row

                    clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
                    clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
                    label32.Text = DateTime.Now.ToShortDateString();
                    label67.Text = p.FirstName + " " + p.SecondName;
                    // Display the new appointment date in label24 with only day, month, and year
                    label30.Text = row["LicenseID"].ToString();
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
               
                    if (Convert.ToBoolean(row["isActive"])==false)
                    {
                        MessageBox.Show("Selected Licenses is not Active,Choose an active license", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        button3.Enabled = false;
                        linkLabel1.Enabled = false;
                        //  linkLabel2.Enabled = false;
                        label2.Text = row["IssueReason"].ToString();
                        Load();
                    }
                    else
                    {

                        label2.Text = row["IssueReason"].ToString();
                        linkLabel1.Enabled = false;
                      //  linkLabel2.Enabled = true;
                        button3.Enabled=true;
                        Load();
                    }
                }

            }
            else
            {
                MessageBox.Show("License ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadApplication();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (clsIssueDriving.UpdateisActivetoFalse(idApp))
            {
                label33.Text = idApp.ToString();
                clsIssueDriving i = new clsIssueDriving();
                i.idApp = idApp;
                i.IssueDate = DateTime.Now;
                i.ExiprationDate = DateTime.Now.AddYears(10);
                i.isActive = true;
                if (radioButton1.Checked)
                    i.IssueReason = "Replacement For Damage";
                else
                    i.IssueReason = "Replacement For Lost";
                i.isDetainted = false;
                if (i.Save())
                {
                    MessageBox.Show("Data Stored Successfully ! with License ID = " + i.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    linkLabel1.Enabled = true;
                    button3.Enabled = false;
                    label33.Text = idApp.ToString();
                    label27.Text = i.LicenseID.ToString();
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
            ShowLicense l = new ShowLicense(u.idUser, p.idPerson, "", idApp);
            l.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            LicenseHistory licenseHistory = new LicenseHistory(p.idPerson,idApp);
            licenseHistory.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = clsApplicationTypes.GetFeesApplicationType(4).ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = clsApplicationTypes.GetFeesApplicationType(3).ToString();
        }
    }
}
