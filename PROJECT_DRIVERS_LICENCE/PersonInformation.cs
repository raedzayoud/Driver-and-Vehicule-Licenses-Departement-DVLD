using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BunissessLayerDVLD;

namespace PROJECT_DRIVERS_LICENCE
{
    public partial class PersonInformation : UserControl
    {
        private int _id; // Add a field to store the id
        private string _NationalNo;
        private clsPerson p;
        private clsCountry c;
        public PersonInformation()
        {
            InitializeComponent();
        }

        // Add a constructor that accepts an id parameter
        public PersonInformation(int id) : this()
        {
            _id = id;
            Load_DATA(); // Call the method to load data based on the id
        }
        public PersonInformation(string  NationalNo)
        {
            _NationalNo = NationalNo;
            InitializeComponent();
           // Load_DataNa(_NationalNo);


        }
        public void Load_DataNa(string _NationalNo)
        {
            p = clsPerson.FindPersonByNational(_NationalNo);
            if (p != null)
            {
                label12.Text = p.idPerson.ToString();
                label13.Text = p.FirstName + " " + p.SecondName + " " + p.LastName;
                label14.Text = p.NationalNo;
                label15.Text = p.Gender;
                label16.Text = p.Email;
                label17.Text = p.Address;
                label18.Text = p.DateofBirth.ToString();
                label19.Text = p.Phone;
                int index = p.NationalityCountry;
                c = clsCountry.FindCountryById(index);
                label20.Text = c.CountryName;
                if (p.ImagePath != "")
                {
                    pictureBox1.Load(p.ImagePath);
                }


            }
        }
            private void Load_DATA()
        {
            // Assuming p is declared somewhere in your class
            p = clsPerson.FindPersonByID(_id);
            if (p != null)
            {
                label12.Text = p.idPerson.ToString();
                label13.Text = p.FirstName + " " + p.SecondName + " " + p.LastName;
                label14.Text = p.NationalNo;
                label15.Text = p.Gender;
                label16.Text = p.Email;
                label17.Text = p.Address;
                label18.Text = p.DateofBirth.ToString();
                label19.Text = p.Phone;
                int index = p.NationalityCountry;
                c =clsCountry.FindCountryById(index);
                label20.Text = c.CountryName;
                if (p.ImagePath != "")
                {
                    pictureBox1.Load(p.ImagePath);
                }
            }
        }

        private void PersonInformation_Load(object sender, EventArgs e)
        {
            if (_NationalNo == "")
                Load_DATA();
            else
                Load_DataNa(_NationalNo);


        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewPerson p = new AddNewPerson(_id);
            p.ShowDialog();
        }
    }
}
