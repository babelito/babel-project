namespace IHMBABEL
{
    partial class FormAddToPlaylist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_playlist = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.combo_playlist = new System.Windows.Forms.ComboBox();
            this.lb_error = new System.Windows.Forms.Label();
            this.btn_createOne = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_playlist
            // 
            this.lb_playlist.AutoSize = true;
            this.lb_playlist.Location = new System.Drawing.Point(70, 65);
            this.lb_playlist.Name = "lb_playlist";
            this.lb_playlist.Size = new System.Drawing.Size(45, 13);
            this.lb_playlist.TabIndex = 0;
            this.lb_playlist.Text = "Playlist :";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(73, 199);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Ajouter";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // combo_playlist
            // 
            this.combo_playlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_playlist.Location = new System.Drawing.Point(73, 101);
            this.combo_playlist.Name = "combo_playlist";
            this.combo_playlist.Size = new System.Drawing.Size(121, 21);
            this.combo_playlist.TabIndex = 2;
            // 
            // lb_error
            // 
            this.lb_error.AutoSize = true;
            this.lb_error.Location = new System.Drawing.Point(12, 149);
            this.lb_error.Name = "lb_error";
            this.lb_error.Size = new System.Drawing.Size(0, 13);
            this.lb_error.TabIndex = 3;
            // 
            // btn_createOne
            // 
            this.btn_createOne.Location = new System.Drawing.Point(73, 228);
            this.btn_createOne.Name = "btn_createOne";
            this.btn_createOne.Size = new System.Drawing.Size(109, 23);
            this.btn_createOne.TabIndex = 4;
            this.btn_createOne.Text = "En creer une";
            this.btn_createOne.UseVisualStyleBackColor = true;
            this.btn_createOne.Visible = false;
            this.btn_createOne.Click += new System.EventHandler(this.btn_createOne_Click);
            // 
            // FormAddToPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 302);
            this.Controls.Add(this.btn_createOne);
            this.Controls.Add(this.lb_error);
            this.Controls.Add(this.combo_playlist);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.lb_playlist);
            this.Name = "FormAddToPlaylist";
            this.Text = "FormAddToPlaylist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_playlist;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ComboBox combo_playlist;
        private System.Windows.Forms.Label lb_error;
        private System.Windows.Forms.Button btn_createOne;
    }
}