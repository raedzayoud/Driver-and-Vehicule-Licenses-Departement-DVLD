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

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class LocalDrivingLicenseApplication : Form
    {
        private int idPerson;
        public LocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        public LocalDrivingLicenseApplication(int idPerson)
        {
            InitializeComponent();
            this.idPerson = idPerson;
            Load1();

        }

        void Load1()
        {

            comboBox1.Items.Add("none");
            comboBox1.Items.Add("NationalNo");
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
            issueToolStripMenuItem.Enabled = false;
            FillDataGrid();
        }

        void ModifyColoum()
        {

            // Modify the width of the "ApplicationID" column
            dataGridView1.Columns["ApplicationID"].Width = 100;

            // Modify the width of the "DrivingClass" column
            dataGridView1.Columns["DrivingClass"].Width = 250;

            // Modify the width of the "NationalNo" column
            dataGridView1.Columns["NationalNo"].Width = 100;

            // Modify the width of the "FullName" column
            dataGridView1.Columns["FullName"].Width = 200;

            // Modify the width of the "ApplicationDate" column
            dataGridView1.Columns["ApplicationDate"].Width = 130;

            // Modify the width of the "PassedTests" column
            dataGridView1.Columns["PassedTests"].Width = 100;

            // Modify the width of the "Status" column
            dataGridView1.Columns["Status"].Width = 100;


        }

        void FillColoumDataGrid()
        {
            // this is a method from clsNewLocalDriving don t forget to translate to the correct class !
            DataTable dt = clsLocalDrivingLicenseApplication.GetAllData();
            DataTable dt1 = clsLocalDrivingLicenseApplication.GetAllDataofLocal();

            // Clear existing columns in dataGridView1
            dataGridView1.Columns.Clear();

            // Define and add columns to dataGridView1
            dataGridView1.Columns.Add("ApplicationID", "ApplicationID");
            dataGridView1.Columns.Add("DrivingClass", "DrivingClass");
            dataGridView1.Columns.Add("NationalNo", "NationalNo");
            dataGridView1.Columns.Add("FullName", "FullName");
            dataGridView1.Columns.Add("ApplicationDate", "ApplicationDate");
            dataGridView1.Columns.Add("PassedTests", "PassedTests");
            dataGridView1.Columns.Add("Status", "Status");

            /*Modify the width*/

            ModifyColoum();

            // Iterate through the rows of the DataTable and populate dataGridView1
            // Assuming dt and dt1 have the same number of rows and you want to process them in pairs
            int minRowCount = Math.Min(dt.Rows.Count, dt1.Rows.Count); // Get the count of rows from the smaller DataTable

            for (int i = 0; i < minRowCount; i++)
            {
                DataRow row = dt.Rows[i]; // Get the row from the first DataTable
                DataRow row1 = dt1.Rows[i]; // Get the corresponding row from the second DataTable

                // Add a new row to dataGridView1
                int rowIndex = dataGridView1.Rows.Add();
                string idLicense = row1["LicenseClassID"].ToString();

                // Using int.Parse() to parse idLicense
                int parsedIdLicense = int.Parse(idLicense);

                // Initialize name variable
                string name = "";

                // Get license type name using parsedIdLicense
                clsLocalDrivingLicenseApplication.GetLicenseType1(parsedIdLicense, ref name);

                // Set values for each column in the newly added row in dataGridView1
                dataGridView1.Rows[rowIndex].Cells["ApplicationID"].Value = row["ApplicationID"];
                dataGridView1.Rows[rowIndex].Cells["DrivingClass"].Value = name; // Assuming 'name' holds the driving class
                dataGridView1.Rows[rowIndex].Cells["NationalNo"].Value = row["NationalNo"];
                dataGridView1.Rows[rowIndex].Cells["FullName"].Value = row["FullName"];// Assuming rowIndex is already defined elsewhere in your code                                                                   // Convert the value to a DateTime and then format it to string
                dataGridView1.Rows[rowIndex].Cells["ApplicationDate"].Value = ((DateTime)row["ApplicationDate"]).ToString("dd/MM/yyyy");
                dataGridView1.Rows[rowIndex].Cells["PassedTests"].Value = row["Passedtest"]; // Assuming you're setting this to 0 as a default
                dataGridView1.Rows[rowIndex].Cells["Status"].Value = row["Status"];
            }

        }

        void FillDataGrid()
        {
            FillColoumDataGrid();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLocalLicense l = new NewLocalLicense();
            l.ShowDialog();
            FillDataGrid();
        }


        private void cancelApplicatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to cancelled it !", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsLocalDrivingLicenseApplication.ModifyStatus((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("Data is not Updated", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Data is not Updated", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void SearchData()
        {
            FillDataGrid();
            string s = textBox1.Text;
            if (s != "")
            {
                DataTable dt = clsLocalDrivingLicenseApplication.SearchData(comboBox1.SelectedItem.ToString(), s);
                if (dt != null)
                    dataGridView1.Rows.Clear();
                foreach (DataRow d in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    string idLicense = d["LicenseClassID"].ToString();

                    // Using int.Parse()
                    int parsedIdLicense = int.Parse(idLicense);

                    string name = "";
                    // Conversion successful
                    clsLocalDrivingLicenseApplication.GetLicenseType(parsedIdLicense, ref name);

                    dataGridView1.Rows[rowIndex].Cells["ApplicationID"].Value = d["ApplicationID"];
                    dataGridView1.Rows[rowIndex].Cells["DrivingClass"].Value = name;
                    dataGridView1.Rows[rowIndex].Cells["NationalNo"].Value = d["NationalNo"];
                    dataGridView1.Rows[rowIndex].Cells["FullName"].Value = d["FullName"];
                    dataGridView1.Rows[rowIndex].Cells["ApplicationDate"].Value = d["ApplicationDate"];
                    dataGridView1.Rows[rowIndex].Cells["PassedTests"].Value = d["Passedtest"];
                    dataGridView1.Rows[rowIndex].Cells["Status"].Value = d["Status"];
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchData();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int idUser = clsNewLicenseApplication.ClassNewwLicenseApplication((int)dataGridView1.CurrentRow.Cells[0].Value).idUser;
            clsVisionTestAppoinmentscs v = new clsVisionTestAppoinmentscs((int)dataGridView1.CurrentRow.Cells[0].Value, idUser);
            v.Show();
            FillColoumDataGrid();
            FillDataGrid();
        }

        void Load()
        {
            scheduleSToolStripMenuItem.Enabled = false;
            //Written
            scheduleToolStripMenuItem1.Enabled = false;
            //visoion
            toolStripMenuItem3.Enabled = true;
            //ShowLicense
            showLicenseToolStripMenuItem.Enabled = false;
            issueToolStripMenuItem.Enabled = false;
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Cells[0].Value == null)
                {
                    e.Cancel = true;
                    return;
                }

                ResetMenuItems();

                int appID = (int)dataGridView1.CurrentRow.Cells[0].Value;

                int passedTest = clsSheduleTestAppointemets.GetPassedTestByAppID(appID);
                string status = clsLocalDrivingLicenseApplication.GetStatusById(appID);

                if (status == "Completed")
                {
                    scheduleSToolStripMenuItem.Enabled = false;
                    toolStripMenuItem3.Enabled = false;
                    scheduleToolStripMenuItem1.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                    issueToolStripMenuItem.Enabled = false;
                    scheduleToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    cancelApplicatToolStripMenuItem.Enabled = false;
                
                }
                else if (passedTest == 0)
                {
                    scheduleSToolStripMenuItem.Enabled = false;
                    scheduleToolStripMenuItem1.Enabled = false;
                    toolStripMenuItem3.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled = false;
                    issueToolStripMenuItem.Enabled = false;
                }
                else if (passedTest == 1)
                {
                    scheduleSToolStripMenuItem.Enabled = false;
                    toolStripMenuItem3.Enabled = false;
                    scheduleToolStripMenuItem1.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled = false;
                    issueToolStripMenuItem.Enabled = false;
                }
                else if (passedTest == 2)
                {
                    toolStripMenuItem3.Enabled = false;
                    scheduleToolStripMenuItem1.Enabled = false;
                    scheduleSToolStripMenuItem.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled = false;
                    issueToolStripMenuItem.Enabled = false;
                }
                else if (passedTest == 3)
                {
                    issueToolStripMenuItem.Enabled = true;
                    scheduleToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                MessageBox.Show("An error occurred while opening the context menu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void ResetMenuItems()
        {
            scheduleSToolStripMenuItem.Enabled = true;
            toolStripMenuItem3.Enabled = true;
            scheduleToolStripMenuItem1.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;
            issueToolStripMenuItem.Enabled = true;
            scheduleToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicatToolStripMenuItem.Enabled = true;
        }

        private void scheduleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int idUser = clsNewLicenseApplication.ClassNewwLicenseApplication((int)dataGridView1.CurrentRow.Cells[0].Value).idUser;
            WrittenTest t = new WrittenTest((int)dataGridView1.CurrentRow.Cells[0].Value, idUser);
            t.ShowDialog();
            FillDataGrid();
        }

        private void scheduleSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreetTestApponitements s = new StreetTestApponitements((int)dataGridView1.CurrentRow.Cells[0].Value);
            s.ShowDialog();
            FillDataGrid();
        }

        void Delete()
        {
            int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp((int)dataGridView1.CurrentRow.Cells[0].Value);
         
            
                if (clsLocalDrivingLicenseApplication.DeleteApplicationfromTestAppointements(idLocal))
                {
                    if (clsLocalDrivingLicenseApplication.DeleteApplicationfromLocal(idLocal))
                    {
                        if (clsLocalDrivingLicenseApplication.DeleteApplication((int)dataGridView1.CurrentRow.Cells[0].Value))
                        {
                            MessageBox.Show("Delete Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Delete is not Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        if (clsLocalDrivingLicenseApplication.DeleteApplication((int)dataGridView1.CurrentRow.Cells[0].Value))
                        {
                            MessageBox.Show("Delete Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Delete is not Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    if (clsLocalDrivingLicenseApplication.DeleteApplicationfromLocal(idLocal))
                    {
                        if (clsLocalDrivingLicenseApplication.DeleteApplication((int)dataGridView1.CurrentRow.Cells[0].Value))
                        {
                            MessageBox.Show("Delete Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Delete is not Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        
    
        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete it?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Code to delete the application goes here
                if (clsIssueDriving.DeleteLicenseFromApplication((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    Delete();
                }
                else
                {
                    Delete();

                }
            }
        }

        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueDrivingLicense i = new IssueDrivingLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            i.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idUer = clsUser.FindUserByIDPerson(idPerson).idUser;
            ShowLicense s=new ShowLicense(idUer,idPerson,(string)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
            s.ShowDialog();
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // we need to replace the idPerson using app id and user id
            int idperson = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp((int)dataGridView1.CurrentRow.Cells[0].Value);
            int passedTest = clsSheduleTestAppointemets.GetPassedTestByAppID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if (passedTest == 3)
            {
                LicenseHistory l = new LicenseHistory(idperson, (int)dataGridView1.CurrentRow.Cells[0].Value, (string)dataGridView1.CurrentRow.Cells[1].Value);
                l.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry you must paased the exams before !","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void LocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("none");
            comboBox1.Items.Add("NationalNo");
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
            issueToolStripMenuItem.Enabled = false;
            FillDataGrid();

        }

        private void LocalDrivingLicenseApplication_Load_1(object sender, EventArgs e)
        {

        }

        private void LocalDrivingLicenseApplication_Load_2(object sender, EventArgs e)
        {

        }

        private void LocalDrivingLicenseApplication_Load_3(object sender, EventArgs e)
        {

        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationDetails c = new ShowApplicationDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            c.ShowDialog();
        }
    }
}
