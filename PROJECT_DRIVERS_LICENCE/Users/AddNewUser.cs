using BunissessLayerDVLD;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.People;

namespace PROJECT_DRIVERS_LICENCE.Users
{
    public partial class AddNewUser : Form
    {
        private clsPerson p;
        private clsPerson person;
        private clsUser user;
        private bool test = false;
        private int id;
        private int idUser;
        private enum enMode {add=0,update=1};
        enMode _mode;
        public AddNewUser()
        {
            InitializeComponent();
            _mode= enMode.add;
        }
       
        public AddNewUser(int id,int idUser)
        {
            InitializeComponent();
            if (id == -1)
            {
                _mode = enMode.add;
            }
            else
            {
                _mode=enMode.update;
            }
            this.id = id;
            this.idUser = idUser;
        }

        void _LoadData()
        {
            if (_mode == enMode.add)
            {
                label1.Text = "Add New User";
                person = new clsPerson();

                // Instantiate personInformation1
                PersonInformation person1= new PersonInformation();

                // Add personInformation1 to tabPage1 controls
                tabPage1.Controls.Add(person1);
                return;
            }
            label1.Text = "Edit User";
            _FillComboBox(); // Call _FillComboBox() method to populate the combobox
            comboBox1.SelectedIndex = 0;

            // Instantiate tabPage1 if it's not already instantiated
            if (tabPage1 == null)
            {
                tabPage1 = new TabPage();
            }
            
            PersonInformation personInformation1 = new PersonInformation(id);
            tabPage1.Controls.Add(personInformation1);
            
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;
            label2.Enabled = false;
            // Now we need to access to user than to fill the username and password
            user=clsUser.FindUserByID(idUser);
            label8.Text=user.idUser.ToString();
            textBox3.Text = user.username;
            textBox4.Text=user.password;
            textBox5.Text = user.password;
            if (user.Bit == 1)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked=false;
            }

        }

        void _FillComboBox()
        {
            List<string> elementos = new List<string> { "NationalNo"};
            comboBox1.Items.AddRange(elementos.ToArray());
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            _FillComboBox();
            comboBox1.SelectedIndex = 0;
            _LoadData();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        bool isFindNationalNo()
        {
          p =clsPerson.FindPersonByNational(textBox1.Text);
            if(p != null)
            {
                return true;

            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isFindNationalNo())
            {
                test = true;
                tabPage1.Controls.RemoveByKey("PersonInformation");

                PersonInformation personInformation2 = new PersonInformation(textBox1.Text);

                tabPage1.Controls.Add(personInformation2);

                personInformation2.Load_DataNa(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Defect there is no NationalNo", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabPage1.Controls.RemoveByKey("PersonInformation");
                test = false;
                PersonInformation personInformation1 = new PersonInformation();

                // Add personInformation1 to tabPage1 controls
                tabPage1.Controls.Add(personInformation1);

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = clsUser.GetAllUser();
            foreach (DataRow row in dt.Rows)
            {
                clsPerson p;
                string nationalNo = "";
                if (int.TryParse(row["idPerson"].ToString(), out int id))
                {
                    // Call FindPersonByID method with the integer parameter
                    p = clsPerson.FindPersonByID(id);
                    nationalNo = p.NationalNo;
                    // Use p as needed
                }
                if (nationalNo == textBox1.Text)
                {
                    MessageBox.Show("This User is alerady exist", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }
            if (test)
            {
                tabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show("User not Found","Defect",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            if (_mode == enMode.add)
            {
                clsUser user = new clsUser();
                user.username = textBox3.Text;
                user.idPerson = clsPerson.FindPersonByNational(p.NationalNo).idPerson;
                string name = clsPerson.FindPersonByNational(p.NationalNo).FirstName + " " + clsPerson.FindPersonByNational(p.NationalNo).SecondName + " " + clsPerson.FindPersonByNational(p.NationalNo).LastName;
                user.FullName = name;
                if (textBox4.Text == textBox5.Text)
                {
                    user.password = textBox4.Text;
                }

                if (checkBox1.Checked)
                {
                    user.Bit = 1;
                }
                else
                {
                    user.Bit = 0;
                }
                if (user.Save())
                {
                    MessageBox.Show("Data Stored Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label8.Text = user.idUser.ToString();
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("Data is not Stored", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                user.username = textBox3.Text.ToString();
                user.idPerson = clsPerson.FindPersonByID(id).idPerson;
                string name = clsPerson.FindPersonByID(id).FirstName + " " + clsPerson.FindPersonByID(id).SecondName + " " + clsPerson.FindPersonByID(id).LastName;
                user.FullName = name;
                if (textBox4.Text == textBox5.Text)
                {
                    user.password = textBox4.Text;
                }

                if (checkBox1.Checked)
                {
                    user.Bit = 1;
                }
                else
                {
                    user.Bit = 0;
                }
                if (user.Save())
                {
                    MessageBox.Show("Data Stored Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label8.Text = user.idUser.ToString();
                }
                else
                {
                    MessageBox.Show("Data is not Stored", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text!=textBox5.Text)
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox5, "You Password is not correct Please check your password");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox5, "");
            }
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            AddNewPerson p = new AddNewPerson(-1);
            p.DataBack += Form2_DataBack;
            p.ShowDialog();
        }
        private void Form2_DataBack(object sender, int personID)
        {
            tabPage1.Controls.RemoveByKey("PersonInformation");

            PersonInformation personInformation2 = new PersonInformation(personID);

            tabPage1.Controls.Add(personInformation2);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
