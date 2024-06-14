using BunissessLayerDVLD;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class ShowLicense : Form
    {
        private int _idUser;
        private int _idPerson;
        private string ClassType;
        private int _idApp;
        bool isFound = false;
        bool isFound1 = false;
        DateTime IsssueDate;
        DateTime ExpirationDate;

        public ShowLicense(int idUser, int idPerson, string ClassType, int idApp)
        {
            InitializeComponent();
            this._idUser = idUser;
            this._idPerson = idPerson;
            this.ClassType = ClassType;
            this._idApp = idApp;
        }

        private void ShowLicense_Load(object sender, EventArgs e)
        {
            DataTable dt = clsIssueDriving.GetLicenseByAppIdAnyActive(this._idApp);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToBoolean(row["isActive"]) == false)
                    {
                        isFound = true;
                    }

                    if (Convert.ToBoolean(row["isActive"]) == true)
                    {
                        isFound1 = true;
                        IsssueDate = Convert.ToDateTime(row["IssueDate"]);
                        ExpirationDate = Convert.ToDateTime(row["ExpirationDate"]);
                    }
                }

                DataTable dt1 = clsIssueDriving.GetLicenseByAppId(this._idApp);

                // Assuming you want the "IssueReason" from the first row of dt1
                if (dt1.Rows.Count > 0)
                {
                    string licenseStatus = dt1.Rows[0]["IssueReason"].ToString();
                    UpdateUIWithLicenseInfo(dt, licenseStatus);
                }
                else
                {
                    Console.WriteLine("No detailed license data found for the given AppID.");
                    MessageBox.Show("No detailed license data found for the given AppID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Console.WriteLine("No data found for the given AppID.");
                MessageBox.Show("No data found for the given AppID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUIWithLicenseInfo(DataTable dt, string licenseStatus)
        {
            if (isFound1)
            {
                DataRow activeRow = null;
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["isActive"]) == 1)
                    {
                        activeRow = row;
                        break;
                    }
                }

                if (activeRow != null)
                {
                    label20.Text = licenseStatus;
                    string LicenseType = "";
                    int idLicenseType = 0;
                    clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(_idApp, ref idLicenseType);
                    clsLocalDrivingLicenseApplication.GetLicenseType(idLicenseType, ref LicenseType);

                    clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(_idApp));
                    clsUser u = clsUser.FindUserByIDPerson(p.idPerson);

                    label12.Text = u.FullName;
                    label17.Text = p.NationalNo;
                    label18.Text = p.Gender;
                    label23.Text = p.DateofBirth.ToString("dd/MM/yyyy");
                    label25.Text = activeRow["isDetainted"].ToString();
                    label10.Text = LicenseType;

                    label14.Text = activeRow["LicenseID"].ToString();
                    label19.Text = IsssueDate.ToString("dd/MM/yyyy");
                    label24.Text = ExpirationDate.ToString("dd/MM/yyyy");
                    label16.Text = Convert.ToBoolean(activeRow["isActive"]).ToString();

                    try
                    {
                        if (!string.IsNullOrEmpty(p.ImagePath) && System.IO.File.Exists(p.ImagePath))
                        {
                            pictureBox2.Image = System.Drawing.Image.FromFile(p.ImagePath);
                        }
                        else
                        {
                            Console.WriteLine("Image path is invalid or file does not exist.");
                            // Optionally, set a default image or handle the case when the image path is invalid
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while loading the image: " + ex.Message);
                        // Optionally, set a default image or handle the exception
                    }
                }
            }
        }


    }
}
