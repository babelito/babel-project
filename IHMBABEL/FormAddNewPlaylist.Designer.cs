namespace IHMBABEL
{
    partial class FormAddNewPlaylist
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
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_theme = new System.Windows.Forms.Label();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_theme = new System.Windows.Forms.TextBox();
            this.lb_visibility = new System.Windows.Forms.Label();
            this.cb_visibility = new System.Windows.Forms.CheckBox();
            this.btn_validate = new System.Windows.Forms.Button();
            this.lb_error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(54, 54);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(35, 13);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "Nom :";
            // 
            // lb_theme
            // 
            this.lb_theme.AutoSize = true;
            this.lb_theme.Location = new System.Drawing.Point(54, 109);
            this.lb_theme.Name = "lb_theme";
            this.lb_theme.Size = new System.Drawing.Size(46, 13);
            this.lb_theme.TabIndex = 1;
            this.lb_theme.Text = "Theme :";
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(110, 51);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(100, 20);
            this.tb_name.TabIndex = 2;
            // 
            // tb_theme
            // 
            this.tb_theme.Location = new System.Drawing.Point(110, 106);
            this.tb_theme.Name = "tb_theme";
            this.tb_theme.Size = new System.Drawing.Size(100, 20);
            this.tb_theme.TabIndex = 3;
            // 
            // lb_visibility
            // 
            this.lb_visibility.AutoSize = true;
            this.lb_visibility.Location = new System.Drawing.Point(54, 165);
            this.lb_visibility.Name = "lb_visibility";
            this.lb_visibility.Size = new System.Drawing.Size(43, 13);
            this.lb_visibility.TabIndex = 4;
            this.lb_visibility.Text = "Visible :";
            // 
            // cb_visibility
            // 
            this.cb_visibility.AutoSize = true;
            this.cb_visibility.Location = new System.Drawing.Point(110, 165);
            this.cb_visibility.Name = "cb_visibility";
            this.cb_visibility.Size = new System.Drawing.Size(15, 14);
            this.cb_visibility.TabIndex = 5;
            this.cb_visibility.UseVisualStyleBackColor = true;
            // 
            // btn_validate
            // 
            this.btn_validate.Location = new System.Drawing.Point(110, 226);
            this.btn_validate.Name = "btn_validate";
            this.btn_validate.Size = new System.Drawing.Size(75, 23);
            this.btn_validate.TabIndex = 6;
            this.btn_validate.Text = "Valider";
            this.btn_validate.UseVisualStyleBackColor = true;
            this.btn_validate.Click += new System.EventHandler(this.btn_validate_Click);
            // 
            // lb_error
            // 
            this.lb_error.AutoSize = true;
            this.lb_error.Location = new System.Drawing.Point(107, 200);
            this.lb_error.Name = "lb_error";
            this.lb_error.Size = new System.Drawing.Size(0, 13);
            this.lb_error.TabIndex = 7;
            // 
            // FormAddNewPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lb_error);
            this.Controls.Add(this.btn_validate);
            this.Controls.Add(this.cb_visibility);
            this.Controls.Add(this.lb_visibility);
            this.Controls.Add(this.tb_theme);
            this.Controls.Add(this.tb_name);
            this.Controls.Add(this.lb_theme);
            this.Controls.Add(this.lb_name);
            this.Name = "FormAddNewPlaylist";
            this.Text = "FormAddNewPlaylist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_theme;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_theme;
        private System.Windows.Forms.Label lb_visibility;
        private System.Windows.Forms.CheckBox cb_visibility;
        private System.Windows.Forms.Button btn_validate;
        private System.Windows.Forms.Label lb_error;
    }
}