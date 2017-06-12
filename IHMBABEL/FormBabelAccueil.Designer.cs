using System.Drawing;

namespace IHMBABEL
{
    partial class FormBabelAccueil
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBabelAccueil));
            this.pn_profile = new System.Windows.Forms.Panel();
            this.lb_myFiles = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.pictureBoxUserConnected = new System.Windows.Forms.PictureBox();
            this.pn_searchBar = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.PictureBox();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.lb_recherche = new System.Windows.Forms.Label();
            this.pn_babel = new System.Windows.Forms.Panel();
            this.lb_titles = new System.Windows.Forms.Label();
            this.btn_myPlaylists = new System.Windows.Forms.Button();
            this.btn_music = new System.Windows.Forms.Button();
            this.btn_book = new System.Windows.Forms.Button();
            this.btn_video = new System.Windows.Forms.Button();
            this.btn_picture = new System.Windows.Forms.Button();
            this.pn_category = new System.Windows.Forms.Panel();
            this.pn_result = new System.Windows.Forms.Panel();
            this.pn_profile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserConnected)).BeginInit();
            this.pn_searchBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchButton)).BeginInit();
            this.pn_babel.SuspendLayout();
            this.pn_category.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_profile
            // 
            this.pn_profile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_profile.BackColor = System.Drawing.Color.White;
            this.pn_profile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_profile.Controls.Add(this.lb_myFiles);
            this.pn_profile.Controls.Add(this.lb_Name);
            this.pn_profile.Controls.Add(this.pictureBoxUserConnected);
            this.pn_profile.Location = new System.Drawing.Point(2621, 0);
            this.pn_profile.Margin = new System.Windows.Forms.Padding(6);
            this.pn_profile.Name = "pn_profile";
            this.pn_profile.Size = new System.Drawing.Size(256, 256);
            this.pn_profile.TabIndex = 1;
            // 
            // lb_myFiles
            // 
            this.lb_myFiles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_myFiles.BackColor = System.Drawing.Color.White;
            this.lb_myFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_myFiles.ForeColor = System.Drawing.Color.White;
            this.lb_myFiles.Location = new System.Drawing.Point(1, 212);
            this.lb_myFiles.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_myFiles.Name = "lb_myFiles";
            this.lb_myFiles.Size = new System.Drawing.Size(250, 35);
            this.lb_myFiles.TabIndex = 2;
            this.lb_myFiles.Text = "Mes fichiers";
            this.lb_myFiles.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_myFiles.Click += new System.EventHandler(this.lb_myFiles_Click);
            // 
            // lb_Name
            // 
            this.lb_Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_Name.BackColor = System.Drawing.Color.White;
            this.lb_Name.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_Name.ForeColor = System.Drawing.Color.White;
            this.lb_Name.Location = new System.Drawing.Point(1, 161);
            this.lb_Name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(250, 35);
            this.lb_Name.TabIndex = 1;
            this.lb_Name.Text = "User";
            this.lb_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Name.Click += new System.EventHandler(this.lb_Name_Click);
            // 
            // pictureBoxUserConnected
            // 
            this.pictureBoxUserConnected.Image = global::IHMBABEL.Properties.Resources.ninjatest;
            this.pictureBoxUserConnected.Location = new System.Drawing.Point(6, 0);
            this.pictureBoxUserConnected.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBoxUserConnected.Name = "pictureBoxUserConnected";
            this.pictureBoxUserConnected.Size = new System.Drawing.Size(160, 160);
            this.pictureBoxUserConnected.TabIndex = 0;
            this.pictureBoxUserConnected.TabStop = false;
            // 
            // pn_searchBar
            // 
            this.pn_searchBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_searchBar.BackColor = System.Drawing.Color.White;
            this.pn_searchBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_searchBar.Controls.Add(this.searchButton);
            this.pn_searchBar.Controls.Add(this.searchBar);
            this.pn_searchBar.Controls.Add(this.lb_recherche);
            this.pn_searchBar.Location = new System.Drawing.Point(532, 0);
            this.pn_searchBar.Margin = new System.Windows.Forms.Padding(6);
            this.pn_searchBar.Name = "pn_searchBar";
            this.pn_searchBar.Size = new System.Drawing.Size(2088, 256);
            this.pn_searchBar.TabIndex = 3;
            // 
            // searchButton
            // 
            this.searchButton.Image = global::IHMBABEL.Properties.Resources.loupe2;
            this.searchButton.Location = new System.Drawing.Point(1625, 71);
            this.searchButton.Margin = new System.Windows.Forms.Padding(6);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(136, 119);
            this.searchButton.TabIndex = 1;
            this.searchButton.TabStop = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(776, 107);
            this.searchBar.Margin = new System.Windows.Forms.Padding(6);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(776, 31);
            this.searchBar.TabIndex = 0;
            // 
            // lb_recherche
            // 
            this.lb_recherche.BackColor = System.Drawing.Color.Transparent;
            this.lb_recherche.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.lb_recherche.ForeColor = System.Drawing.Color.White;
            this.lb_recherche.Location = new System.Drawing.Point(302, 81);
            this.lb_recherche.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_recherche.Name = "lb_recherche";
            this.lb_recherche.Size = new System.Drawing.Size(463, 70);
            this.lb_recherche.TabIndex = 1;
            this.lb_recherche.Text = "Recherche :";
            // 
            // pn_babel
            // 
            this.pn_babel.BackColor = System.Drawing.Color.White;
            this.pn_babel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_babel.Controls.Add(this.lb_titles);
            this.pn_babel.Location = new System.Drawing.Point(0, 0);
            this.pn_babel.Margin = new System.Windows.Forms.Padding(6);
            this.pn_babel.Name = "pn_babel";
            this.pn_babel.Size = new System.Drawing.Size(525, 256);
            this.pn_babel.TabIndex = 2;
            // 
            // lb_titles
            // 
            this.lb_titles.BackColor = System.Drawing.Color.Transparent;
            this.lb_titles.Font = new System.Drawing.Font("Segoe Script", 50F);
            this.lb_titles.ForeColor = System.Drawing.Color.White;
            this.lb_titles.Location = new System.Drawing.Point(24, 28);
            this.lb_titles.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_titles.Name = "lb_titles";
            this.lb_titles.Size = new System.Drawing.Size(575, 253);
            this.lb_titles.TabIndex = 0;
            this.lb_titles.Text = "Babel";
            this.lb_titles.Click += new System.EventHandler(this.lb_titles_Click);
            // 
            // btn_myPlaylists
            // 
            this.btn_myPlaylists.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_myPlaylists.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_myPlaylists.FlatAppearance.BorderSize = 2;
            this.btn_myPlaylists.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_myPlaylists.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_myPlaylists.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_myPlaylists.ForeColor = System.Drawing.Color.White;
            this.btn_myPlaylists.Location = new System.Drawing.Point(10, 945);
            this.btn_myPlaylists.Margin = new System.Windows.Forms.Padding(6);
            this.btn_myPlaylists.Name = "btn_myPlaylists";
            this.btn_myPlaylists.Size = new System.Drawing.Size(500, 150);
            this.btn_myPlaylists.TabIndex = 2;
            this.btn_myPlaylists.Text = "Mes playlists";
            this.btn_myPlaylists.UseVisualStyleBackColor = true;
            this.btn_myPlaylists.Click += new System.EventHandler(this.btnPlaylists_click);
            // 
            // btn_music
            // 
            this.btn_music.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_music.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_music.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_music.FlatAppearance.BorderSize = 2;
            this.btn_music.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_music.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_music.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_music.ForeColor = System.Drawing.Color.White;
            this.btn_music.Location = new System.Drawing.Point(10, 24);
            this.btn_music.Margin = new System.Windows.Forms.Padding(6);
            this.btn_music.Name = "btn_music";
            this.btn_music.Size = new System.Drawing.Size(500, 150);
            this.btn_music.TabIndex = 2;
            this.btn_music.Text = "Musiques";
            this.btn_music.UseVisualStyleBackColor = true;
            this.btn_music.Click += new System.EventHandler(this.btnMusic_Click);
            // 
            // btn_book
            // 
            this.btn_book.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_book.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_book.FlatAppearance.BorderSize = 2;
            this.btn_book.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_book.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_book.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_book.ForeColor = System.Drawing.Color.White;
            this.btn_book.Location = new System.Drawing.Point(10, 701);
            this.btn_book.Margin = new System.Windows.Forms.Padding(6);
            this.btn_book.Name = "btn_book";
            this.btn_book.Size = new System.Drawing.Size(500, 150);
            this.btn_book.TabIndex = 2;
            this.btn_book.Text = "Livres";
            this.btn_book.UseVisualStyleBackColor = true;
            this.btn_book.Click += new System.EventHandler(this.btnBook_click);
            // 
            // btn_video
            // 
            this.btn_video.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_video.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_video.FlatAppearance.BorderSize = 2;
            this.btn_video.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_video.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_video.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_video.ForeColor = System.Drawing.Color.White;
            this.btn_video.Location = new System.Drawing.Point(10, 238);
            this.btn_video.Margin = new System.Windows.Forms.Padding(6);
            this.btn_video.Name = "btn_video";
            this.btn_video.Size = new System.Drawing.Size(500, 150);
            this.btn_video.TabIndex = 2;
            this.btn_video.Text = "Videos";
            this.btn_video.UseVisualStyleBackColor = true;
            this.btn_video.Click += new System.EventHandler(this.btnVideo_click);
            // 
            // btn_picture
            // 
            this.btn_picture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_picture.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_picture.FlatAppearance.BorderSize = 2;
            this.btn_picture.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btn_picture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_picture.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_picture.ForeColor = System.Drawing.Color.White;
            this.btn_picture.Location = new System.Drawing.Point(10, 474);
            this.btn_picture.Margin = new System.Windows.Forms.Padding(6);
            this.btn_picture.Name = "btn_picture";
            this.btn_picture.Size = new System.Drawing.Size(500, 150);
            this.btn_picture.TabIndex = 2;
            this.btn_picture.Text = "Images";
            this.btn_picture.UseVisualStyleBackColor = true;
            this.btn_picture.Click += new System.EventHandler(this.btnPictures_click);
            // 
            // pn_category
            // 
            this.pn_category.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pn_category.BackColor = System.Drawing.Color.White;
            this.pn_category.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_category.Controls.Add(this.btn_myPlaylists);
            this.pn_category.Controls.Add(this.btn_music);
            this.pn_category.Controls.Add(this.btn_book);
            this.pn_category.Controls.Add(this.btn_video);
            this.pn_category.Controls.Add(this.btn_picture);
            this.pn_category.Location = new System.Drawing.Point(0, 263);
            this.pn_category.Margin = new System.Windows.Forms.Padding(6);
            this.pn_category.Name = "pn_category";
            this.pn_category.Size = new System.Drawing.Size(525, 1112);
            this.pn_category.TabIndex = 3;
            // 
            // pn_result
            // 
            this.pn_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_result.BackColor = System.Drawing.Color.DimGray;
            this.pn_result.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pn_result.BackgroundImage")));
            this.pn_result.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pn_result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pn_result.Location = new System.Drawing.Point(532, 263);
            this.pn_result.Margin = new System.Windows.Forms.Padding(6);
            this.pn_result.Name = "pn_result";
            this.pn_result.Size = new System.Drawing.Size(2343, 1112);
            this.pn_result.TabIndex = 4;
            this.pn_result.Tag = "tendances";
            // 
            // FormBabelAccueil
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(2868, 1375);
            this.Controls.Add(this.pn_category);
            this.Controls.Add(this.pn_profile);
            this.Controls.Add(this.pn_result);
            this.Controls.Add(this.pn_searchBar);
            this.Controls.Add(this.pn_babel);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormBabelAccueil";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Babel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pn_profile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserConnected)).EndInit();
            this.pn_searchBar.ResumeLayout(false);
            this.pn_searchBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchButton)).EndInit();
            this.pn_babel.ResumeLayout(false);
            this.pn_category.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pn_profile;
        private System.Windows.Forms.Panel pn_babel;
        private System.Windows.Forms.Panel pn_searchBar;
        private System.Windows.Forms.PictureBox pictureBoxUserConnected;
        private System.Windows.Forms.Label lb_Name;
        private System.Windows.Forms.Label lb_myFiles;
        private System.Windows.Forms.Label lb_titles;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.PictureBox searchButton;
        private System.Windows.Forms.Panel pn_result;
        private System.Windows.Forms.Button btn_music;
        private System.Windows.Forms.Button btn_video;
        private System.Windows.Forms.Button btn_picture;
        private System.Windows.Forms.Button btn_book;
        private System.Windows.Forms.Button btn_myPlaylists;
        private System.Windows.Forms.Panel pn_category;
        private System.Windows.Forms.Label lb_recherche;
    }
}

