using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_DRIVERS_LICENCE.People
{
    public partial class ShowClinetList : Form
    {
        int _id;
        private PersonInformation personInformation1;
        public ShowClinetList(int id)
        {
            InitializeComponent();
            _id = id;
            personInformation1 = new PersonInformation(_id);
           // personInformation1.Dock = DockStyle.Fill; // Dock the user control to fill the form
            Controls.Add(personInformation1); // Add the user control to the form's controls
        }

        private void ShowClinetList_Load(object sender, EventArgs e)
        {
            personInformation1 = new PersonInformation(_id);
            // personInformation1.Dock = DockStyle.Fill; // Dock the user control to fill the form
            Controls.Add(personInformation1); // Add the user control to the form's controls

        }
    }
}
