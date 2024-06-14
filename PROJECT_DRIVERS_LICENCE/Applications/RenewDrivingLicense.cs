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
    public partial class RenewDrivingLicense : Form
    {
        private int idApp = 0;
        public RenewDrivingLicense()
        {
            InitializeComponent();
        }

        void Load()
        {
            DataTable dt = clsIssueDriving.GetLicenseByAppId(idApp);

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
                DataTable dt = clsIssueDriving.GetLicenseByAppId(idApp);

                // Check if the DataTable is not empty
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0]; // Access the first row

                    clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
                    clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
                    label32.Text = DateTime.Now.ToShortDateString();
                    label39.Text=clsApplicationTypes.GetFeesApplicationType(2).ToString();
                    label67.Text = p.FirstName + " " + p.SecondName;
                    label31.Text = DateTime.Now.ToShortDateString();
                    label2.Text = "20";
                    label4.Text=(20+ clsApplicationTypes.GetFeesApplicationType(2)).ToString();
                    // Display the new appointment date in label24 with only day, month, and year
                    label29.Text = DateTime.Now.AddYears(10).ToShortDateString();
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
            int LicenseID;
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
                    dt1 = clsIssueDriving.GetLicenseByAppId(idApp);
                    groupBox5.Enabled = false;
                    break;
                }
            }

            if (found)
            {
                
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        DataRow row = dt1.Rows[0]; // Get the first row
                        DateTime expirationDate = Convert.ToDateTime(row["ExpirationDate"]);

                    if (expirationDate > DateTime.Now)
                    {
                        MessageBox.Show("Selected License is not yet expired, it will expire on: " + expirationDate.ToString("dd/MM/yyyy"), "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        button3.Enabled = false;
                        label33.Text = idApp.ToString();
                        label27.Text = row["LicenseID"].ToString();
                        linkLabel1.Enabled = true;
                        Load();
                    }
                    else
                    {
                        linkLabel1.Enabled = false;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
            ShowLicense l = new ShowLicense(u.idUser,p.idPerson,"",idApp);
            l.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
            LicenseHistory l =new LicenseHistory(p.idPerson,idApp);
            l.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsIssueDriving.UpdateisActivetoFalse(idApp))
            {
                label33.Text = idApp.ToString();
                clsIssueDriving i = new clsIssueDriving();
                i.idApp = idApp;
                i.IssueDate = DateTime.Now;
                i.ExiprationDate= DateTime.Now.AddYears(10);
                i.isActive = true;
                i.IssueReason = "Renew";
                i.isDetainted = false;
                if (i.Save())
                {
                    MessageBox.Show("Data Stored Successfully ! with License ID = "+i.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    linkLabel1.Enabled = true;
                    button3.Enabled = false;
                    label33.Text=idApp.ToString();
                    label27.Text=i.LicenseID.ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
