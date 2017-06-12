using IHMBABEL.Classes.Files.FileTypes;
using IHMBABEL.Classes.Managers;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace IHMBABEL
{
    public partial class FormBabelAccueil : Form
    {

        #region WindowsFormMethods
        private string fileType = "";
        private string fileFormat = "";
        private bool listViewSearchUserSorted = true;
        private bool[] listViewSearchFileSorted = { true, false, false, false, false };
        private bool[] listViewSearchPlaylistsSorted = { true, false, false, false };

        public FormBabelAccueil()
        {
            InitializeComponent();
            this.Visible = false;
            FormConnection formConnection = new FormConnection();
            formConnection.ShowDialog();
            if (formConnection.DialogResult == DialogResult.OK)
            {
                this.Visible = true;
            }
            else
            {
                this.Close();
            }
            setColorLayout();
            setConnectedUserLayout();
        }

        private void setColorLayout()
        {
            pn_category.BackColor = ColorTranslator.FromHtml("#282828");
            pn_searchBar.BackColor = ColorTranslator.FromHtml("#282828");
            pn_profile.BackColor = ColorTranslator.FromHtml("#282828");
            pn_babel.BackColor = ColorTranslator.FromHtml("#282828");
        }

        private void setConnectedUserLayout()
        {
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            byte[] fileData = request.DownloadData(ConnectedUser.user.Pic);
            pictureBoxUserConnected.Image = ByteToImage(fileData);
            pictureBoxUserConnected.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUserConnected.Location = new System.Drawing.Point((pn_profile.Width / 2) - (pictureBoxUserConnected.Width / 2), 5);
            lb_Name.Text = ConnectedUser.user.Pseudo;
            lb_Name.BackColor = ColorTranslator.FromHtml("#4f4d4b");
            lb_Name.Location = new System.Drawing.Point((pn_profile.Width / 2) - (lb_Name.Width / 2) - 4, pictureBoxUserConnected.Height + 8);
            lb_myFiles.BackColor = ColorTranslator.FromHtml("#4f4d4b");
            lb_myFiles.Location = new System.Drawing.Point((pn_profile.Width / 2) - (lb_myFiles.Width / 2) - 4, pictureBoxUserConnected.Width - 5 + lb_Name.Height * 2);
        }

        public void ClearBody()
        {
            pn_result.Controls.Clear();
        }

        private void btnVideo_click(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "videos";
            DisplayBody_Trendies("video");
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "musiques";
            DisplayBody_Trendies("song");
        }

        private void btnPictures_click(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "photos";
            DisplayBody_Trendies("photo");
        }

        private void btnBook_click(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "livres";
            DisplayBody_Trendies("book");
        }

        private void btnPlaylists_click(object sender, EventArgs e)
        {
            ClearBody();
            DisplayBody_UserPlaylists(ConnectedUser.user.IdUser);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "tendances";
            DisplayBody_Trendies("");
        }

        private void lb_titles_Click(object sender, EventArgs e)
        {
            ClearBody();
            pn_result.Tag = "tendances";
            DisplayBody_Trendies("");
        }

        private void btn_addToPlaylist_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            FormAddToPlaylist form = new FormAddToPlaylist(Convert.ToInt32(btn.Tag));
            form.ShowDialog();
        }
        #endregion


        #region BabelCrewMethods

        private void DisplayBody_User(string author)
        {

            string commandPseudoPass = @"SELECT * FROM users WHERE pseudo = '" + author + "'";
            ConnectionManager cm = new ConnectionManager();
            User user = cm.GetUser(new SqlCommand(commandPseudoPass));

            PictureBox pb = new PictureBox();
            Label pseudo = new Label();
            Label mail = new Label();
            Label account_created = new Label();
            pn_result.Controls.Clear();

            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            byte[] fileData = request.DownloadData(string.Format(user.Pic));
            pb.Image = ByteToImage(fileData);

            pb.Size = new Size(300, 300);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Location = new Point(((pn_result.Width / 2) / 2) - (pb.Width / 2), 100);
            pseudo.Text = "Utilisateur : " + user.Pseudo;
            pseudo.BackColor = Color.White;
            pseudo.AutoSize = true;
            pseudo.Location = new Point(pb.Location.X, pb.Location.Y + 310);
            mail.Text = "Mail enregistrer : " + user.Mail;
            mail.BackColor = Color.White;
            mail.Location = new Point(pb.Location.X, pseudo.Location.Y + 20);
            mail.AutoSize = true;
            account_created.Text = "Compte crée le : " + user.AccountCreated.ToShortDateString();
            account_created.BackColor = Color.White;
            account_created.Location = new Point(pb.Location.X, mail.Location.Y + 20);
            account_created.AutoSize = true;
            pn_result.Controls.Add(pb);
            pn_result.Controls.Add(pseudo);
            pn_result.Controls.Add(mail);
            pn_result.Controls.Add(account_created);

            //Fichier le plus vue
            string commandBestNotation = @"SELECT TOP 1 idFile, title, notation, nbViews, pseudo, fileLanguage, fileOwner, f.picture FROM files as f, users as u WHERE u.idUser = f.fileOwner AND f.fileOwner = '" + user.IdUser + "' ORDER BY notation DESC";
            FileManager fm = new FileManager();
            List<BabelFiles> bestFile = fm.GetFiles(new SqlCommand(commandBestNotation));

            Panel panel = new Panel();
            PictureBox pictureBoxe = new PictureBox();

            pn_result.Controls.Add(panel);
            panel.Size = new System.Drawing.Size(300, 300);
            panel.Location = new System.Drawing.Point((((pn_result.Width / 2) / 2) * 3) - (panel.Width / 2), pb.Location.Y);
            panel.Name = string.Format("body_panels_{0}", 0);
            panel.BackColor = ColorTranslator.FromHtml("#282828");
            panel.BorderStyle = BorderStyle.Fixed3D;
            panel.TabIndex = 0;

            pictureBoxe.Size = new System.Drawing.Size(258, 197);
            pictureBoxe.Location = new System.Drawing.Point((panel.Width / 2) - (pictureBoxe.Width / 2), 0);
            pictureBoxe.SizeMode = PictureBoxSizeMode.Zoom;

            using (WebClient request2 = new WebClient())
            {
                request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                byte[] fileData2 = request2.DownloadData(string.Format("{0}", bestFile.ElementAt(0).Picture.ToString()));
                pictureBoxe.Image = ByteToImage(fileData2);
            }

            pictureBoxe.Name = string.Format("body_pictureBox_{0}", 0);
            pictureBoxe.TabIndex = 1;
            pictureBoxe.TabStop = false;
            panel.Controls.Add(pictureBoxe);

            Label[] labels = new Label[4];
            labels[0] = new Label();
            labels[1] = new Label();
            labels[2] = new Label();
            labels[3] = new Label();
            labels[0].Text = bestFile.ElementAt(0).Title.ToString();
            if (bestFile.ElementAt(0).Notation == -2)
            {
                labels[2].Text = string.Format("Pas encore de note!");
            }
            else
            {
                labels[2].Text = string.Format("Notation : {0}", bestFile.ElementAt(0).Notation);
            }
            labels[3].Text = string.Format("{0} vues", bestFile.ElementAt(0).NbViews.ToString());

            for (int j = 0; j < labels.Length; j++)
            {
                labels[j].Font = new System.Drawing.Font("Verdana", 12F);
                labels[j].Name = string.Format("label_DisplayBox{0}_{1}", 0, j);
                labels[j].TextAlign = ContentAlignment.TopCenter;
                labels[j].AutoSize = true;
                labels[j].Dock = DockStyle.None;
                labels[j].TabIndex = 2 + 0 + j;
                labels[j].BringToFront();

                if (labels[j].Text.Length > 30)
                {
                    labels[j].Text = labels[j].Text.Substring(0, 30) + "...";
                }

                if (j == 0)
                {
                    labels[j].Location = new System.Drawing.Point(0 - 2, pictureBoxe.Location.Y + pictureBoxe.Width - 40);

                    labels[j].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                    labels[j].BackColor = ColorTranslator.FromHtml("#4f4d4b");
                    labels[j].BorderStyle = BorderStyle.Fixed3D;
                    labels[j].ForeColor = Color.White;
                    labels[j].Size = new System.Drawing.Size(panel.Size.Width, 50);
                    labels[j].AutoSize = true;
                    labels[j].MinimumSize = new System.Drawing.Size(panel.Size.Width, 0);
                    labels[j].Tag = bestFile.ElementAt(0).IdFile;
                    labels[j].Click += (sender, e) => DisplayBody_SpecificFile(sender, e);
                }
                else if (j == 2)
                {
                    labels[j].ForeColor = Color.White;
                    labels[j].Location = new System.Drawing.Point(0, pictureBoxe.Location.Y + pictureBoxe.Width + (labels[j - 1].Height * 2) - 40);
                }
                else if (j == 3)
                {
                    labels[j].ForeColor = Color.White;
                    labels[j].Location = new System.Drawing.Point(210, labels[j - 1].Location.Y);
                }
                panel.Controls.Add(labels[j]);
            }

            pn_result.Controls.Add(panel);

            Label file = new Label();
            file.Text = "Fichier télécharger le plus vue : " + bestFile[0].Title;
            file.BackColor = Color.White;
            file.AutoSize = true;
            file.Size = new System.Drawing.Size(100, 100);
            file.Location = new Point(panel.Location.X, pb.Location.Y + 310);

            pn_result.Controls.Add(file);

            pn_result.Show();

        }

        private void DisplayBody_Trendies(string currentPage)
        {
            pn_result.Hide();
            //On écrit bievenue dans le menu actuel
            TextBox titleSection = new TextBox();
            titleSection.Size = new Size(pn_result.Width, pn_result.Height);
            titleSection.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
            titleSection.Text = string.Format("Bienvenue sur la page des {0}", pn_result.Tag);
            titleSection.BringToFront();
            titleSection.MaximumSize = new Size(700, 300);
            titleSection.Location = new Point((pn_result.Width / 2) - (titleSection.Width / 2), pn_searchBar.Location.Y + titleSection.Height * 2);
            titleSection.Font = new System.Drawing.Font("Verdana", 25F);
            titleSection.ForeColor = Color.Black;
            titleSection.Name = "Title";
            titleSection.TextAlign = HorizontalAlignment.Center;
            titleSection.ReadOnly = true;

            pn_result.Controls.Add(titleSection);

            //On commence l'affichage dynamique des panels
            List<BabelFiles> listFiles = GetTrendys(currentPage).GetRange(0, 4);
            Panel[] panels = new Panel[listFiles.Count];
            PictureBox[] pictureBoxes = new PictureBox[listFiles.Count];

            for (int i = 0; i < panels.Length; i++)
            {
                if (i == 0)
                {
                    panels[i] = new Panel();
                    pn_result.Controls.Add(panels[i]);
                    panels[i].Size = new System.Drawing.Size(280, 300);
                    panels[i].Location = new System.Drawing.Point((pn_result.Width / 2) - (((panels[i].Width * 4) + 20) / 2), (pn_result.Height / 2) - (panels[i].Height / 2));
                    panels[i].Name = string.Format("body_panels_{0}", i);
                    panels[i].BackColor = ColorTranslator.FromHtml("#282828");
                    panels[i].BorderStyle = BorderStyle.Fixed3D;
                    panels[i].TabIndex = 0;
                }
                else
                {
                    panels[i] = new Panel();
                    pn_result.Controls.Add(panels[i]);
                    panels[i].Location = new System.Drawing.Point(panels[i - 1].Location.X + panels[i - 1].Size.Width + 5, panels[i - 1].Location.Y);
                    panels[i].Name = string.Format("body_panels_{0}", i);
                    panels[i].BackColor = ColorTranslator.FromHtml("#282828");
                    panels[i].BorderStyle = BorderStyle.Fixed3D;
                    panels[i].Size = new System.Drawing.Size(280, 300);
                    panels[i].TabIndex = i;
                }

                pictureBoxes[i] = new PictureBox();
                pictureBoxes[i].Size = new System.Drawing.Size(258, 197);
                pictureBoxes[i].Location = new System.Drawing.Point((panels[i].Width / 2) - (pictureBoxes[i].Width / 2), 0);
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                    byte[] fileData = request.DownloadData(string.Format("{0}", listFiles.ElementAt(i).Picture.ToString()));
                    pictureBoxes[i].Image = ByteToImage(fileData);
                }

                pictureBoxes[i].Name = string.Format("body_pictureBox_{0}", i);
                pictureBoxes[i].TabIndex = 1 + i;
                pictureBoxes[i].TabStop = false;
                panels[i].Controls.Add(pictureBoxes[i]);

                Label[] labels = new Label[4];
                labels[0] = new Label();
                labels[1] = new Label();
                labels[2] = new Label();
                labels[3] = new Label();
                labels[0].Text = listFiles.ElementAt(i).Title.ToString();
                labels[1].Text = listFiles.ElementAt(i).FileOwner.Pseudo.ToString();
                if (listFiles.ElementAt(i).Notation == -2)
                {
                    labels[2].Text = string.Format("Pas encore de note!");
                }
                else
                {
                    labels[2].Text = string.Format("Notation : {0}", listFiles.ElementAt(i).Notation);
                }
                labels[3].Text = string.Format("{0} vues", listFiles.ElementAt(i).NbViews.ToString());

                for (int j = 0; j < labels.Length; j++)
                {
                    labels[j].Font = new System.Drawing.Font("Verdana", 12F);
                    labels[j].Name = string.Format("label_DisplayBox{0}_{1}", i, j);
                    labels[j].TextAlign = ContentAlignment.TopCenter;
                    labels[j].AutoSize = true;
                    labels[j].Dock = DockStyle.None;
                    labels[j].TabIndex = 2 + i + j;
                    labels[j].BringToFront();

                    if (labels[j].Text.Length > 30)
                    {
                        labels[j].Text = labels[j].Text.Substring(0, 30) + "...";
                    }

                    if (j == 0)
                    {
                        labels[j].Location = new System.Drawing.Point(0 - 2, pictureBoxes[i].Location.Y + pictureBoxes[i].Size.Height + 5);

                        labels[j].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                        labels[j].BackColor = ColorTranslator.FromHtml("#4f4d4b");
                        labels[j].BorderStyle = BorderStyle.Fixed3D;
                        labels[j].ForeColor = Color.White;
                        labels[j].Size = new System.Drawing.Size(panels[0].Size.Width, 50);
                        labels[j].AutoSize = true;
                        labels[j].MinimumSize = new System.Drawing.Size(panels[0].Size.Width, 0);
                        labels[j].Tag = listFiles.ElementAt(i).IdFile;
                        labels[j].Click += (sender, e) => DisplayBody_SpecificFile(sender, e);
                    }
                    else if (j == 1)
                    {
                        labels[j].Location = new System.Drawing.Point(0 - 2, labels[j - 1].Location.Y + labels[j - 1].Size.Height + 5);

                        labels[j].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                        labels[j].BackColor = ColorTranslator.FromHtml("#4f4d4b");
                        labels[j].BorderStyle = BorderStyle.Fixed3D;
                        labels[j].ForeColor = Color.White;
                        labels[j].Size = new System.Drawing.Size(panels[0].Size.Width, 50);
                        labels[j].AutoSize = true;
                        labels[j].MinimumSize = new System.Drawing.Size(panels[0].Size.Width, 0);
                        labels[j].Click += (sender, e) => DisplayBody_User(labels[1].Text);
                    }
                    else if (j == 2)
                    {
                        labels[j].ForeColor = Color.White;
                        labels[j].Location = new System.Drawing.Point(pictureBoxes[0].Location.X, labels[j - 1].Location.Y + labels[j - 1].Size.Height + 5);
                    }
                    else
                    {
                        labels[j].ForeColor = Color.White;
                        labels[j].Location = new System.Drawing.Point(panels[0].Location.X + panels[i].Size.Width - labels[j].Size.Width, labels[j - 1].Location.Y);
                    }
                    panels[i].Controls.Add(labels[j]);
                }
            }
            pn_result.Show();
        }

        private List<BabelFiles> GetTrendys(string currentPage)
        {
            List<BabelFiles> listFiles = new List<BabelFiles>();
            FileManager manager = new FileManager();
            StringBuilder builder = new StringBuilder("");

            builder.Append(string.Format(@"SELECT TOP 4 idFile, title, notation, nbViews, pseudo, fileLanguage, fileOwner, f.picture 
                                            FROM files f, users u 
                                            WHERE u.idUser = f.fileOwner 
                                            AND f.visibility = 1"));

            if (currentPage != "")
            {
                builder.Clear();
                builder.Append(string.Format(@"SELECT TOP 4 idFile, title, notation, nbViews, pseudo, fileLanguage, fileOwner, f.picture 
                                                FROM files f, users u, {0} 
                                                WHERE u.idUser = f.fileOwner 
                                                AND f.visibility = 1 
                                                AND {0}.id{0} = f.idFile", currentPage));

            }

            if (currentPage == "subs")
            {
                builder.Clear();
                builder.Append(string.Format(@"SELECT TOP 4 idFile, title, notation, nbViews, pseudo, fileLanguage, fileOwner, f.picture 
                                                FROM files f, users u, subscribers s
	                                            WHERE (s.idUser_subscriber = {0})
	                                            AND u.idUser = s.idUser_subscriber
                                                AND f.visibility = 1", ConnectedUser.user.IdUser));
            }
            builder = builder.Append(" ORDER BY notation DESC");

            SqlCommand command = new SqlCommand(builder.ToString());

            listFiles = manager.GetFiles(command);

            return listFiles;
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void DisplayBody_SpecificFile(object sender, EventArgs e)
        {
            try
            {
                fileType = "";
                Label txtBox = (Label)sender;
                int idFile = Convert.ToInt32(txtBox.Tag.ToString());
                ClearBody();
                if (fileType == "")
                {
                    FileManager manager = new FileManager();
                    manager.IncrementNbViews(idFile);
                    StringBuilder builder = new StringBuilder(string.Format(@"SELECT TOP 1 idFile, idBook as book, idVideo as video, idPhoto as photo, idSong as song
                                                             FROM files, book, video, song, photo
                                                             WHERE idFile = {0}
                                                             AND(
                                                             idFile = idBook
                                                             OR idFile = idVideo
                                                             OR idFile = idPhoto
                                                             OR idFile = idSong)", idFile));

                    SqlCommand command = new SqlCommand(builder.ToString());
                    fileType = manager.GetFileType(command, idFile);
                }
                ConnectionManager connection = new ConnectionManager();
                Book book = null;
                Song song = null;
                Video video = null;
                Picture photo = null;
                BabelFiles file = null;

                switch (fileType)
                {
                    case "book":
                        book = connection.GetAdditionalBookInfo(fileType, idFile);
                        file = new BabelFiles(book.IdFile, book.Title, book.Directory, book.FileFormat.ToString(), book.DateCreated, book.Notation, book.Visibility, book.Genre, book.FileLanguage.ToString(), book.Theme, book.NbViews, book.Picture, book.FileOwner);
                        fileType = "Book";
                        break;
                    case "song":
                        song = connection.GetAdditionalSongInfo(fileType, idFile);
                        file = new BabelFiles(song.IdFile, song.Title, song.Directory, song.FileFormat.ToString(), song.DateCreated, song.Notation, song.Visibility, song.Genre, song.FileLanguage.ToString(), song.Theme, song.NbViews, song.Picture, song.FileOwner);
                        fileType = "Sound";
                        break;
                    case "video":
                        video = connection.GetAdditionalVideoInfo(fileType, idFile);
                        file = new BabelFiles(video.IdFile, video.Title, video.Directory, video.FileFormat.ToString(), video.DateCreated, video.Notation, video.Visibility, video.Genre, video.FileLanguage.ToString(), video.Theme, video.NbViews, video.Picture, video.FileOwner);
                        fileType = "Video";
                        break;
                    case "photo":
                        photo = connection.GetAdditionalPhotoInfo(fileType, idFile);
                        file = new BabelFiles(photo.IdFile, photo.Title, photo.Directory, photo.FileFormat.ToString(), photo.DateCreated, photo.Notation, photo.Visibility, photo.Genre, photo.FileLanguage.ToString(), photo.Theme, photo.NbViews, photo.Picture, photo.FileOwner);
                        fileType = "Picture";
                        break;
                }
                fileFormat = file.FileFormat.ToString();
                pn_result.Tag = file.Directory;
                pn_result.Hide();

                //Titre de la section
                Label titleSection = new Label();
                titleSection.Size = new Size(pn_result.Width, 30);
                titleSection.Font = new System.Drawing.Font("Verdana", 16F);
                titleSection.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                titleSection.Location = new Point(-1, 5);
                titleSection.Text = string.Format("{0}", file.Title);
                titleSection.BorderStyle = BorderStyle.Fixed3D;
                titleSection.BackColor = ColorTranslator.FromHtml("#4f4d4b");
                titleSection.ForeColor = Color.White;
                titleSection.BringToFront();
                titleSection.MaximumSize = new Size(pn_result.Width, pn_result.Height);
                titleSection.Name = "Title";
                titleSection.TextAlign = ContentAlignment.TopCenter;
                pn_result.Controls.Add(titleSection);

                //Conteneur
                Label lb_info = new Label();
                lb_info.Size = new Size(pn_result.Width / 2, 350);
                lb_info.Location = new Point((pn_result.Width / 2) - (lb_info.Width / 2), titleSection.Height * 2);
                lb_info.BackColor = ColorTranslator.FromHtml("#282828");
                lb_info.BorderStyle = BorderStyle.Fixed3D;

                pn_result.Controls.Add(lb_info);

                //L'image du fichier
                PictureBox pictureBoxSpecificFileLayout = new PictureBox();
                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                byte[] fileData = request.DownloadData(string.Format("{0}", file.Picture));
                pictureBoxSpecificFileLayout.Image = ByteToImage(fileData);
                pictureBoxSpecificFileLayout.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSpecificFileLayout.Size = new System.Drawing.Size(200, 200);
                pictureBoxSpecificFileLayout.Location = new System.Drawing.Point((lb_info.Width / 2) - (pictureBoxSpecificFileLayout.Width), ((lb_info.Height / 2) - (pictureBoxSpecificFileLayout.Height / 2)) - 25);
                if (fileType == "Picture")
                {
                    pictureBoxSpecificFileLayout.SizeMode = PictureBoxSizeMode.Zoom;
                }
                lb_info.Controls.Add(pictureBoxSpecificFileLayout);

                //Le nom de l'auteur
                Label textBoxAuthor = new Label();
                textBoxAuthor.Size = new Size(200, 25);
                textBoxAuthor.Font = new System.Drawing.Font("Verdana", 12F);
                textBoxAuthor.Location = new Point(lb_info.Width / 2, pictureBoxSpecificFileLayout.Location.Y);
                textBoxAuthor.Text = string.Format("De : {0}", file.FileOwner.Pseudo);
                textBoxAuthor.ForeColor = Color.White;
                textBoxAuthor.BackColor = Color.Transparent;
                textBoxAuthor.BringToFront();
                textBoxAuthor.Name = "Author";
                textBoxAuthor.TextAlign = ContentAlignment.TopLeft;

                lb_info.Controls.Add(textBoxAuthor);

                //Le theme
                Label textBoxTheme = new Label();
                textBoxTheme.Size = new Size(200, 25);
                textBoxTheme.AutoSize = true;
                textBoxTheme.Font = new System.Drawing.Font("Verdana", 12F);
                textBoxTheme.Location = new Point(lb_info.Width / 2, textBoxAuthor.Location.Y + textBoxAuthor.Height);
                textBoxTheme.Text = string.Format("Theme(s) : {0}", file.Theme);
                textBoxTheme.ForeColor = Color.White;
                textBoxTheme.BackColor = Color.Transparent;
                textBoxTheme.BringToFront();
                textBoxTheme.Name = "Theme";
                textBoxTheme.TextAlign = ContentAlignment.MiddleCenter;

                lb_info.Controls.Add(textBoxTheme);

                //Le genre
                Label labelGenre = new Label();
                labelGenre.Size = new Size(200, 25);
                labelGenre.AutoSize = true;
                labelGenre.Font = new System.Drawing.Font("Verdana", 12F);
                labelGenre.Location = new Point(lb_info.Width / 2, textBoxTheme.Location.Y + textBoxTheme.Height + 5);
                labelGenre.Text = string.Format("Genre : {0}", file.Genre);
                labelGenre.ForeColor = Color.White;
                labelGenre.BackColor = Color.Transparent;
                labelGenre.BringToFront();
                labelGenre.Name = "Genre";
                labelGenre.TextAlign = ContentAlignment.MiddleCenter;

                lb_info.Controls.Add(labelGenre);

                //Nb vues
                Label labelNbViews = new Label();
                labelNbViews.Size = new Size(200, 25);
                labelNbViews.AutoSize = true;
                labelNbViews.Font = new System.Drawing.Font("Verdana", 12F);
                labelNbViews.Location = new Point(lb_info.Width / 2, labelGenre.Location.Y + labelGenre.Height + 5);
                labelNbViews.Text = string.Format("Nombre de vues : {0}", file.NbViews);
                labelNbViews.ForeColor = Color.White;
                labelNbViews.BackColor = Color.Transparent;
                labelNbViews.BringToFront();
                labelNbViews.Name = "NbViews";
                labelNbViews.TextAlign = ContentAlignment.MiddleCenter;

                lb_info.Controls.Add(labelNbViews);

                //La date de création
                Label labelCreationDate = new Label();
                labelCreationDate.Size = new Size(200, 25);
                labelCreationDate.AutoSize = true;
                labelCreationDate.Font = new System.Drawing.Font("Verdana", 12F);
                labelCreationDate.Location = new Point(lb_info.Width / 2, labelNbViews.Location.Y + labelNbViews.Height + 5);
                labelCreationDate.Text = string.Format("Date de création : {0}", file.DateCreated.Value.ToShortDateString());
                labelCreationDate.ForeColor = Color.White;
                labelCreationDate.BackColor = Color.Transparent;
                labelCreationDate.BringToFront();
                labelCreationDate.Name = "DateCreate";
                labelCreationDate.TextAlign = ContentAlignment.MiddleCenter;

                lb_info.Controls.Add(labelCreationDate);

                //Notation
                Label labelNotation = new Label();
                labelNotation.Size = new Size(200, 25);
                labelNotation.AutoSize = true;
                labelNotation.Font = new System.Drawing.Font("Verdana", 12F);
                labelNotation.Location = new Point(lb_info.Width / 2, labelCreationDate.Location.Y + labelCreationDate.Height + 5);
                labelNotation.ForeColor = Color.White;
                labelNotation.BackColor = Color.Transparent;
                labelNotation.BringToFront();
                labelNotation.Name = "Notation";
                labelNotation.TextAlign = ContentAlignment.MiddleCenter;

                if (file.Notation == -2)
                {
                    labelNotation.Text = string.Format("Pas encore de notes pour cette oeuvre!");
                }
                else
                {
                    labelNotation.Text = string.Format("Notation : {0}", file.Notation);
                }

                lb_info.Controls.Add(labelNotation);

                //Le langage
                Label labelLanguage = new Label();
                labelLanguage.Size = new Size(200, 25);
                labelLanguage.AutoSize = true;
                labelLanguage.Font = new System.Drawing.Font("Verdana", 12F);
                labelLanguage.Location = new Point(lb_info.Width / 2, labelNotation.Location.Y + labelNotation.Height + 5);
                labelLanguage.Text = string.Format("Langue(s) : {0}", file.FileLanguage.ToString());
                labelLanguage.ForeColor = Color.White;
                labelLanguage.BackColor = Color.Transparent;
                labelLanguage.BringToFront();
                labelLanguage.Name = "Language";
                labelLanguage.TextAlign = ContentAlignment.MiddleCenter;

                lb_info.Controls.Add(labelLanguage);

                //ListBox note
                ListBox lb_note = new ListBox();
                lb_note.Size = new Size(50, 50);
                lb_note.Location = new Point((lb_info.Width / 2) + 5, labelLanguage.Location.Y + labelLanguage.Height + 5);
                lb_note.BringToFront();
                lb_note.ItemHeight = 25;
                for(int i = 0; i <= 20; i++)
                {
                    lb_note.Items.Add(i);
                }

                lb_info.Controls.Add(lb_note);

                //Bouton note
                Button btn_note = new Button();
                btn_note.Size = new Size(50, 25);
                btn_note.AutoSize = true;
                btn_note.Location = new Point(lb_info.Width / 2 + lb_note.Width * 2, lb_note.Location.Y + 10);
                btn_note.Text = "Ajouter une note";
                btn_note.ForeColor = Color.White;
                btn_note.BackColor = ColorTranslator.FromHtml("#4f4d4b");
                btn_note.Click += (s, e1) => btn_note_Click(s, e1, Convert.ToInt32(lb_note.GetItemText(lb_note.SelectedItem)), file.IdFile, ConnectedUser.user.IdUser);
                lb_info.Controls.Add(btn_note);

                //Le bouton DL/See
                Button buttonSeeMedia = new Button(); textBoxTheme.Size = new Size(200, 25);
                buttonSeeMedia.AutoSize = true;
                buttonSeeMedia.Font = new System.Drawing.Font("Verdana", 12F);
                buttonSeeMedia.Location = new Point((lb_info.Width / 2) - (buttonSeeMedia.Width * 2) - 40, pictureBoxSpecificFileLayout.Location.Y + pictureBoxSpecificFileLayout.Height + 45);
                buttonSeeMedia.BringToFront();
                buttonSeeMedia.BackColor = ColorTranslator.FromHtml("#4f4d4b");
                buttonSeeMedia.ForeColor = Color.White;
                buttonSeeMedia.Tag = file.IdFile;
                buttonSeeMedia.Name = "ButtonMedia";
                buttonSeeMedia.TextAlign = ContentAlignment.MiddleCenter;
                buttonSeeMedia.Click += new System.EventHandler(this.buttonSeeMedia_Click);

                lb_info.Controls.Add(buttonSeeMedia);

                //Le bouton DOWNLOAD
                Button buttonDL = new Button();
                buttonDL.Text = "Telecharger";
                buttonDL.Tag = file.Directory;
                buttonDL.Click += new EventHandler(StartDownload);
                buttonDL.BackColor = ColorTranslator.FromHtml("#4f4d4b");
                buttonDL.ForeColor = Color.White;
                buttonDL.AutoSize = true;
                buttonDL.Location = new Point((lb_info.Width / 2) - (buttonSeeMedia.Width / 2) - 40, pictureBoxSpecificFileLayout.Location.Y + pictureBoxSpecificFileLayout.Height + 45);
                buttonDL.Font = new System.Drawing.Font("Verdana", 12F);


                lb_info.Controls.Add(buttonDL);

                //Le bouton AddToPlaylist
                Button btn_addToPlaylist = new Button();
                btn_addToPlaylist.Text = "Ajouter à une playlist";
                btn_addToPlaylist.Tag = file.IdFile;
                btn_addToPlaylist.Click += new EventHandler(btn_addToPlaylist_Click);
                btn_addToPlaylist.BackColor = ColorTranslator.FromHtml("#4f4d4b");
                btn_addToPlaylist.ForeColor = Color.White;
                btn_addToPlaylist.AutoSize = true;
                btn_addToPlaylist.Location = new Point((lb_info.Width / 2) + buttonSeeMedia.Width, pictureBoxSpecificFileLayout.Location.Y + pictureBoxSpecificFileLayout.Height + 45);
                btn_addToPlaylist.Font = new System.Drawing.Font("Verdana", 12F);

                lb_info.Controls.Add(btn_addToPlaylist);

                //Les lables différents selon le type
                switch (fileType)
                {
                    case "Book":
                        buttonSeeMedia.Text = "Lire";
                        //Editeur
                        /*Label labelEditor = new Label();
                        labelEditor.Size = new Size(200, 25);
                        labelEditor.AutoSize = true;
                        labelEditor.Font = new System.Drawing.Font("Verdana", 12F);
                        labelEditor.Location = new Point(labelNbViews.Location.X, labelNbViews.Location.Y + labelNbViews.Height + 5);
                        labelEditor.Text = string.Format("Editeur : {0}", book.Editor);
                        pn_result.Controls.Add(labelEditor);

                        //Date de sortie
                        Label releaseDate = new Label();
                        releaseDate.Size = new Size(200, 25);
                        releaseDate.AutoSize = true;
                        releaseDate.Font = new System.Drawing.Font("Verdana", 12F);
                        releaseDate.Location = new Point(labelEditor.Location.X, labelEditor.Location.Y + labelEditor.Height + 5);
                        releaseDate.Text = string.Format("Date de sortie : {0}", book.ReleaseDate.ToShortDateString());
                        pn_result.Controls.Add(releaseDate);*/
                        break;
                    case "Sound":
                        buttonSeeMedia.Text = "Ecouter";
                        /*
                        //Duration
                        Label labelDurationSong = new Label();
                        labelDurationSong.Size = new Size(200, 25);
                        labelDurationSong.AutoSize = true;
                        labelDurationSong.Font = new System.Drawing.Font("Verdana", 12F);
                        labelDurationSong.Location = new Point(labelNbViews.Location.X, labelNbViews.Location.Y + labelNbViews.Height + 5);
                        labelDurationSong.Text = string.Format("Duree : {0}", song.Duration);
                        pn_result.Controls.Add(labelDurationSong);
                        */
                        break;
                    case "Video":
                        buttonSeeMedia.Text = "Regarder";
                        /*
                        //Duration
                        Label labelDurationVideo = new Label();
                        labelDurationVideo.Size = new Size(200, 25);
                        labelDurationVideo.AutoSize = true;
                        labelDurationVideo.Font = new System.Drawing.Font("Verdana", 12F);
                        labelDurationVideo.Location = new Point(labelNbViews.Location.X, labelNbViews.Location.Y + labelNbViews.Height + 5);
                        labelDurationVideo.Text = string.Format("Duree : {0}", video.Duration);
                        pn_result.Controls.Add(labelDurationVideo);
                        */
                        break;
                    case "Picture":
                        buttonSeeMedia.Text = "Agrandir";
                        /*
                        //Taille
                        Label labelSize = new Label();
                        labelSize.Size = new Size(200, 25);
                        labelSize.AutoSize = true;
                        labelSize.Font = new System.Drawing.Font("Verdana", 12F);
                        labelSize.Location = new Point(labelNbViews.Location.X, labelNbViews.Location.Y + labelNbViews.Height + 5);
                        labelSize.Text = string.Format("Taille : {0}x{1}", photo.Width, photo.Height);
                        pn_result.Controls.Add(labelSize);
                        */
                        break;
                }

                //Exprimez vous
                Label labelComment = new Label();
                labelComment.Size = new Size(200, 25);
                labelComment.AutoSize = true;
                labelComment.Font = new System.Drawing.Font("Verdana", 12F);
                labelComment.Location = new Point((pn_result.Width / 4) - 50, ((pn_result.Height / 4) * 3) - labelComment.Height * 4);
                labelComment.ForeColor = Color.White;
                labelComment.BackColor = Color.Transparent;
                labelComment.Text = "Exprimez-vous :";

                pn_result.Controls.Add(labelComment);

                //Commentaire
                RichTextBox comments = new RichTextBox();
                comments.Size = new Size(300, 25);
                comments.Font = new System.Drawing.Font("Verdana", 12F);
                comments.Location = new Point(labelComment.Location.X + labelComment.Width + 50, labelComment.Location.Y);
                pn_result.Controls.Add(comments);

                //Envoyer commentaire
                bool quisertarien = false;
                Dictionary<Comment, User> commentList = connection.GetListComment(file.IdFile, ref quisertarien);
                Button comment_btn = new Button();
                comment_btn.Text = "Commenter";

                comment_btn.Click += (s, e1) => addCommenty(s, e1, commentList.Count + 1, comments.Text, ConnectedUser.user.IdUser, file.IdFile);

                comment_btn.Size = new Size(200, 25);
                comment_btn.Location = new Point(comments.Location.X + comments.Width + 50, comments.Location.Y);
                comment_btn.AutoSize = true;
                comment_btn.Font = new System.Drawing.Font("Verdana", 12F);
                comment_btn.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                comment_btn.ForeColor = Color.White;
                pn_result.Controls.Add(comment_btn);

                pn_result.ScrollControlIntoView(ActiveControl);
                LoadComments(file.IdFile, (pictureBoxSpecificFileLayout.Location.Y + pictureBoxSpecificFileLayout.Height));

                pn_result.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void addCommenty(object sender, EventArgs e, int idComment, string text, int user, int idFile)
        {
            ConnectionManager connection = new ConnectionManager();

            if (connection.AddComment(idComment, text, user, idFile))
            {
                Label labelOK = new Label();
                labelOK.Size = new Size(200, 25);
                labelOK.AutoSize = true;
                labelOK.Font = new System.Drawing.Font("Verdana", 12F);
                labelOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                labelOK.Text = string.Format("Commentaire ajouter avec succès.");
                labelOK.TextAlign = ContentAlignment.MiddleCenter;
                labelOK.AutoSize = false;
                labelOK.Dock = DockStyle.Top;

                Button buttonOK = new Button();
                buttonOK.Size = new Size(50, 50);
                buttonOK.AutoSize = true;
                buttonOK.Font = new System.Drawing.Font("Verdana", 12F);
                buttonOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                buttonOK.Text = "OK";
                buttonOK.TextAlign = ContentAlignment.MiddleCenter;
                buttonOK.AutoSize = false;
                buttonOK.Dock = DockStyle.Bottom;
                buttonOK.DialogResult = DialogResult.OK;

                Form dialogOK = new Form();
                dialogOK.Controls.Add(labelOK);
                dialogOK.Controls.Add(buttonOK);
                dialogOK.ShowDialog();

                if (buttonOK.DialogResult == DialogResult.OK)
                {
                    dialogOK.Close();
                }
            }
        }

        private void LoadComments(int idFile, int locationY)
        {
            bool anyComment = false;
            ConnectionManager cm = new ConnectionManager();
            Dictionary<Comment, User> commentList = cm.GetListComment(idFile, ref anyComment);
            Panel[] listPanels = new Panel[commentList.Count];
            PictureBox[] listPictureBox = new PictureBox[commentList.Count];
            Label[] listLabels = new Label[commentList.Count];
            Label[] listLabelsContent = new Label[commentList.Count];

            Panel panel1 = new Panel();

            int i = 0;

            if (anyComment)
            {
                foreach (KeyValuePair<Comment, User> item in commentList)
                {
                    try
                    {
                        listPanels[i] = new Panel();
                        listPictureBox[i] = new PictureBox();
                        listLabels[i] = new Label();
                        listLabelsContent[i] = new Label();

                        //Le panel pour les gouverner tous
                        //Il ne compte quand meme que pour un
                        listPanels[i].Name = string.Format("panel{0}", i);
                        listPanels[i].BackColor = ColorTranslator.FromHtml("#282828");
                        listPanels[i].Size = new System.Drawing.Size(pn_result.Width - 8, 100);
                        if (i == 0)
                        {
                            listPanels[i].Location = new System.Drawing.Point(pn_category.Location.X, 0);
                        }
                        else
                        {
                            listPanels[i].Location = new System.Drawing.Point(listPanels[i - 1].Location.X, listPanels[i - 1].Location.Y + listPanels[i - 1].Height + 5);
                        }
                        //La pdp de l'utilisateur
                        WebClient request = new WebClient();
                        request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                        byte[] fileData = request.DownloadData(string.Format("{0}", item.Value.Pic));

                        listPictureBox[i].Name = string.Format("pictureBox{0}", i);
                        listPictureBox[i].Location = new Point(listPanels[i].Location.X, 0);
                        listPictureBox[i].Image = ByteToImage(fileData);
                        listPictureBox[i].Size = new Size(4 * (listPanels[i].Height / 5), 4 * (listPanels[i].Height / 5));
                        listPictureBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                        listPictureBox[i].BringToFront();

                        //Son pseudo
                        listLabels[i].Name = string.Format("label{0}", i);
                        listLabels[i].Location = new Point(listPictureBox[i].Location.X + 7, listPictureBox[i].Location.Y + listPictureBox[i].Height);
                        listLabels[i].AutoSize = true;
                        listLabels[i].Text = item.Value.Pseudo;
                        listLabels[i].Font = new System.Drawing.Font("Verdana", 12F);
                        listLabels[i].ForeColor = Color.White;
                        listLabels[i].BringToFront();

                        //Et le contenu!
                        listLabelsContent[i].Name = string.Format("labelContent{0}", i);
                        listLabelsContent[i].Location = new Point(listPictureBox[i].Location.X + listPictureBox[i].Width + 5, listPictureBox[i].Location.Y);
                        listLabelsContent[i].Size = new Size(listPanels[i].Width - listLabelsContent[i].Location.X, listPanels[i].Height);
                        listLabelsContent[i].Text = item.Key.Content;
                        listLabelsContent[i].Font = new System.Drawing.Font("Verdana", 12F);
                        listLabelsContent[i].ForeColor = Color.White;
                        listLabelsContent[i].BringToFront();

                        //On add tout
                        listPanels[i].Controls.Add(listPictureBox[i]);
                        listPanels[i].Controls.Add(listLabels[i]);
                        listPanels[i].Controls.Add(listLabelsContent[i]);
                        panel1.Controls.Add(listPanels[i]);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            panel1.AutoScroll = true;
            panel1.Size = new Size(pn_result.Width - 7, 220);
            panel1.Location = new Point(0, ((pn_result.Height / 4) * 3) - 50);


            pn_result.Controls.Add(panel1);

        }

        private void btn_note_Click(object sender, EventArgs e, int note, int idFile, int idUser)
        {
            FileManager fm = new FileManager();
            if (fm.UpdateNote(note, idFile, idUser))
            {
                Label labelOK = new Label();
                labelOK.Size = new Size(200, 25);
                labelOK.AutoSize = true;
                labelOK.Font = new System.Drawing.Font("Verdana", 12F);
                labelOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                labelOK.Text = string.Format("Fichier noter avec succès.");
                labelOK.TextAlign = ContentAlignment.MiddleCenter;
                labelOK.AutoSize = false;
                labelOK.Dock = DockStyle.Top;

                Button buttonOK = new Button();
                buttonOK.Size = new Size(80, 80);
                buttonOK.AutoSize = true;
                buttonOK.Font = new System.Drawing.Font("Verdana", 12F);
                buttonOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                buttonOK.Text = "OK";
                buttonOK.TextAlign = ContentAlignment.MiddleCenter;
                buttonOK.AutoSize = false;
                buttonOK.Dock = DockStyle.Bottom;
                buttonOK.DialogResult = DialogResult.OK;

                Form dialogOK = new Form();
                dialogOK.Controls.Add(labelOK);
                dialogOK.Controls.Add(buttonOK);
                dialogOK.ShowDialog();

                if (buttonOK.DialogResult == DialogResult.OK)
                {
                    dialogOK.Close();
                }
            }

        }

        private void buttonSeeMedia_Click(object sender, EventArgs e)
        {
            UploadDownload ud = new UploadDownload();
            string downloadFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            downloadFilePath = downloadFilePath.Substring(0, downloadFilePath.LastIndexOf('\\'));
            downloadFilePath = downloadFilePath.Substring(0, downloadFilePath.LastIndexOf('\\'));
            downloadFilePath = downloadFilePath + "\\temp";
            string tempFileComputer = "";
            if (!Directory.Exists(downloadFilePath))
            {
                Directory.CreateDirectory(downloadFilePath);
            }
            tempFileComputer = string.Format("{0}\\tempFile.{1}", downloadFilePath, fileFormat);
            string ftpFilePath = pn_result.Tag.ToString();
            ud.DownloadFile(tempFileComputer, ftpFilePath);

            if (fileType == "Book")
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(tempFileComputer);
                Process.Start(sInfo);
            }
            else
            {
                FormPlayer player = new FormPlayer();
                player.GetFtp(tempFileComputer);
                player.GetFileType(fileType);
                player.ShowDialog();
            }
        }

        private void lb_Name_Click(object sender, EventArgs e)
        {
            DisplayBody_User(ConnectedUser.user.Pseudo);
        }

        //Mes fichiers
        private void DisplayFiles()
        {
            pn_result.Controls.Clear();

            /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            /*------------------------------------------------------------------LIST BOX-----------------------------------------------------------------------------------------*/
            /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            ListView listBoxFiles = new ListView();
            List<BabelFiles> listFiles = new List<BabelFiles>();

            listBoxFiles.View = View.Details;
            listBoxFiles.GridLines = true;
            listBoxFiles.Height = 250;
            listBoxFiles.Width = pn_result.Width / 2;
            listBoxFiles.Location = new Point((pn_result.Width / 2) - (listBoxFiles.Width / 2), (pn_result.Height / 2) - (listBoxFiles.Height / 2));
            listBoxFiles.Columns.Add("Titre", 150);
            listBoxFiles.Columns.Add("Nombre de vues", 100);
            listBoxFiles.Columns.Add("Theme", 95);
            listBoxFiles.Columns.Add("Genre", 90);
            listBoxFiles.Columns.Add("Visibilité", 50);
            listBoxFiles.Columns.Add("Data de création", 100);
            FileManager manager = new FileManager();
            StringBuilder builder = new StringBuilder("");
            builder.Clear();
            builder.Append(string.Format(@"SELECT * FROM files f, users u
                                           WHERE u.idUser = f.fileOwner
                                           AND u.pseudo='{0}'", ConnectedUser.user.Pseudo));

            SqlCommand command = new SqlCommand(builder.ToString());
            listFiles = manager.GetALLFiles(command);

            ListViewItem[] item = new ListViewItem[listFiles.Count];
            ListViewItem.ListViewSubItem[] subItemNbViews = new ListViewItem.ListViewSubItem[listFiles.Count];
            ListViewItem.ListViewSubItem[] subItemGenre = new ListViewItem.ListViewSubItem[listFiles.Count];
            ListViewItem.ListViewSubItem[] subItemTheme = new ListViewItem.ListViewSubItem[listFiles.Count];
            ListViewItem.ListViewSubItem[] subItemVisibility = new ListViewItem.ListViewSubItem[listFiles.Count];
            ListViewItem.ListViewSubItem[] subItemCreated = new ListViewItem.ListViewSubItem[listFiles.Count];

            int compteur = 0; //compteur pour le foreach
            foreach (BabelFiles files in listFiles)
            {
                //initialisation
                item[compteur] = new ListViewItem();
                subItemNbViews[compteur] = new ListViewItem.ListViewSubItem();
                subItemTheme[compteur] = new ListViewItem.ListViewSubItem();
                subItemGenre[compteur] = new ListViewItem.ListViewSubItem();
                subItemVisibility[compteur] = new ListViewItem.ListViewSubItem();
                subItemCreated[compteur] = new ListViewItem.ListViewSubItem();
                item[compteur].Text = files.Title;
                item[compteur].Tag = files.IdFile;
                subItemGenre[compteur].Text = files.Genre;
                subItemTheme[compteur].Text = files.Theme;
                subItemVisibility[compteur].Text = files.Visibility.ToString();
                subItemNbViews[compteur].Text = files.NbViews.ToString();
                subItemCreated[compteur].Text = files.DateCreated.Value.ToShortDateString();
                item[compteur].SubItems.Add(subItemNbViews[compteur]);
                item[compteur].SubItems.Add(subItemTheme[compteur]);
                item[compteur].SubItems.Add(subItemGenre[compteur]);
                item[compteur].SubItems.Add(subItemVisibility[compteur]);
                item[compteur].SubItems.Add(subItemCreated[compteur]);

                compteur++;
            }
            listBoxFiles.Items.AddRange(item);

            listBoxFiles.DoubleClick += new System.EventHandler(listBoxFiles_DoubleClick);
            /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            Label titre = new Label();
            titre.Size = new Size(200, 35);
            titre.Font = new System.Drawing.Font("Verdana", 20F);
            titre.Location = new Point((pn_result.Width / 2) - (titre.Width / 2), (pn_result.Height / 2) - (titre.Height / 2) - listBoxFiles.Height);
            titre.ForeColor = Color.Black;
            titre.BackColor = Color.White;
            titre.Text = "Vos fichiers";
            titre.TextAlign = ContentAlignment.MiddleCenter;

            //Control - upload

            Button button_upload = new Button();
            button_upload.Size = new Size(300, 35);
            button_upload.Font = new System.Drawing.Font("Verdana", 15F);
            button_upload.Location = new Point((pn_result.Width / 2) - (button_upload.Width / 2), (pn_result.Height / 2) + listBoxFiles.Height);
            button_upload.Text = "Mettre en ligne un fichier";
            button_upload.ForeColor = Color.White;
            button_upload.BackColor = ColorTranslator.FromHtml("#4f4d4b");

            FormAddNewFile form = new FormAddNewFile();
            button_upload.Click += (sender, e) => form.ShowDialog();

            pn_result.Controls.Add(listBoxFiles);
            pn_result.Controls.Add(titre);
            pn_result.Controls.Add(button_upload);


        }

        private void listBoxFiles_DoubleClick(object sender, EventArgs e)
        {
            ListView autoroute = (ListView)sender;
            Label voiture = new Label();
            voiture.Tag = autoroute.FocusedItem.Tag;
            DisplayBody_SpecificFile(voiture, e);

        }

        private void lb_myFiles_Click(object sender, EventArgs e)
        {
            DisplayFiles();
        }

        //Recherche
        private void searchButton_Click(object sender, EventArgs e)
        {
            ClearBody();
            SearchManager sm = new SearchManager();
            string[] listResultUser = sm.resultSearchUser(searchBar.Text).ToArray();
            BabelFiles[] listResultFile = sm.resultSearchFile(searchBar.Text).ToArray();
            DisplayBody_SearchResult(listResultUser, listResultFile);
        }

        private void btn_addPlaylist_Click(object sender, EventArgs e)
        {
            FormAddNewPlaylist form = new FormAddNewPlaylist();
            form.ShowDialog();
            DisplayBody_UserPlaylists(ConnectedUser.user.IdUser);
        }

        private void listViewSearchFile_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            Label lb = new Label();
            lb.Tag = lv.FocusedItem.Tag.ToString();
            DisplayBody_SpecificFile(lb, e);
        }

        private void listViewSearchUser_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            DisplayBody_User(lv.FocusedItem.Tag.ToString());
        }

        private void listViewPlaylists_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            DisplayBody_Playlist(Int32.Parse(lv.FocusedItem.Tag.ToString()));
        }

        private void listViewPlaylist_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            Label lb = new Label();
            lb.Tag = lv.FocusedItem.Tag.ToString();
            DisplayBody_SpecificFile(lb, e);
        }

        private void sortListViewPlaylists(object sender, ColumnClickEventArgs e)
        {
            bool isInt = false;
            if (e.Column == 3 || e.Column == 4)
            {
                isInt = true;
            }
            string[] columnText = { "∨ Playlist(s)", "∨ Auteur", "∨ Theme", "∨ Taille" };
            string[] columnTextSorted = { "∧ Playlist(s)", "∧ Auteur", "∧ Theme", "∧ Taille" };
            ListView lv = (ListView)sender;
            if (listViewSearchPlaylistsSorted[e.Column])
            {
                lv.ListViewItemSorter = new ListViewItemComparer(e.Column, -1, isInt);
                lv.Columns[e.Column].Text = columnText[e.Column];
                listViewSearchPlaylistsSorted[e.Column] = false;
            }
            else
            {
                lv.ListViewItemSorter = new ListViewItemComparer(e.Column, 1, isInt);
                for (int i = 0; i < listViewSearchPlaylistsSorted.Count(); i++)
                {
                    if (i == e.Column)
                    {
                        lv.Columns[i].Text = columnTextSorted[i];
                        listViewSearchPlaylistsSorted[i] = true;
                    }
                    else
                    {
                        lv.Columns[i].Text = columnText[i];
                        listViewSearchPlaylistsSorted[i] = false;
                    }
                }
            }
        }

        private void sortListViewFile(object sender, ColumnClickEventArgs e)
        {
            bool isInt = false;
            if (e.Column == 3 || e.Column == 4)
            {
                isInt = true;
            }
            string[] columnText = { "∨ Titre", "∨ Auteur", "∨ Genre", "∨ Vues", "∨ Note" };
            string[] columnTextSorted = { "∧ Titre", "∧ Auteur", "∧ Genre", "∧ Vues", "∧ Note" };
            ListView lv = (ListView)sender;
            if (listViewSearchFileSorted[e.Column])
            {
                lv.ListViewItemSorter = new ListViewItemComparer(e.Column, -1, isInt);
                lv.Columns[e.Column].Text = columnText[e.Column];
                listViewSearchFileSorted[e.Column] = false;
            }
            else
            {
                lv.ListViewItemSorter = new ListViewItemComparer(e.Column, 1, isInt);
                for (int i = 0; i < listViewSearchFileSorted.Count(); i++)
                {
                    if (i == e.Column)
                    {
                        lv.Columns[i].Text = columnTextSorted[i];
                        listViewSearchFileSorted[i] = true;
                    }
                    else
                    {
                        lv.Columns[i].Text = columnText[i];
                        listViewSearchFileSorted[i] = false;
                    }
                }
            }
        }

        private void sortListViewUser(object sender, ColumnClickEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (listViewSearchUserSorted)
            {
                lv.Sorting = System.Windows.Forms.SortOrder.Descending;
                lv.Columns[e.Column].Text = "∨ Utilisateur(s)";
                listViewSearchUserSorted = false;
            }
            else
            {
                lv.Sorting = System.Windows.Forms.SortOrder.Ascending;
                lv.Columns[e.Column].Text = "∧ Utilisateur(s)";
                listViewSearchUserSorted = true;
            }
        }

       

        private void DisplayBody_Playlist(int idPlaylist)
        {
            ClearBody();
            ListView lv_playlist = new ListView();

            lv_playlist.Size = new Size(pn_result.Width - 150, pn_result.Height - 100);
            lv_playlist.Location = new Point((pn_result.Width / 2) - (lv_playlist.Width / 2), (pn_result.Height / 2) - (lv_playlist.Height / 2) + 35);
            lv_playlist.View = View.Details;
            lv_playlist.CheckBoxes = false;
            lv_playlist.FullRowSelect = true;
            lv_playlist.GridLines = true;
            lv_playlist.Sorting = System.Windows.Forms.SortOrder.Ascending;

            PlaylistManager pm = new PlaylistManager();
            Playlist playlist = pm.GetPlaylist(idPlaylist);

            Label titre = new Label();
            titre.Size = new Size(200, 35);
            titre.AutoSize = true;
            titre.Font = new System.Drawing.Font("Verdana", 20F);
            titre.Location = new Point((pn_result.Width / 2) - (titre.Width / 2), (pn_result.Height / 2) - (titre.Height / 2) - (lv_playlist.Height / 2));
            titre.ForeColor = Color.Black;
            titre.BackColor = Color.White;
            titre.Text = playlist.Name;
            titre.TextAlign = ContentAlignment.MiddleCenter;

            List<ListViewItem> lviListFile = new List<ListViewItem>();

            foreach (BabelFiles file in playlist.List)
            {
                ListViewItem lviFile = new ListViewItem();
                lviFile.Text = file.Title;
                lviFile.Tag = file.IdFile;

                ListViewItem.ListViewSubItem si_owner = new ListViewItem.ListViewSubItem();
                ListViewItem.ListViewSubItem si_genre = new ListViewItem.ListViewSubItem();
                ListViewItem.ListViewSubItem si_nbViews = new ListViewItem.ListViewSubItem();
                ListViewItem.ListViewSubItem si_notation = new ListViewItem.ListViewSubItem();

                si_owner.Text = file.FileOwner.Pseudo;
                si_genre.Text = file.Genre;
                si_nbViews.Text = file.NbViews.ToString();
                if (file.Notation == -2)
                {
                    si_notation.Text = "NN";
                }
                else
                {
                    si_notation.Text = file.Notation.ToString();
                }

                lviFile.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { si_owner, si_genre, si_nbViews, si_notation });
                lviListFile.Add(lviFile);
            }




            ColumnHeader columnFileTitle = new ColumnHeader();
            columnFileTitle.Text = "∧ Titre";
            columnFileTitle.Width = lv_playlist.Width / 3;
            columnFileTitle.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileOwner = new ColumnHeader();
            columnFileOwner.Text = "∨ Auteur";
            columnFileOwner.Width = lv_playlist.Width / 6;
            columnFileOwner.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileGenre = new ColumnHeader();
            columnFileGenre.Text = "∨ Genre";
            columnFileGenre.Width = lv_playlist.Width / 6;
            columnFileGenre.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileViews = new ColumnHeader();
            columnFileViews.Text = "∨ Vues";
            columnFileViews.Width = lv_playlist.Width / 6;
            columnFileViews.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileNotation = new ColumnHeader();
            columnFileNotation.Text = "∨ Note";
            columnFileNotation.Width = lv_playlist.Width / 6;
            columnFileNotation.TextAlign = HorizontalAlignment.Left;

            lv_playlist.Columns.AddRange(new ColumnHeader[] { columnFileTitle, columnFileOwner, columnFileGenre, columnFileViews, columnFileNotation });
            lv_playlist.Items.AddRange(lviListFile.ToArray());

            lv_playlist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(sortListViewFile);
            lv_playlist.DoubleClick += new System.EventHandler(listViewPlaylist_DoubleClick);
            pn_result.Controls.Add(lv_playlist);
            pn_result.Controls.Add(titre);
        }

        private void DisplayBody_UserPlaylists(int idUser)
        {
            ClearBody();
            ListView lv_playlists = new ListView();

            lv_playlists.Size = new Size(pn_result.Width - 150, pn_result.Height - 100);
            lv_playlists.Location = new Point((pn_result.Width / 2) - (lv_playlists.Width / 2), (pn_result.Height / 2) - (lv_playlists.Height / 2) + 35);
            lv_playlists.View = View.Details;
            lv_playlists.CheckBoxes = false;
            lv_playlists.FullRowSelect = true;
            lv_playlists.GridLines = true;
            lv_playlists.Sorting = System.Windows.Forms.SortOrder.Ascending;

            Label titre = new Label();
            titre.Size = new Size(200, 35);
            titre.Font = new System.Drawing.Font("Verdana", 20F);
            titre.Location = new Point((pn_result.Width / 2) - (titre.Width / 2), (pn_result.Height / 2) - (titre.Height / 2) - (lv_playlists.Height / 2));
            titre.ForeColor = Color.Black;
            titre.BackColor = Color.White;
            titre.Text = "Vos playlists";
            titre.TextAlign = ContentAlignment.MiddleCenter;

            Button btn_addPlaylist = new Button();
            btn_addPlaylist.Size = new Size(150, 35);
            btn_addPlaylist.Location = new Point(titre.Location.X + (btn_addPlaylist.Width * 2), titre.Location.Y);
            btn_addPlaylist.Text = "Ajouter une Playlist";
            btn_addPlaylist.TextAlign = ContentAlignment.MiddleCenter;
            btn_addPlaylist.Click += new System.EventHandler(btn_addPlaylist_Click);

            PlaylistManager pm = new PlaylistManager();
            List<Playlist> playlists = pm.GetPlaylists(idUser);

            List<ListViewItem> lviListPlaylist = new List<ListViewItem>();

            if (playlists.Count() > 0)
            {
                foreach (Playlist p in playlists)
                {
                    ListViewItem lviPlaylist = new ListViewItem();
                    lviPlaylist.Text = p.Name;
                    lviPlaylist.Tag = p.IdPlaylist;

                    ListViewItem.ListViewSubItem si_owner = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem si_theme = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem si_size = new ListViewItem.ListViewSubItem();

                    si_owner.Text = p.Owner.Pseudo;
                    si_theme.Text = p.Theme;
                    si_size.Text = p.List.Count().ToString();

                    lviPlaylist.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { si_owner, si_theme, si_size });
                    lviListPlaylist.Add(lviPlaylist);
                }
            }
            

            ColumnHeader columnPlaylist = new ColumnHeader();
            columnPlaylist.Text = "∧ Playlist(s)";
            columnPlaylist.Width = lv_playlists.Width / 4;
            columnPlaylist.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnPlaylistOwner = new ColumnHeader();
            columnPlaylistOwner.Text = "∨ Auteur";
            columnPlaylistOwner.Width = lv_playlists.Width / 4;
            columnPlaylistOwner.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnPlaylistTheme = new ColumnHeader();
            columnPlaylistTheme.Text = "∨ Theme";
            columnPlaylistTheme.Width = lv_playlists.Width / 4;
            columnPlaylistTheme.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnPlaylistSize = new ColumnHeader();
            columnPlaylistSize.Text = "∨ Taille";
            columnPlaylistSize.Width = lv_playlists.Width / 4;
            columnPlaylistSize.TextAlign = HorizontalAlignment.Left;

            lv_playlists.Columns.AddRange(new ColumnHeader[] { columnPlaylist, columnPlaylistOwner, columnPlaylistTheme, columnPlaylistSize });
            lv_playlists.Items.AddRange(lviListPlaylist.ToArray());

            lv_playlists.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(sortListViewPlaylists);
            lv_playlists.DoubleClick += new System.EventHandler(listViewPlaylists_DoubleClick);
            pn_result.Controls.Add(lv_playlists);
            pn_result.Controls.Add(titre);
            pn_result.Controls.Add(btn_addPlaylist);
        }

        private void DisplayBody_SearchResult(string[] listResultUser, BabelFiles[] listResultFile)
        {
            ClearBody();
            ListView lv_serachResultUser = new ListView();
            ListView lv_serachResultFile = new ListView();

            lv_serachResultUser.Size = new Size(pn_result.Width / 2 - 150, pn_result.Height - 100);
            lv_serachResultUser.Location = new Point(((pn_result.Width / 2) / 2) - (lv_serachResultUser.Width / 2), (pn_result.Height / 2) - (lv_serachResultUser.Height / 2));
            lv_serachResultUser.View = View.Details;
            lv_serachResultUser.CheckBoxes = false;
            lv_serachResultUser.FullRowSelect = true;
            lv_serachResultUser.GridLines = true;
            lv_serachResultUser.Sorting = System.Windows.Forms.SortOrder.Ascending;

            lv_serachResultFile.Size = new Size(pn_result.Width / 2 - 150, pn_result.Height - 100);
            lv_serachResultFile.Location = new Point((((pn_result.Width / 2) / 2) * 3) - (lv_serachResultUser.Width / 2), (pn_result.Height / 2) - (lv_serachResultFile.Height / 2));
            lv_serachResultFile.View = View.Details;
            lv_serachResultFile.CheckBoxes = false;
            lv_serachResultFile.FullRowSelect = true;
            lv_serachResultFile.GridLines = true;
            lv_serachResultFile.Sorting = System.Windows.Forms.SortOrder.Ascending;

            List<ListViewItem> lviListUser = new List<ListViewItem>();
            List<ListViewItem> lviListFile = new List<ListViewItem>();

            if (listResultUser.Count() > 0)
            {
                foreach (string userPseudo in listResultUser)
                {
                    ListViewItem lviUser = new ListViewItem();
                    lviUser.Text = userPseudo;
                    lviUser.Tag = userPseudo;
                    lviListUser.Add(lviUser);
                }
            }

            if (listResultFile.Count() > 0)
            {
                foreach (BabelFiles file in listResultFile)
                {
                    ListViewItem lviFile = new ListViewItem();
                    lviFile.Text = file.Title;
                    lviFile.Tag = file.IdFile;

                    ListViewItem.ListViewSubItem si_owner = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem si_views = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem si_genre = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem si_notation = new ListViewItem.ListViewSubItem();

                    si_owner.Text = file.FileOwner.Pseudo;
                    si_views.Text = file.NbViews.ToString();
                    si_genre.Text = file.Genre;
                    if (file.Notation == -2)
                    {
                        si_notation.Text = "NN";
                    }
                    else
                    {
                        si_notation.Text = file.Notation.ToString();
                    }

                    lviFile.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { si_owner, si_genre, si_views, si_notation });
                    lviListFile.Add(lviFile);
                }
            }

            ColumnHeader columnUser = new ColumnHeader();
            columnUser.Text = "∧ Utilisateur(s)";
            columnUser.Width = lv_serachResultUser.Width;
            columnUser.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileTitle = new ColumnHeader();
            columnFileTitle.Text = "∧ Titre";
            columnFileTitle.Width = lv_serachResultFile.Width / 3;
            columnFileTitle.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileOwner = new ColumnHeader();
            columnFileOwner.Text = "∨ Auteur";
            columnFileOwner.Width = lv_serachResultFile.Width / 6;
            columnFileOwner.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileGenre = new ColumnHeader();
            columnFileGenre.Text = "∨ Genre";
            columnFileGenre.Width = lv_serachResultFile.Width / 6;
            columnFileGenre.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileViews = new ColumnHeader();
            columnFileViews.Text = "∨ Vues";
            columnFileViews.Width = lv_serachResultFile.Width / 6;
            columnFileViews.TextAlign = HorizontalAlignment.Left;

            ColumnHeader columnFileNotation = new ColumnHeader();
            columnFileNotation.Text = "∨ Note";
            columnFileNotation.Width = lv_serachResultFile.Width / 6;
            columnFileNotation.TextAlign = HorizontalAlignment.Left;

            lv_serachResultUser.Columns.Add(columnUser);
            lv_serachResultUser.Items.AddRange(lviListUser.ToArray());

            lv_serachResultFile.Columns.AddRange(new ColumnHeader[] { columnFileTitle, columnFileOwner, columnFileGenre, columnFileViews, columnFileNotation });
            lv_serachResultFile.Items.AddRange(lviListFile.ToArray());

            lv_serachResultUser.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(sortListViewUser);
            lv_serachResultFile.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(sortListViewFile);

            lv_serachResultFile.DoubleClick += new System.EventHandler(listViewSearchFile_DoubleClick);
            lv_serachResultUser.DoubleClick += new System.EventHandler(listViewSearchUser_DoubleClick);

            pn_result.Controls.Add(lv_serachResultUser);
            pn_result.Controls.Add(lv_serachResultFile);
        }

        

        private void StartDownload(object sender, EventArgs e)
        {
            Button suu = (Button)sender;
            string tag = suu.Tag.ToString();
            UploadDownload ud = new UploadDownload();
            string downloadFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            downloadFilePath = downloadFilePath.Substring(0, downloadFilePath.LastIndexOf('\\'));
            downloadFilePath = downloadFilePath + "\\Downloads";
            string tempFileComputer = "";
            if (!Directory.Exists(downloadFilePath))
            {
                Directory.CreateDirectory(downloadFilePath);
            }
            string ftpFilePath = pn_result.Tag.ToString();

            string[] words = ftpFilePath.Split('/');
            string fileName = words[words.Length - 1];

            tempFileComputer = string.Format("{0}\\{1}", downloadFilePath, fileName);
            ud.DownloadFile(tempFileComputer, ftpFilePath);

            Label labelOK = new Label();
            labelOK.Size = new Size(200, 25);
            labelOK.AutoSize = true;
            labelOK.Font = new System.Drawing.Font("Verdana", 12F);
            labelOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
            labelOK.Text = string.Format("Fichier téléchargé avec succès.");
            labelOK.TextAlign = ContentAlignment.MiddleCenter;
            labelOK.AutoSize = false;
            labelOK.Dock = DockStyle.Top;

            Button buttonOK = new Button();
            buttonOK.Size = new Size(50, 50);
            buttonOK.AutoSize = true;
            buttonOK.Font = new System.Drawing.Font("Verdana", 12F);
            buttonOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
            buttonOK.Text = "OK";
            buttonOK.TextAlign = ContentAlignment.MiddleCenter;
            buttonOK.AutoSize = false;
            buttonOK.Dock = DockStyle.Bottom;
            buttonOK.DialogResult = DialogResult.OK;

            Form dialogOK = new Form();
            dialogOK.Controls.Add(labelOK);
            dialogOK.Controls.Add(buttonOK);
            dialogOK.ShowDialog();

            if (buttonOK.DialogResult == DialogResult.OK)
            {
                dialogOK.Close();
            }
        }

        #endregion
    }
}
