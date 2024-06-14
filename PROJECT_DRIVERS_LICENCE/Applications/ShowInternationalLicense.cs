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
    public partial class ShowInternationalLicense : Form
    {
        private int _idApp;
        public ShowInternationalLicense(int idApp)
        {
            InitializeComponent();
            _idApp = idApp;
            Load();
        }

        void Load()
        {
            clsPerson p = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(_idApp));
            clsUser u = clsUser.FindUserByIDPerson(p.idPerson);
            label33.Text=_idApp.ToString();
            label53.Text = u.FullName;
            label51.Text = p.NationalNo;
            label42.Text=u.Bit.ToString();
            label46.Text=p.DateofBirth.ToString("dd/mm/yyyy");
            label44.Text = "NO";
            int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(_idApp);
            label49.Text =DateTime.Now.ToShortDateString();
            label45.Text = DateTime.Now.AddYears(10).ToShortDateString();

            label27.Text = clsInternationalLicense.GetInternationalLicense(_idApp).ToString();

            label50.Text=p.Gender.ToString();

            try
            {
                if (!string.IsNullOrEmpty(p.ImagePath) && System.IO.File.Exists(p.ImagePath))
                {
                    pictureBox1.Image = Image.FromFile(p.ImagePath);
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

    }
}
