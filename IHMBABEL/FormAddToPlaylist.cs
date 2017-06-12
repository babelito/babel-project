using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHMBABEL.Classes.Users;
using IHMBABEL.Classes.Managers;

namespace IHMBABEL
{
    public partial class FormAddToPlaylist : Form
    {
        private int idFileConcerned;
        public FormAddToPlaylist(int idFile)
        {
            InitializeComponent();
            idFileConcerned = idFile;
            PlaylistManager pm = new PlaylistManager();
            List<Playlist> listPlaylist = pm.GetPlaylists(ConnectedUser.user.IdUser);
            if(listPlaylist.Count() == 0)
            {
                showCreateOne();
            }else
            {
                foreach (Playlist p in listPlaylist)
                {
                    bool showPlaylist = true;
                    foreach (BabelFiles file in p.List)
                    {
                        if (file.IdFile == idFileConcerned)
                        {
                            showPlaylist = false;
                        }
                    }
                    if (showPlaylist)
                    {
                        ComboBoxItem cbi = new ComboBoxItem();
                        cbi.Text = p.Name;
                        cbi.Value = p.IdPlaylist;
                        combo_playlist.Items.Add(cbi);
                    }
                }
                if(combo_playlist.Items.Count == 0)
                {
                    showCreateOne();
                }
            }
        }

        public void showCreateOne()
        {
            lb_playlist.Visible = false;
            combo_playlist.Visible = false;
            btn_add.Visible = false;
            btn_createOne.Visible = true;
            lb_error.Text = "Vous ne possedez aucune playlist pouvant contenir ce fichier";
            
        }

        public void btn_add_Click(object sender, EventArgs e)
        {
            if (combo_playlist.SelectedItem != null)
            {
                PlaylistManager pm = new PlaylistManager();
                pm.AddToPlaylist(idFileConcerned, (combo_playlist.SelectedItem as ComboBoxItem).Value);
                this.Close();
            }
        }

        public void btn_createOne_Click(object sender, EventArgs e)
        {
            FormAddNewPlaylist form = new FormAddNewPlaylist();
            form.ShowDialog();
            this.Close();
        }
    }
}
