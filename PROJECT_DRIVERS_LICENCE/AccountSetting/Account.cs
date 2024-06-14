using BunissessLayerDVLD;
using PROJECT_DRIVERS_LICENCE.LoginSystem;
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

namespace PROJECT_DRIVERS_LICENCE.AccountSetting
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }
        void ChargerCredentials(string cheminFichier, ref string usernme, ref string password)
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

                    }
                    else
                    {
                        Console.WriteLine("Le fichier est vide.");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors du chargement des données : " + ex.Message);
                //return null;
            }
        }

        private void Account_Load(object sender, EventArgs e)
        {
            string username = "", password = "";
            ChargerCredentials("C:\\Users\\user\\source\\repos\\PROJECT_DRIVERS_LICENCE\\SaveData.txt", ref username, ref password);

            DataTable dt = clsLogin.FindId(username, password);

            if (dt != null && dt.Rows.Count > 0)
            {
                int idUser = Convert.ToInt32(dt.Rows[0]["idUser"]);
                int idPerson = Convert.ToInt32(dt.Rows[0]["idPerson"]);

                UserInformation userInformation = new UserInformation(idPerson, idUser);
                this.Controls.Add(userInformation);
            }
            else
            {
                MessageBox.Show("User information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
