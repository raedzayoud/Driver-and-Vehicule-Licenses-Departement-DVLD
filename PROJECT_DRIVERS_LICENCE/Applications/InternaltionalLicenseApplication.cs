using BunissessLayerDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class InternaltionalLicenseApplication : Form
    {
        private Image originalImage;
        int idApp = 0;
        int LicenseID;
        public InternaltionalLicenseApplication()
        {
            InitializeComponent();
            linkLabel1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsInternationalLicense.InsertInternationalLicense(idApp,DateTime.Now,DateTime.Now.AddYears(10))!=-1)
            {
                MessageBox.Show("Data Stored Successfully with Int.LicenseID = "+clsInternationalLicense.GetInternationalLicense(idApp), "Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                linkLabel1.Enabled = true;
                button3.Enabled = false;
                label33.Text=idApp.ToString();
                label27.Text = clsInternationalLicense.GetInternationalLicense(idApp).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Return()
        {
            MessageBox.Show("There is no License ID of this Type !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            label52.Text = "???";
            label53.Text = "???";
            label51.Text = "???";
            label50.Text = "???";
            label46.Text = "???";
            label42.Text = "???";
            label54.Text = "???";
            label49.Text = "???";
            label44.Text = "???";
            label45.Text = "???";
            // Load an image from the Resources folder
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "Male 5121.png");
            if (System.IO.File.Exists(imagePath))
            {
                originalImage = Image.FromFile(imagePath);
                pictureBox1.Image = originalImage;
            }
            else
            {
                MessageBox.Show("Image not found!");
            }


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

                if (Convert.ToInt32(row["isActive"]) == 1)
                {
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
                    MessageBox.Show("This user is not active!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No license information found for the given application ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void SearchDriverLicenseInfo()
        {
            LicenseID = Convert.ToInt32((textBox2.Text));
            DataTable dt = clsIssueDriving.GetAllLicense();

            // we can use BinarySearch tech to identify the idApp fast

            bool test = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["LicenseID"]) == LicenseID)
                {
                    test = true;
                    idApp = Convert.ToInt32(dr["AppID"]);
                    groupBox5.Enabled = false;
                    break;
                }
            }
            int idLicensetype = 0;
            clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(idApp, ref idLicensetype);
            if (idLicensetype != 3)
            {
                MessageBox.Show("Selected License should be class 3,select another one.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button3.Enabled = false;
                linkLabel1.Enabled= false;
                Load();
                return;
            }
            if (test)
            {
                if (!clsInternationalLicense.FoundInternationalLicense(idApp))
                {
                    Load();
                }
                else
                {
                    MessageBox.Show("Sorry this User have Aleardy a international License !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label33.Text = idApp.ToString();
                    //GET License ID
                    label27.Text = clsInternationalLicense.GetInternationalLicense(idApp).ToString();
                    Load();
                    linkLabel1.Enabled = true;
                    button3.Enabled = false;

                }

            }
            else
            {
                Return();
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
                    label32.Text = p.DateofBirth.ToString("dd/MM/yyyy");
                    int fees = 0;
                    clsApplicationTypes.GetFeesApplication(6,ref fees);
                    label39.Text =fees.ToString();
                    label67.Text = p.FirstName + " " + p.SecondName;
                    label31.Text = Convert.ToDateTime(row["IssueDate"]).ToString("dd/MM/yyyy");

                    // Display the new appointment date in label24 with only day, month, and year
                    label29.Text = Convert.ToDateTime(row["ExpirationDate"]).ToString("dd/MM/yyyy");
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
            SearchDriverLicenseInfo();
            LoadApplication();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowInternationalLicense s=new ShowInternationalLicense(idApp);
            s.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int idperson = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp);
            LicenseHistory h =new LicenseHistory(idperson,idApp);
            h.Show();
           
        }

       
    }
}
