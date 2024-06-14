using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunissessLayerDVLD;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROJECT_DRIVERS_LICENCE.People
{
    public partial class AddNewPerson : Form
    {
        public delegate void DataBackEventHandler(object sender,int personID);
        
        
        public event DataBackEventHandler DataBack;                            
        private enum enMode { Add=0,Update=1};                                 
        enMode _Mode;                                                          
        private clsPerson person;                                              
        private int _idPerson;                                                 
        public AddNewPerson(int id)                                            
        {
            InitializeComponent();
            _idPerson = id;
            if (id == -1)
            {
                _Mode = enMode.Add;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            DataTable dt=clsPerson.GetAllPeople();
            string text=textBox2.Text;
            bool test = false;
            foreach(DataRow row in dt.Rows)
            {
                if (row["NationalNo"].ToString() == text)
                {
                    test = true;
                    break;
                }
            }
            if (test)
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "this NationalNo is used");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, "");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.person_man__1_;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.admin_female;
        }

        private bool _Find(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == '@')
                {
                    return true;
                }
            }
            return false;

        }

      
        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            string text = textBox7.Text;
            if (!_Find(text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox7, "This textBox is not contains @");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(textBox7, "");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...
            }
        }
        //button Save
        private void button2_Click(object sender, EventArgs e)
        {
            person.FirstName = textBox1.Text.ToString(); // Trim to remove leading and trailing whitespaces
            
            person.LastName = textBox3.Text.ToString();
            
            person.SecondName = textBox4.Text.ToString();
            
            person.NationalNo = textBox2.Text.ToString();
          
            person.Email = textBox7.Text.ToString();
          
            if (radioButton1.Checked)
            {
                person.Gender = "Male";
            }
            else
            {
                person.Gender = "Female";
            }

            string country = comboBox1.SelectedItem?.ToString() ?? ""; // This assumes comboBox1 has a selected item
            int id = 1;
            if (clsCountry.GetIDOftheCountry(country, ref id))
            {
                person.NationalityCountry = id;
            }
            person.Address = textBox8.Text.ToString();
           
            // Ensure pictureBox1.ImageLocation is not null before accessing it
            if (pictureBox1.ImageLocation != null)
            {
                person.ImagePath = pictureBox1.ImageLocation.ToString();
            }
            else
            {
                person.ImagePath = ""; // Set a default value if ImagePath is not available
            }
            person.DateofBirth = DateTime.Parse(dateTimePicker1.Text);
            // we need to modify the full name 
            /*
             wirte code 
             */
            if (clsPerson.updatePersonOfuser(person.idPerson,person.FirstName,person.SecondName,person.LastName))
            {
                person.Phone = textBox6.Text.ToString();

                // Assuming person.Save() method saves the person's data and returns a boolean indicating success
                if (person.Save())
                {
                    MessageBox.Show("Data Stored Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label14.Text = person.idPerson.ToString();
                    DataBack?.Invoke(this, person.idPerson);
                }
                else
                {
                    MessageBox.Show("Data is not Stored", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _Mode = enMode.Update;
            }
        }

        void _FillDataCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["countryName"]);

            }

        }

        void _LoadData()
        {
            _FillDataCountries();
            comboBox1.SelectedIndex = 0;
            if (_Mode == enMode.Add)
            {
                label1.Text = "Add New Person";
                person=new clsPerson();
                return;
            }
            person = clsPerson.FindPersonByID(_idPerson);
            if(person == null)
            {
                MessageBox.Show("this Form is not found !","Defect",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            label1.Text = "Edit Person " + _idPerson;

            textBox1.Text= person.FirstName; 

            textBox3.Text= person.LastName;

            textBox4.Text= person.SecondName;

            textBox2.Text = person.NationalNo;

            textBox7.Text= person.Email;

            if (person.Gender == "Male")
            {
              radioButton1.Checked=true;
            }
            else if(person.Gender =="Female")
            {
              radioButton2.Checked=true;
            }
            textBox6.Text = person.Phone;
            textBox8.Text = person.Address;
           
            dateTimePicker1.Value = person.DateofBirth;
            
            if (person.ImagePath != "")
            {
                pictureBox1.Load(person.ImagePath);
            }
            _Mode = enMode.Update;
            comboBox1.SelectedIndex = comboBox1.FindString(clsCountry.FindCountryById(person.NationalityCountry).CountryName);
        }


        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            // Selects the first item in the list
            _LoadData();

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "You must write your FirstName");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");
            }
        }
      

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "You must write your SecondName");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox4, "");
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "You must write your LastName");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox3, "");
            }
        }

     
    }
    }
