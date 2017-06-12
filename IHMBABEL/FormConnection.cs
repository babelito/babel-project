using IHMBABEL.Classes.Managers;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHMBABEL
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void connexionButton_Click(object sender, EventArgs e)
        {
            if (textBox_username.Text.ToString() != "" && textBox_password.Text.ToString() != "")
            {
                string commandPseudoPass = @"SELECT * FROM users WHERE pseudo = '" + textBox_username.Text + "'" + " AND password = '" + textBox_password.Text + "'";

                ConnectionManager cm = new ConnectionManager();
                User user = cm.GetUser(new SqlCommand(commandPseudoPass));
                if (!user.IdUser.Equals(0))
                {
                    ConnectedUser.user = user;
                    button_connexion.DialogResult = DialogResult.OK;
                } else
                {
                    label_status.Text = "Le nom d'utilisateur et/ou le mot de passe que vous avez entrés ne correspondent pas.";
                }

            } else
            {
                label_status.Text = "Un champ n'est pas rempli.";
            }
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inscirptionBtn_Click(object sender, EventArgs e)
        {
            FormInscription form = new FormInscription();
            form.ShowDialog();
        }
    }
}
