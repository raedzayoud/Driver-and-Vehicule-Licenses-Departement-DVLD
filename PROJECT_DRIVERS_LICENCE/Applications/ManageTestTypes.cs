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
    public partial class ManageTestTypes : Form
    {
        public ManageTestTypes()
        {
            InitializeComponent();
        }
        void FillDataTest()
        {
            dataGridView1.DataSource = clsTestType.GetDataofTest();
        }
       
        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            FillDataTest();

            // Set width for each column
            dataGridView1.Columns["ID"].Width = 100;
            dataGridView1.Columns["Title"].Width = 150;
            dataGridView1.Columns["Fees"].Width = 100;
            dataGridView1.Columns["Description"].Width = 250;

        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTypeTest edit = new EditTypeTest((int)dataGridView1.CurrentRow.Cells[0].Value);
            edit.ShowDialog();
            FillDataTest();
        }
    }
}
