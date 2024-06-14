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
    public partial class InernationalDrivingLicenseApplication : Form
    {
        public InernationalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        void ModifyColoumIn()
        {

            // Modify the width of the "ApplicationID" column
            dataGridView1.Columns["Int.LicID"].Width = 150;

            // Modify th2 width of the "DrivingClass" column
            dataGridView1.Columns["AppID"].Width = 150;

            // Modify th2 width of the "FullName" column
            dataGridView1.Columns["IssueDate"].Width = 200;

            // Modify th2 width of the "ApplicationDate" column
            dataGridView1.Columns["ExpirationDate"].Width = 180;

            dataGridView1.Columns["isActive"].Width = 150;

        }

        void FillColoumDataGridInternationalLicense()
        {
            // Get all licenses for the user
            DataTable dt = clsInternationalLicense.GetAllIntLicense();

            // Clear existing columns and rows
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Add columns to the DataGridView
            dataGridView1.Columns.Add("Int.LicID", "Int.LicID");
            dataGridView1.Columns.Add("AppID", "AppID");
            dataGridView1.Columns.Add("IssueDate", "IssueDate");
            dataGridView1.Columns.Add("ExpirationDate", "ExpirationDate");
            dataGridView1.Columns.Add("isActive", "isActive");
            ModifyColoumIn();

            foreach (DataRow d in dt.Rows)
            {
                int applicationID = Convert.ToInt32(d["ApplicationID"]);
                int idperson = clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(applicationID);

                int idUser = clsUser.FindUserByIDPerson(idperson).idUser;
                // Create a new row in the DataGridView
                int rowIndex = dataGridView1.Rows.Add();

                // Get the necessary data from the DataRow
                int idLocal = clsLocalDrivingLicenseApplication.GetLocalDrivingApplicationByIdApp(applicationID);
                string licenseID = clsInternationalLicense.GetInternationalLicense(applicationID).ToString();
                int isActive = clsUser.FindUserByID(idUser).Bit;

                // Set the cell values for the new row
                dataGridView1.Rows[rowIndex].Cells["Int.LicID"].Value = licenseID;
                dataGridView1.Rows[rowIndex].Cells["AppID"].Value = applicationID.ToString();
                dataGridView1.Rows[rowIndex].Cells["IssueDate"].Value = Convert.ToDateTime(d["IssueDate"]).ToString("dd/MM/yyyy");
                dataGridView1.Rows[rowIndex].Cells["ExpirationDate"].Value = Convert.ToDateTime(d["ExpirationDate"]).ToString("dd/MM/yyyy");
                dataGridView1.Rows[rowIndex].Cells["isActive"].Value = isActive;
            }
        }


        private void InernationalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            FillColoumDataGridInternationalLicense();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InternaltionalLicenseApplication i = new InternaltionalLicenseApplication();
            i.ShowDialog();
            FillColoumDataGridInternationalLicense();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Assurez-vous que la cellule n'est pas nulle
                if (dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    // Essayez de convertir la valeur en int de manière sûre
                    int idApp;
                    if (int.TryParse(dataGridView1.CurrentRow.Cells[1].Value.ToString(), out idApp))
                    {
                        // Continuez avec le reste de votre logique si la conversion réussit
                        clsPerson p1 = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));
                        People.ShowClinetList p = new People.ShowClinetList(p1.idPerson);
                        p.ShowDialog();
                    }
                    else
                    {
                        // Gérer le cas où la conversion échoue
                        MessageBox.Show("La valeur dans la cellule n'est pas un entier valide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Gérer le cas où la cellule est nulle
                    MessageBox.Show("La cellule sélectionnée est vide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les autres exceptions potentielles
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Assurez-vous que la cellule n'est pas nulle
                if (dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    // Essayez de convertir la valeur en int de manière sûre
                    int idApp;
                    if (int.TryParse(dataGridView1.CurrentRow.Cells[1].Value.ToString(), out idApp))
                    {
                        ShowInternationalLicense i = new ShowInternationalLicense(idApp);
                        i.ShowDialog();
                    }
                    else
                    {
                        // Gérer le cas où la conversion échoue
                        MessageBox.Show("La valeur dans la cellule n'est pas un entier valide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Gérer le cas où la cellule est nulle
                    MessageBox.Show("La cellule sélectionnée est vide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les autres exceptions potentielles
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void showPersonHistoryDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Assurez-vous que la cellule n'est pas nulle
                if (dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    // Essayez de convertir la valeur en int de manière sûre
                    int idApp;
                    if (int.TryParse(dataGridView1.CurrentRow.Cells[1].Value.ToString(), out idApp))
                    {
                        clsPerson p1 = clsPerson.FindPersonByID(clsLocalDrivingLicenseApplication.GetIdPersonByIDUSERByIDApp(idApp));

                        LicenseHistory h = new LicenseHistory(p1.idPerson,idApp);
                        h.ShowDialog();
                    }
                    else
                    {
                        // Gérer le cas où la conversion échoue
                        MessageBox.Show("La valeur dans la cellule n'est pas un entier valide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Gérer le cas où la cellule est nulle
                    MessageBox.Show("La cellule sélectionnée est vide.", "Erreur de conversion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Gérer les autres exceptions potentielles
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
    }
}
