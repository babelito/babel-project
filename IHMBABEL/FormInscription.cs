using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Managers;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHMBABEL
{
    public partial class FormInscription : Form
    {

        string photo = "";

        public FormInscription()
        {
            InitializeComponent();
        }

        private void FormInscription_Load(object sender, EventArgs e)
        {
        }

        private void photoBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Séléctionner une photo";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] words = dlg.FileName.Split('\\');
                string fileName = words[words.Length - 1];

                words = fileName.Split('.');
                string fileType = words[words.Length - 1];

                if (fileType == FileFormats.jpg.ToString() || fileType == FileFormats.png.ToString()) {
                    photo = dlg.FileName;
                } else
                {
                    label_passError.Text = "Le format de votre image n'est pas bon";
                }
            }
        }

        private void inscriptionBtn_Click(object sender, EventArgs e)
        { 
            if (textBox_pseudo.Text != "" && textBox_password1.Text != "" && textBox_password2.Text != "" && textBox_prenom.Text != "" && textBox_nom.Text != "" && textBox_mail.Text != "")
            {
                if (textBox_password1.Text.Equals(textBox_password2.Text))
                {
                    UserManager userManager = new UserManager();
                    label_passError.Text = userManager.SetUser(textBox_pseudo.Text, textBox_password1.Text, textBox_prenom.Text, textBox_nom.Text, textBox_mail.Text, photo);
                }
                else
                {
                    label_passError.Text = "Les deux mots de passe ne correspondent pas";
                }
            }
            else
            {
                label_passError.Text = "Un champ obligatoire n'est pas rempli";
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setLabel(string text)
        {
            label_passError.Text = text;
        }
    }
}
