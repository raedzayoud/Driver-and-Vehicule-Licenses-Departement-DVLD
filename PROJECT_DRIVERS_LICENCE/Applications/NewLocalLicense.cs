using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class NewLocalLicense : Form
    {
        private clsPerson p;
        private clsPerson person;
        private clsUser user;
        private bool test = false;
        private int id;
        private int idUser;
        private enum enMode { add = 0, update = 1 };
        enMode _mode;

        public NewLocalLicense()
        {
            InitializeComponent();
        }
        public NewLocalLicense(int id)
        {
            InitializeComponent();
            if (id == -1)
            {
               _mode= enMode.add;
            }
        }
        void _LoadData()
        {
            if (_mode == enMode.add)
            {
                person = new clsPerson();

                // Instantiate personInformation1
                PersonInformation person1 = new PersonInformation();

                // Add personInformation1 to tabPage1 controls
                tabPage1.Controls.Add(person1);
                return;
            }

        }

        void _FillComboBox()
        {
            List<string> elementos = new List<string> { "NationalNo" };
            comboBox1.Items.AddRange(elementos.ToArray());
        }

        void LoadLocalLicense()
        {
            _FillComboBox();
            comboBox1.SelectedIndex = 0;
            FillComboBoxLicense();
            comboBox2.SelectedIndex = 2;
            label7.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            _LoadData();
        }

        private void LocalLicense_Load(object sender, EventArgs e)
        {
            LoadLocalLicense();
        }
        bool isFindNationalNo()
        {
            p = clsPerson.FindPersonByNational(textBox1.Text);
            if (p != null)
            {
                return true;

            }
            return false;
        }

        void FillComboBoxLicense()
        {
            // Récupérer les données de la DataTable LicenseType
            DataTable dtLicenseType = clsNewLicenseApplication.GetLicenseClass();

            // Assurez-vous que la ComboBox est vide avant de remplir
            comboBox2.Items.Clear();

            // Parcourir les lignes de la DataTable et ajouter chaque valeur à la ComboBox
            foreach (DataRow row in dtLicenseType.Rows)
            {
                // Supposons que la colonne contenant les types de licence s'appelle "LicenseTypeName"
                string licenseTypeName = row["LicenseType"].ToString();
                comboBox2.Items.Add(licenseTypeName);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            p = clsPerson.FindPersonByNational(textBox1.Text);
            if (clsNewLicenseApplication.isUserOrNot(p.idPerson))
            {
                tabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show("This is not a user in the system","Defect",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isFindNationalNo())
            {
                tabPage1.Controls.RemoveByKey("PersonInformation");

                PersonInformation personInformation2 = new PersonInformation(textBox1.Text);

                tabPage1.Controls.Add(personInformation2);
                int fees = 0;
                label11.Text=clsNewLicenseApplication.GetFees(fees).ToString();
                if (clsUser.FindUserByIDPerson(p.idPerson) != null)
                    label12.Text = clsUser.FindUserByIDPerson(p.idPerson).username.ToString();
                else
                    MessageBox.Show("this is not a user i can t give you the access Sorry ! ", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Defect there is no NationalNo", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabPage1.Controls.RemoveByKey("PersonInformation");
                PersonInformation personInformation1 = new PersonInformation();

                // Add personInformation1 to tabPage1 controls
                tabPage1.Controls.Add(personInformation1);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isContainsOrNotLicense(int idUser, int idLicense)
        {
            DataTable dt = clsNewLicenseApplication.GetIdLicenseClass(idUser);
            DataTable dt1 = clsNewLicenseApplication.GetallStatus(idUser);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow d = dt.Rows[i];
                DataRow d1 = dt1.Rows[i];

                // Use Equals method to compare objects properly
                if (d["LicenseClassID"].Equals(idLicense) && d1["Status"].ToString() == "New")
                {
                    return true;
                }
                if(d["LicenseClassID"].Equals(idLicense) && d1["Status"].ToString() == "Completed")
                {
                    return true;
                }
            }

            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clsNewLicenseApplication c=new clsNewLicenseApplication();
            if (DateTime.TryParse(label7.Text, out DateTime applicationDate))
            {
                c.ApplicationDate = applicationDate;
            }
            int id = 0;
            clsNewLicenseApplication.GetIDOfLicenseClass(ref id,comboBox2.SelectedItem.ToString());
           
            if (isContainsOrNotLicense(clsUser.FindUserByIDPerson(p.idPerson).idUser,id))
            {
                MessageBox.Show("The user is have aleardy this License ! please choose another one", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            c.idLicense = id;
            c.idApplicationType = 1;
            c.idUser = clsUser.FindUserByIDPerson(p.idPerson).idUser;
            c.Status = "New";
            if (c.Save())
            {
                if (clsLocalDrivingLicenseApplication.AddLocal(c.idApplication, c.idLicense))
                {
                    MessageBox.Show("Data Stored Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label5.Text = c.idApplication.ToString();
                }
                else
                {
                    MessageBox.Show("Data is not Stored", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Data is not Stored", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }
    }
}
