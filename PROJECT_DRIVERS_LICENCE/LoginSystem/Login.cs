using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.Users;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PROJECT_DRIVERS_LICENCE.LoginSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
           // checkBox1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (clsLogin.isCorrect(username, password))
            {
                if (checkBox1.Checked) { SauvegarderCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", username, password);  } else { SauvegarderCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", "", ""); }
                this.Visible = false;
                WindowPricipale.All all = new WindowPricipale.All();
                all.ShowDialog();
            }
            else
            {
                MessageBox.Show("Your Information are not correct", "Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SauvegarderCredentials(string cheminFichier, string username, string password)
        {
            try
            {
                // Écriture des données utilisateur dans le fichier
                using (StreamWriter sw = new StreamWriter(cheminFichier))
                {
                    sw.WriteLine(username + "," + password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la sauvegarde des données : " + ex.Message);
            }
        }


        bool ChargerCredentials(string cheminFichier, ref string usernme, ref string password)
        {
            try
            {
                // Lecture des données utilisateur depuis le fichier
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        // Séparation de la ligne en username et password
                        string[] credentials = line.Split(',');
                        usernme = credentials[credentials.Length - 2].ToString();
                        password = credentials[credentials.Length - 1].ToString();
                        if(usernme!="" && password!="")
                        return true;
                        else
                        return false;

                    }
                    else
                    {
                        Console.WriteLine("Le fichier est vide.");
                        return false;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors du chargement des données : " + ex.Message);
                //return null;
            }

            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                SauvegarderCredentials("SaveData.txt", username, password);
            }
            else
            {
                SauvegarderCredentials("SaveData.txt", "", "");

            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            string username1 = "", password1 = "";
            if (ChargerCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", ref username1, ref password1))
            {
                checkBox1.Checked = true;
                textBox1.Text = username1;
                textBox2.Text = password1;
            }
            else
            {
                checkBox1.Checked = false;
                textBox1.Text = username1;
                textBox2.Text = password1;
            }


            
            
            /*Method to do the id of the Account*/


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
