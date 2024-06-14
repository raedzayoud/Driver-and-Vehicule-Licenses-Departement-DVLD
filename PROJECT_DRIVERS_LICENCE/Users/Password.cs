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

namespace PROJECT_DRIVERS_LICENCE.Users
{
    public partial class Password : Form
    {
        private int idPerson;
        private int idUser;
        public Password(int idPerson, int idUser)
        {
            InitializeComponent();
            this.idPerson = idPerson;
            this.idUser = idUser;
        }

        void PersonInformation()
        {
            PersonInformation personInformation = new PersonInformation(idPerson);
            this.Controls.Add(personInformation);

        }

        void LoginInformation()
        {
            clsUser user = clsUser.FindUserByID(idUser);
            UserLabel.Text =idUser.ToString();
            isActiveLabel.Text = user.Bit.ToString();
            usernameLabel.Text = user.username.ToString();
        }

        private void Password_Load(object sender, EventArgs e)
        {
            PersonInformation();
            LoginInformation();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string password = clsUser.FindUserByID(idUser).password;
            if (password != textBox1.Text)
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1,"this password is not correct");
            }
            else{
                e.Cancel=false;
                errorProvider1.SetError(textBox1, "");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if(textBox2.Text!=textBox3.Text)
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "this password is not the same with the new password");
            }
            else
            {
                e.Cancel=false;
                errorProvider1.SetError(textBox3, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsUser.UpdatatePassword(idUser,textBox3.Text))
            {
                MessageBox.Show("Password change Successfully", "Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password Defect", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
