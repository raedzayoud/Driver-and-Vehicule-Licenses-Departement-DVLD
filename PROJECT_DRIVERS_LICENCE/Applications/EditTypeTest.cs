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
    public partial class EditTypeTest : Form
    {
        int _id;
        public EditTypeTest(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void EditTypeTest_Load(object sender, EventArgs e)
        {
            clsTestType c = clsTestType.GetAllInformationOfApplication(_id);
            label6.Text = _id.ToString();
            textBox1.Text = c.Title;
            textBox2.Text = c.Description.ToString();
            textBox3.Text=c.Fees.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            string Description= textBox2.Text;
            int fees = Convert.ToInt32(textBox3.Text);
            if (clsTestType.isUpdate(_id, title, fees,Description))
            {
                MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Data is not Updated", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
