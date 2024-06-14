using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunissessLayerDVLD;
namespace PROJECT_DRIVERS_LICENCE.People
{
    public partial class People : Form
    {
        public People()
        {
            InitializeComponent();
        }

        private void _LoadDataofPeople()
        {
            dataGridView1.DataSource = clsPerson.GetAllPeople();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                _LoadDataofPeople();
        }
        private void People_Load_1(object sender, EventArgs e)
        {
            _FillComboBox();
            _LoadDataofPeople();
            comboBox1.SelectedIndex = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
            }



        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddNewPerson p=new AddNewPerson(-1);
            p.ShowDialog();
            _LoadDataofPeople();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson p=new AddNewPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            p.ShowDialog();
            _LoadDataofPeople();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete this Person","???",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)== DialogResult.OK)
            {
               if(clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value)){
                    MessageBox.Show("Deleting Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadDataofPeople();
               }
                else
                {
                    MessageBox.Show("Deleting is not Successfully !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void showClinetListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowClinetList p = new ShowClinetList((int)dataGridView1.CurrentRow.Cells[0].Value);
            p.ShowDialog();
        }

        void _FillComboBox()
        {
            List<string> elementos = new List<string> { "None","idPerson", "FirstName", "SecondName", "LastName", "NationalNo",  "Gender", "Adress", "Phone", "Email","NationatlityCountry" };
            comboBox1.Items.AddRange(elementos.ToArray());

        }

        void SearchData()
        {
            _LoadDataofPeople();
            textBox1.Visible = true;
            string s = textBox1.Text;
            if (s != "")
            {
                dataGridView1.DataSource = clsPerson.Search(comboBox1.SelectedItem.ToString(), s);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                _LoadDataofPeople();
                textBox1.Visible = false;

            }
            if (comboBox1.SelectedIndex == 1)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 6)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 7)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 8)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
            if (comboBox1.SelectedIndex == 9)
            {
                _LoadDataofPeople();
                textBox1.Visible = true;
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson p = new AddNewPerson(-1);
            p.ShowDialog();
        }
    }
}
