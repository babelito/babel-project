namespace IHMBABEL
{
    partial class FormConnection
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
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_connexion = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.label_status = new System.Windows.Forms.Label();
            this.inscirptionBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(468, 133);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(405, 31);
            this.textBox_username.TabIndex = 1;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(468, 236);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(405, 31);
            this.textBox_password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nom d\'utilisateur";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mot de passe";
            // 
            // button_connexion
            // 
            this.button_connexion.Location = new System.Drawing.Point(572, 334);
            this.button_connexion.Name = "button_connexion";
            this.button_connexion.Size = new System.Drawing.Size(178, 86);
            this.button_connexion.TabIndex = 4;
            this.button_connexion.Text = "Connexion";
            this.button_connexion.UseVisualStyleBackColor = true;
            this.button_connexion.Click += new System.EventHandler(this.connexionButton_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(572, 502);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(178, 86);
            this.exitBtn.TabIndex = 5;
            this.exitBtn.Text = "Quitter";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(451, 39);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(0, 25);
            this.label_status.TabIndex = 6;
            // 
            // inscirptionBtn
            // 
            this.inscirptionBtn.Location = new System.Drawing.Point(1217, 681);
            this.inscirptionBtn.Name = "inscirptionBtn";
            this.inscirptionBtn.Size = new System.Drawing.Size(178, 86);
            this.inscirptionBtn.TabIndex = 7;
            this.inscirptionBtn.Text = "Inscription";
            this.inscirptionBtn.UseVisualStyleBackColor = true;
            this.inscirptionBtn.Click += new System.EventHandler(this.inscirptionBtn_Click);
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 800);
            this.Controls.Add(this.inscirptionBtn);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.button_connexion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_username);
            this.Name = "FormConnection";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_connexion;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button inscirptionBtn;
    }
}