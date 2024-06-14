using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.Applications;
using PROJECT_DRIVERS_LICENCE.LoginSystem;
using PROJECT_DRIVERS_LICENCE.People;
using PROJECT_DRIVERS_LICENCE.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.WindowPricipale
{
    public partial class All : Form
    {
        public All()
        {
            InitializeComponent();
        }
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            People.People p = new People.People();
            p.Show();
        }

        private void All_Load(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Visible = false;
            LoginSystem.Login login = new LoginSystem.Login();
            login.ShowDialog();
            

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users.Users user=new Users.Users();
            user.ShowDialog();
        }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountSetting.Account c1= new AccountSetting.Account();
            c1.ShowDialog();
        }

        void ChargerCredentials(string cheminFichier, ref string usernme, ref string password)
        {
            try
            {
                // Lecture des données utilisateur depuis le fichier
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        // Séparation de la ligne en username et password
                        string[] credentials = line.Split(',');
                        usernme = credentials[credentials.Length - 2].ToString();
                        password = credentials[credentials.Length - 1].ToString();

                    }
                    else
                    {
                        Console.WriteLine("Le fichier est vide.");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors du chargement des données : " + ex.Message);
                //return null;
            }
        }


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string username = "";
            string password = "";
            ChargerCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", ref username, ref password);
            DataTable dt = clsLogin.FindId(username, password);

            if (dt != null && dt.Rows.Count > 0)
            {
             int idUser = Convert.ToInt32(dt.Rows[0]["idUser"]);
             int idPerson = Convert.ToInt32(dt.Rows[0]["idPerson"]);

                Password p = new Password(idPerson, idUser);
                p.ShowDialog();
            }
        }
        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Applications.ManageApplicationTypes pw=new Applications.ManageApplicationTypes();
            pw.ShowDialog();

        }

        private void manageTestsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Applications.ManageTestTypes c=new Applications.ManageTestTypes();
            c.ShowDialog();
            
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Applications.NewLocalLicense c=new Applications.NewLocalLicense();
            c.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idPerson1 = 0;
            string username = "";
            string password = "";
            ChargerCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", ref username, ref password);
            DataTable dt = clsLogin.FindId(username, password);
            if (dt != null && dt.Rows.Count > 0)
            {
                idPerson1 = Convert.ToInt32(dt.Rows[0]["idPerson"]);
            }
            Applications.LocalDrivingLicenseApplication c=new Applications.LocalDrivingLicenseApplication(idPerson1);
            c.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void drToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drivers.Driver driver = new Drivers.Driver();
            driver.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drivers.Driver driver = new Drivers.Driver();
            driver.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           InternaltionalLicenseApplication i=new InternaltionalLicenseApplication();
            i.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InernationalDrivingLicenseApplication i=new InernationalDrivingLicenseApplication();
            i.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewDrivingLicense a=new RenewDrivingLicense();
            a.ShowDialog();
        }

        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replacement_for_Damaged_License c=new Replacement_for_Damaged_License();
            c.ShowDialog();
        }

        private void detainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicense d = new DetainLicense();
            d.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense a=new ReleaseDetainedLicense();
            a.ShowDialog();
        }

        private void manageDetainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDetainedLicense c = new ListDetainedLicense();
            c.Show();
        }
    }
}
