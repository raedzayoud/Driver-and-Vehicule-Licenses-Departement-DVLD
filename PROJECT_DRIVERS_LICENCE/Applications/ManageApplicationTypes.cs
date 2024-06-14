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

namespace PROJECT_DRIVERS_LICENCE.Applications
{
    public partial class ManageApplicationTypes : Form
    {
        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        void FillDataGrid()
        {
            dataGridView1.DataSource = clsApplicationTypes.GetApplicationTypes();
        }

        private void ManageApplicationTypes_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            

            // Set width for each column
            dataGridView1.Columns["ID"].Width = 100; 
            dataGridView1.Columns["Title"].Width = 250;
            dataGridView1.Columns["Fees"].Width = 100; 
                                                       

        }

        private void editApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationType edit=new EditApplicationType((int)dataGridView1.CurrentRow.Cells[0].Value);
            edit.ShowDialog();
            FillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
