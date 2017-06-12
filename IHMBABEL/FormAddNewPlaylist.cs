using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHMBABEL.Classes.Managers;

namespace IHMBABEL
{
    public partial class FormAddNewPlaylist : Form
    {
        public FormAddNewPlaylist()
        {
            InitializeComponent();
        }

        private void btn_validate_Click(object sender, EventArgs e)
        {
            if(tb_name.Text == "")
            {
                lb_error.Text = "Entrez un nom pour creer une playlist";
            }
            else if(tb_theme.Text == ""){
                lb_error.Text = "Entrez un theme pour creer une playlist";
            }else
            {
                int visibility = 0;
                PlaylistManager pm = new PlaylistManager();
                if(cb_visibility.CheckState == CheckState.Checked)
                {
                    visibility = 1;
                }
                pm.NewPlaylist(tb_name.Text, tb_theme.Text, visibility);
                this.Close();
            }
        }
    }
}
