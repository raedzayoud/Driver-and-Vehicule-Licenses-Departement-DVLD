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
    public partial class ShowApplicationDetails : Form
    {
        private int idApp;
        public ShowApplicationDetails(int idApp)
        {
            InitializeComponent();
            this.idApp = idApp;
        }

        private void ShowApplicationDetails_Load(object sender, EventArgs e)
        {
            DrivingLicense d = new DrivingLicense(idApp);
            this.Controls.Add(d);
        }

        
    }
}
