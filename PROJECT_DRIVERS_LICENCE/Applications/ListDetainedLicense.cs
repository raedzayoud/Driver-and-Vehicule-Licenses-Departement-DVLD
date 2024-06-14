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
    public partial class ListDetainedLicense : Form
    {
        public ListDetainedLicense()
        {
            InitializeComponent();
        }

        void ModifyColoum()
        {
            // Modify the width of the columns based on the names defined in FillColoumDataGrid
            dataGridView1.Columns["D.ID"].Width = 100;
            dataGridView1.Columns["L.ID"].Width = 100;
            dataGridView1.Columns["D.Date"].Width = 100;
            dataGridView1.Columns["isReleased"].Width = 120;
            dataGridView1.Columns["FineFees"].Width = 130;
            dataGridView1.Columns["ReleaseDate"].Width = 130;
            dataGridView1.Columns["N.No"].Width = 100;
            dataGridView1.Columns["FullName"].Width = 170;
            dataGridView1.Columns["ReleaseAppID"].Width = 100;
        }

        void FillColoumDataGrid()
        {
            DataTable dt = clsDetain.GetAllDetain();

            // Clear existing columns in dataGridView1
            dataGridView1.Columns.Clear();

            // Define and add columns to dataGridView1
            dataGridView1.Columns.Add("D.ID", "D.ID");
            dataGridView1.Columns.Add("L.ID", "L.ID");
            dataGridView1.Columns.Add("D.Date", "D.Date");
            dataGridView1.Columns.Add("isReleased", "isReleased");
            dataGridView1.Columns.Add("FineFees", "FineFees");
            dataGridView1.Columns.Add("ReleaseDate", "ReleaseDate");
            dataGridView1.Columns.Add("N.No", "N.No");
            dataGridView1.Columns.Add("FullName", "FullName");
            dataGridView1.Columns.Add("ReleaseAppID", "ReleaseAppID");

            // Modify the width of the columns
            ModifyColoum();

            foreach (DataRow row in dt.Rows)
            {
                DataTable dt1 = clsReleaseApp.GetReleaseofDetain(Convert.ToInt32(row["DetainID"]));

                // Add a new row to dataGridView1
                int rowIndex = dataGridView1.Rows.Add();
                int idApp = 0;

                DataTable dt2 = clsIssueDriving.GetLicenseByIdLicenseID(Convert.ToInt32(row["LicenseID"]));

                if (dt2.Rows.Count > 0)
                {
                    idApp = Convert.ToInt32(dt2.Rows[0]["AppID"]);
                }

                int personId = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp);
                clsPerson p = clsPerson.FindPersonByID(personId);
                clsUser u = clsUser.FindUserByIDPerson(p.idPerson);

                // Set values for each column in the newly added row in dataGridView1
                dataGridView1.Rows[rowIndex].Cells["D.ID"].Value = row["DetainID"];
                dataGridView1.Rows[rowIndex].Cells["L.ID"].Value = row["LicenseID"];
                dataGridView1.Rows[rowIndex].Cells["D.Date"].Value = Convert.ToDateTime(row["DebutDate"]).ToString("dd/MM/yyyy");
                dataGridView1.Rows[rowIndex].Cells["FineFees"].Value = row["FineFees"];
                dataGridView1.Rows[rowIndex].Cells["N.No"].Value = p.NationalNo;
                dataGridView1.Rows[rowIndex].Cells["FullName"].Value = u.FullName;

                if (dt1.Rows.Count > 0)
                {
                    dataGridView1.Rows[rowIndex].Cells["ReleaseAppID"].Value = dt1.Rows[0]["ReleaseAppID"];
                    dataGridView1.Rows[rowIndex].Cells["isReleased"].Value = dt1.Rows[0]["isRelease"];
                    dataGridView1.Rows[rowIndex].Cells["ReleaseDate"].Value = Convert.ToDateTime(dt1.Rows[0]["ReleaseDate"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells["ReleaseAppID"].Value = DBNull.Value;
                    dataGridView1.Rows[rowIndex].Cells["isReleased"].Value = false;
                    dataGridView1.Rows[rowIndex].Cells["ReleaseDate"].Value = DBNull.Value;
                }
            }
        }



        private void ListDetainedLicense_Load(object sender, EventArgs e)
        {
            FillColoumDataGrid();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetainLicense l=new DetainLicense();
            l.ShowDialog();
            FillColoumDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense r=new ReleaseDetainedLicense();
            r.ShowDialog();
            FillColoumDataGrid();
        }
    }
}
