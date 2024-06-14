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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROJECT_DRIVERS_LICENCE.Users
{
    public partial class Users : Form
    {
        private System.Windows.Forms.ComboBox c1;
        public Users()
        {
            InitializeComponent();
        }
        void FillComboxbox()
        {
            List<string> l = new List<string>() { "None", "idUser", "idPerson", "username","fullname", "isActive" };

            // Adding items from the list to comboBox1
            foreach (string item in l)
            {
                comboBox1.Items.Add(item);
            }

        }
       
        void FillComboxbox2()
        {
            List<string> l = new List<string>() {"ALL","YES","NO"};

            // Adding items from the list to comboBox1
            foreach (string item in l)
            {
                comboBox2.Items.Add(item);
            }

        }

        void FillDataGrid()
        {
            // Set the data source for the DataGridView
            dataGridView1.DataSource = clsUser.GetAllUser();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Get the ID from the data source (assuming it's stored in a column named "idPerson")
                object idObj = row.Cells["idPerson"].Value;

                // Check if the cell value is not null and can be converted to a string
                if (idObj != null && idObj != DBNull.Value)
                {
                    string id = idObj.ToString();

                    // Parse the ID to an integer
                    if (int.TryParse(id, out int personID))
                    {
                        // Assuming clsPerson is a class with a static method FindPersonByID
                        clsPerson person = clsPerson.FindPersonByID(personID);

                        if (person != null)
                        {
                            // Assuming person has properties FirstName, LastName, and SecondName
                            string firstName = person.FirstName;
                            string lastName = person.LastName;
                            string secondName = person.SecondName;

                            // Set the value of the "FullName" column to the person's name
                            row.Cells["FullName"].Value = $"{firstName} {secondName} {lastName}";
                        }
                        else
                        {
                            // Handle case where person is not found
                            row.Cells["FullName"].Value = "Person Not Found";
                        }
                    }
                    else
                    {
                        // Handle case where ID cannot be parsed as an integer
                        row.Cells["FullName"].Value = "Invalid ID";
                    }
                }

            }


        }

        private void Users_Load(object sender, EventArgs e)
        {
            FillComboxbox();
            comboBox1.SelectedIndex = 0;
            comboBox2.Visible = false;
            textBox1.Visible = false;
            FillDataGrid();
            dataGridView1.Columns["IdUser"].Width = 80;
            dataGridView1.Columns["IdPerson"].Width = 80;
            dataGridView1.Columns["Username"].Width = 130;
            dataGridView1.Columns["isActive"].Width = 120;
            dataGridView1.Columns["FullName"].Width = 200;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewUser user = new AddNewUser();
            user.ShowDialog();
            FillDataGrid();
        }

        void SearchData()
        {
            FillDataGrid();
            comboBox2.Visible = false;
            textBox1.Visible = true;
            string s = textBox1.Text;
            if (s != "")
            {
                dataGridView1.DataSource = clsUser.Search(comboBox1.SelectedItem.ToString(), s);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
               SearchData();   
        }
        void SelectCombox()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
                comboBox2.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                textBox1.Visible = false;
                comboBox2.Visible = true;
                FillComboxbox2();
                comboBox2.SelectedIndex = 0;
                
            }
            else
            {
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCombox();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           dataGridView1.DataSource =clsUser.GererCombox(comboBox2.SelectedItem.ToString());
            
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password p = new Password((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value) ;
            p.ShowDialog();
            FillDataGrid();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowUserList showUser = new ShowUserList((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
            showUser.ShowDialog();
            FillDataGrid();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser user = new AddNewUser((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
            user.ShowDialog();
            FillDataGrid();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete this User","Question",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK) {

                if (clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Delete Failed", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                FillDataGrid();

            }
        }
    }
}
