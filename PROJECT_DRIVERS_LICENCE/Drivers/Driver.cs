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

namespace PROJECT_DRIVERS_LICENCE.Drivers
{
    public partial class Driver : Form
    {
        public Driver()
        {
            InitializeComponent();
        }

        void ModifyColoum()
        {

            // Modify the width of the "ApplicationID" column
            dataGridView1.Columns["DriverID"].Width = 100;

            // Modify the width of the "DrivingClass" column
            dataGridView1.Columns["PersonID"].Width = 100;

            // Modify the width of the "NationalNo" column
            dataGridView1.Columns["NationalNo"].Width = 100;

            // Modify the width of the "FullName" column
            dataGridView1.Columns["FullName"].Width = 200;

            // Modify the width of the "PassedTests" column
            dataGridView1.Columns["Active Licenses"].Width = 100;

        }
        void FillColoumDataGrid()
        {
            DataTable dt = clsDriver.GetAllDriver();

            // Clear existing columns in dataGridView1
            dataGridView1.Columns.Clear();

            // Define and add columns to dataGridView1
            dataGridView1.Columns.Add("DriverID", "DriverID");
            dataGridView1.Columns.Add("PersonID", "PersonID");
            dataGridView1.Columns.Add("NationalNo", "NationalNo");
            dataGridView1.Columns.Add("FullName", "FullName");
            dataGridView1.Columns.Add("Active Licenses", "Active Licenses");

            // Modify column widths
            ModifyColoum();

            // Loop through each row in the DataTable
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                // Check if PersonID already exists in the DataGridView
                bool personExists = false;
                foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                {
                    if (dgRow.Cells["PersonID"].Value != null && Convert.ToInt32(dgRow.Cells["PersonID"].Value) == Convert.ToInt32(row["idPerson"]))
                    {
                        personExists = true;
                        break;
                    }
                }
                if (personExists)
                {
                    continue;
                }

                // Add a new row to dataGridView1
                int rowIndex = dataGridView1.Rows.Add();

                clsPerson p = clsPerson.FindPersonByID(Convert.ToInt32(row["idPerson"]));
                clsUser u = clsUser.FindUserByIDPerson(p.idPerson);

                // Set values for each column in the newly added row in dataGridView1
                dataGridView1.Rows[rowIndex].Cells["DriverID"].Value = row["DriverID"];
                dataGridView1.Rows[rowIndex].Cells["PersonID"].Value = p.idPerson;
                dataGridView1.Rows[rowIndex].Cells["NationalNo"].Value = p.NationalNo;
                dataGridView1.Rows[rowIndex].Cells["FullName"].Value = u.FullName;
                dataGridView1.Rows[rowIndex].Cells["Active Licenses"].Value = row["ActiveLicenses"];
            }
        }



        void FillDataGrid()
        {
            FillColoumDataGrid();

        }


        private void Drivers_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
