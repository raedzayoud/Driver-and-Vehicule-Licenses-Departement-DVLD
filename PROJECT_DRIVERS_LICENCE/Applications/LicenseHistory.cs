using BunissessLayerDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class LicenseHistory : Form
    {
        private int idPerson;
        private int idApp;
        private string className;
        public LicenseHistory(int idPerson=0, int idApp=0, string className="")
        {
            InitializeComponent();
            this.idPerson = idPerson;
            this.idApp = idApp;
            this.className = className;
        }

        void ModifyColoum()
        {

            // Modify the width of the "ApplicationID" column
            dataGridView1.Columns["LicID"].Width = 100;

            // Modify the width of the "DrivingClass" column
            dataGridView1.Columns["AppID"].Width = 100;

            // Modify the width of the "NationalNo" column
            dataGridView1.Columns["ClassName"].Width = 250;

            // Modify the width of the "FullName" column
            dataGridView1.Columns["IssueDate"].Width = 150;

            // Modify the width of the "ApplicationDate" column
            dataGridView1.Columns["ExpirationDate"].Width = 150;

            dataGridView1.Columns["isActive"].Width = 100;

        }
        void FillColoumDataGrid()
        {
            // Get the user ID based on the person ID
            int idUser = clsUser.FindUserByIDPerson(idPerson).idUser;

            // Get all licenses for the user
            DataTable dt = clsIssueDriving.GetAllLicenseSameUser(idUser);

            // Clear existing columns and rows
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Add columns to the DataGridView
            dataGridView1.Columns.Add("LicID", "LicID");
            dataGridView1.Columns.Add("AppID", "AppID");
            dataGridView1.Columns.Add("ClassName", "ClassName");
            dataGridView1.Columns.Add("IssueDate", "IssueDate");
            dataGridView1.Columns.Add("ExpirationDate", "ExpirationDate");
            dataGridView1.Columns.Add("isActive", "isActive");

            // Modify the columns if necessary
            ModifyColoum();

            // Loop through each DataRow in the DataTable
            foreach (DataRow d in dt.Rows)
            {
                // Create a new row in the DataGridView
                int rowIndex = dataGridView1.Rows.Add();

                // Get the necessary data from the DataRow
                int applicationID = Convert.ToInt32(d["AppID"]);

                string LicenseType = "";
                int idLicenseType = 0;
                clsLocalDrivingLicenseApplication.GetIDLicenseByIDApp(applicationID, ref idLicenseType);
                clsLocalDrivingLicenseApplication.GetLicenseType(idLicenseType, ref LicenseType);
                string className = LicenseType;

                // Set the cell values for the new row
                dataGridView1.Rows[rowIndex].Cells["LicID"].Value = Convert.ToInt32(d["LicenseID"]);
                dataGridView1.Rows[rowIndex].Cells["AppID"].Value = applicationID.ToString();
                dataGridView1.Rows[rowIndex].Cells["ClassName"].Value = className;

                // Convert to DateTime and format the date
                dataGridView1.Rows[rowIndex].Cells["IssueDate"].Value = Convert.ToDateTime(d["IssueDate"]).ToString("dd/MM/yyyy");
                dataGridView1.Rows[rowIndex].Cells["ExpirationDate"].Value = Convert.ToDateTime(d["ExpirationDate"]).ToString("dd/MM/yyyy");

                dataGridView1.Rows[rowIndex].Cells["isActive"].Value = Convert.ToBoolean(d["isActive"]) ? "Active" : "Inactive";
            }
        }

        void ModifyColoumIn()
        {
            // Modify the width of the columns in dataGridView2
            dataGridView2.Columns["Int.LicID"].Width = 100;
            dataGridView2.Columns["AppID"].Width = 100;
            dataGridView2.Columns["IssueDate"].Width = 150;
            dataGridView2.Columns["ExpirationDate"].Width = 150;
            dataGridView2.Columns["isActive"].Width = 100;
        }

        void FillColoumDataGridInternationalLicense()
        {
            // Get all licenses for the user
            DataTable dt = clsInternationalLicense.GetAllIntLicense();

            // Clear existing columns and rows
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            // Add columns to the DataGridView
            dataGridView2.Columns.Add("Int.LicID", "Int.LicID");
            dataGridView2.Columns.Add("AppID", "AppID");
            dataGridView2.Columns.Add("IssueDate", "IssueDate");
            dataGridView2.Columns.Add("ExpirationDate", "ExpirationDate");
            dataGridView2.Columns.Add("isActive", "isActive");
            ModifyColoumIn();

            foreach (DataRow d in dt.Rows)
            {
                int applicationID = Convert.ToInt32(d["ApplicationID"]);
                int idperson = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(applicationID);

                int idUser = clsUser.FindUserByIDPerson(idperson).idUser;
                // Create a new row in the DataGridView
                int rowIndex = dataGridView2.Rows.Add();

                // Get the necessary data from the DataRow
                int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(applicationID);
                string licenseID = clsInternationalLicense.GetInternationalLicense(applicationID).ToString();
                int isActive = clsUser.FindUserByID(idUser).Bit;

                // Set the cell values for the new row
                dataGridView2.Rows[rowIndex].Cells["Int.LicID"].Value = licenseID;
                dataGridView2.Rows[rowIndex].Cells["AppID"].Value = applicationID.ToString();
                dataGridView2.Rows[rowIndex].Cells["IssueDate"].Value = Convert.ToDateTime(d["IssueDate"]).ToString("dd/MM/yyyy");
                dataGridView2.Rows[rowIndex].Cells["ExpirationDate"].Value = Convert.ToDateTime(d["ExpirationDate"]).ToString("dd/MM/yyyy");
                dataGridView2.Rows[rowIndex].Cells["isActive"].Value = isActive;
            }
        }





        private void LicenseHistory_Load(object sender, EventArgs e)
        {
            PersonInformation personInformation = new PersonInformation(this.idPerson);
            this.Controls.Add(personInformation);
            FillColoumDataGrid();
            FillColoumDataGridInternationalLicense();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
