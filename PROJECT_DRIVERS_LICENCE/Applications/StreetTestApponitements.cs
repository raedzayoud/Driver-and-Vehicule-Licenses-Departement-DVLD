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
    public partial class StreetTestApponitements : Form
    {
        private int _idApp;
        private static int nb = 0;
        public StreetTestApponitements(int idApp)
        {
            InitializeComponent();
            this._idApp = idApp;
        }

        void LoadDataGrid()
        {
            dataGridView1.Columns.Clear();

            // Define and add columns to dataGridView1 (this part is correct as is)
            dataGridView1.Columns.Add("AppointementID", "AppointementID");
            dataGridView1.Columns.Add("AppointementDate", "AppointementDate");
            dataGridView1.Columns.Add("PaidFees", "PaidFees");
            dataGridView1.Columns.Add("isLocked", "isLocked");
            int idLicense = 0;
            clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(_idApp, ref idLicense);

            // Retrieve appointments data (assuming GetAllAppointents is corrected as in my previous response)
            DataTable dt = clsStreetTest.GetAllTestAppointementsofTest3(_idApp, idLicense);

            // Populate the DataGridView with data from the DataTable
            // Ensure your DataGridView has columns defined either through the designer or programmatically

            foreach (DataRow dt1 in dt.Rows)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();

                // Create and add cells to the row
                row.Cells.Add(new DataGridViewTextBoxCell { Value = dt1["TestAppointementsID"] });
                row.Cells.Add(new DataGridViewTextBoxCell
                {
                    Value = Convert.ToDateTime(dt1["AppointementDate"]).ToString("dd/MM/yyyy")
                }); row.Cells.Add(new DataGridViewTextBoxCell { Value = "35" });
                row.Cells.Add(new DataGridViewCheckBoxCell { Value = dt1["isLocaked"] }); // Assuming 'isLocked' is a boolean

                // Add the row to the DataGridView
                dataGridView1.Rows.Add(row);

            }


        }

        private void StreetTestApponitements_Load(object sender, EventArgs e)
        {
            DrivingLicense d = new DrivingLicense(_idApp);
            this.Controls.Add(d);
            LoadDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int appointmentId;
            int LocalID;
            bool isLocked = false;

            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                // Convert the value in the cell to an integer
                appointmentId = (int)dataGridView1.CurrentRow.Cells[0].Value;

                // Call the clsSheduleTestAppointemets.GetLocaked method with the appointmentId
                isLocked = clsSheduleTestAppointemets.GetLocaked(appointmentId);
            }
            LocalID = clsSheduleTestAppointemets.GETLocalDrivingID(_idApp);
            string Reslut = clsSheduleTestAppointemets.GetResult(clsSheduleTestAppointemets.SelectMaxTestAppointementsIDByLocalID(LocalID));
            int TestType1 = 0;
            clsWrittenTest.GetTestType(clsSheduleTestAppointemets.GETLocalDrivingID(_idApp), ref TestType1);
            if (Reslut == "Pass" && TestType1 == 3)
            {
                MessageBox.Show("Sorry this User pass this Test Successfully !", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Reslut == "Pass" && TestType1 == 2)
            {
                // remove the first Vistion Appointemnts 
                clsWrittenTest.DeleteTestAppointements(clsSheduleTestAppointemets.GETLocalDrivingID(_idApp));
            }

            if (!clsSheduleTestAppointemets.isFoundorNot(_idApp) && isLocked == false)
            {
                SheduleStreet s = new SheduleStreet(_idApp);
                s.Show();
                LoadDataGrid();
            }
            else if (Reslut == "Fail" && isLocked == true)
            {
                // Assuming _idApp is already defined elsewhere in your code
                int appId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                DateTime appointmentDate = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);

                SheduleStreet s = new SheduleStreet(_idApp, appointmentDate, appId, 3);
                s.ShowDialog();
                LoadDataGrid();
            }
            else
            {
                MessageBox.Show("Sorry this user have aleardy an Apponitements !", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void Test()
        {
            bool isLocked = clsSheduleTestAppointemets.GetLocaked((int)dataGridView1.CurrentRow.Cells[0].Value);
            int idLocal = clsSheduleTestAppointemets.GETLocalDrivingID(_idApp);
            //i used this idLocal to return the all Result in db
            DataTable Result1 = clsSheduleTestAppointemets.GetResults(idLocal);

            if (isLocked == false)
            {
                foreach (DataRow row in Result1.Rows)
                {
                    foreach (DataColumn column in Result1.Columns)
                    {
                        if (row[column].ToString() == "Fail")
                        {
                            // Assuming _idApp is already defined elsewhere in your code
                            int appId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                            DateTime appointmentDate = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);

                            SheduleStreet s = new SheduleStreet(_idApp, appointmentDate, appId, 4);
                            s.ShowDialog();
                            LoadDataGrid();
                            return;
                           
                        }
                    }
                }
            }
            

            if (isLocked == false)
            {
                int appId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                DateTime appointmentDate = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);

                SheduleStreet s = new SheduleStreet(_idApp, appointmentDate, appId, -1);
                s.ShowDialog();
                LoadDataGrid();
                return;
            }
            if (isLocked == true)
            {
                int appId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                DateTime appointmentDate = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);

                SheduleStreet s = new SheduleStreet(_idApp, appointmentDate, appId, 2);
                s.ShowDialog();
                LoadDataGrid();
                return;
            }
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestStreet t = new  TakeTestStreet(_idApp, (int)dataGridView1.CurrentRow.Cells[0].Value);
            t.ShowDialog();
            LoadDataGrid();


        }
    }
}
