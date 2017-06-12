namespace IHMBABEL
{
    partial class FormAddNewFile
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
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.buttonChooseFile = new System.Windows.Forms.Button();
            this.labelChooseFile = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelFileTypeDesc = new System.Windows.Forms.Label();
            this.labelMetadata1 = new System.Windows.Forms.Label();
            this.textBoxFileType = new System.Windows.Forms.TextBox();
            this.textBoxMetadata1 = new System.Windows.Forms.TextBox();
            this.textBoxMetadata2 = new System.Windows.Forms.TextBox();
            this.labelMetadata2 = new System.Windows.Forms.Label();
            this.dateTimePickerMetadata = new System.Windows.Forms.DateTimePicker();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.comboBoxFileLanguages = new System.Windows.Forms.ComboBox();
            this.labelTheme = new System.Windows.Forms.Label();
            this.textBoxTheme = new System.Windows.Forms.TextBox();
            this.textBoxGenre = new System.Windows.Forms.TextBox();
            this.labelGenre = new System.Windows.Forms.Label();
            this.checkBoxVisibility = new System.Windows.Forms.CheckBox();
            this.labelChoosePicture = new System.Windows.Forms.Label();
            this.textBoxChooseAdditionalPicture = new System.Windows.Forms.TextBox();
            this.buttonChooseAdditionalPicture = new System.Windows.Forms.Button();
            this.pictureBoxDisplayChoosenPicture = new System.Windows.Forms.PictureBox();
            this.labelTitleUpload = new System.Windows.Forms.Label();
            this.textBoxTitleUpload = new System.Windows.Forms.TextBox();
            this.buttonUploadFile = new System.Windows.Forms.Button();
            this.labelFileFormat = new System.Windows.Forms.Label();
            this.labelGetFilePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplayChoosenPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(44, 58);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(380, 20);
            this.textBoxFilePath.TabIndex = 0;
            this.textBoxFilePath.Tag = "Mandatory";
            // 
            // buttonChooseFile
            // 
            this.buttonChooseFile.Location = new System.Drawing.Point(404, 58);
            this.buttonChooseFile.Name = "buttonChooseFile";
            this.buttonChooseFile.Size = new System.Drawing.Size(20, 20);
            this.buttonChooseFile.TabIndex = 1;
            this.buttonChooseFile.Text = "...";
            this.buttonChooseFile.UseVisualStyleBackColor = true;
            this.buttonChooseFile.Click += new System.EventHandler(this.buttonChooseFile_Click);
            // 
            // labelChooseFile
            // 
            this.labelChooseFile.AutoSize = true;
            this.labelChooseFile.Location = new System.Drawing.Point(44, 39);
            this.labelChooseFile.Name = "labelChooseFile";
            this.labelChooseFile.Size = new System.Drawing.Size(102, 13);
            this.labelChooseFile.TabIndex = 2;
            this.labelChooseFile.Text = "Choisissez un fichier";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelTitle.Location = new System.Drawing.Point(176, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(118, 20);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Nouveau fichier";
            // 
            // labelFileTypeDesc
            // 
            this.labelFileTypeDesc.AutoSize = true;
            this.labelFileTypeDesc.Location = new System.Drawing.Point(69, 133);
            this.labelFileTypeDesc.Name = "labelFileTypeDesc";
            this.labelFileTypeDesc.Size = new System.Drawing.Size(77, 13);
            this.labelFileTypeDesc.TabIndex = 4;
            this.labelFileTypeDesc.Text = "Type de fichier";
            // 
            // labelMetadata1
            // 
            this.labelMetadata1.AutoSize = true;
            this.labelMetadata1.Location = new System.Drawing.Point(205, 133);
            this.labelMetadata1.Name = "labelMetadata1";
            this.labelMetadata1.Size = new System.Drawing.Size(52, 13);
            this.labelMetadata1.TabIndex = 6;
            this.labelMetadata1.Text = "labelMD1";
            this.labelMetadata1.Visible = false;
            // 
            // textBoxFileType
            // 
            this.textBoxFileType.Location = new System.Drawing.Point(44, 149);
            this.textBoxFileType.Name = "textBoxFileType";
            this.textBoxFileType.ReadOnly = true;
            this.textBoxFileType.Size = new System.Drawing.Size(121, 20);
            this.textBoxFileType.TabIndex = 7;
            this.textBoxFileType.Tag = " ";
            // 
            // textBoxMetadata1
            // 
            this.textBoxMetadata1.Location = new System.Drawing.Point(180, 149);
            this.textBoxMetadata1.Name = "textBoxMetadata1";
            this.textBoxMetadata1.Size = new System.Drawing.Size(92, 20);
            this.textBoxMetadata1.TabIndex = 8;
            this.textBoxMetadata1.Tag = " ";
            this.textBoxMetadata1.Visible = false;
            // 
            // textBoxMetadata2
            // 
            this.textBoxMetadata2.Location = new System.Drawing.Point(289, 149);
            this.textBoxMetadata2.Name = "textBoxMetadata2";
            this.textBoxMetadata2.Size = new System.Drawing.Size(92, 20);
            this.textBoxMetadata2.TabIndex = 10;
            this.textBoxMetadata2.Tag = " ";
            this.textBoxMetadata2.Visible = false;
            // 
            // labelMetadata2
            // 
            this.labelMetadata2.AutoSize = true;
            this.labelMetadata2.Location = new System.Drawing.Point(305, 133);
            this.labelMetadata2.Name = "labelMetadata2";
            this.labelMetadata2.Size = new System.Drawing.Size(52, 13);
            this.labelMetadata2.TabIndex = 9;
            this.labelMetadata2.Text = "labelMD2";
            this.labelMetadata2.Visible = false;
            // 
            // dateTimePickerMetadata
            // 
            this.dateTimePickerMetadata.Location = new System.Drawing.Point(289, 149);
            this.dateTimePickerMetadata.Name = "dateTimePickerMetadata";
            this.dateTimePickerMetadata.Size = new System.Drawing.Size(92, 20);
            this.dateTimePickerMetadata.TabIndex = 11;
            this.dateTimePickerMetadata.Tag = " ";
            this.dateTimePickerMetadata.Visible = false;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(81, 183);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(43, 13);
            this.labelLanguage.TabIndex = 12;
            this.labelLanguage.Text = "Langue";
            // 
            // comboBoxFileLanguages
            // 
            this.comboBoxFileLanguages.FormattingEnabled = true;
            this.comboBoxFileLanguages.Location = new System.Drawing.Point(44, 199);
            this.comboBoxFileLanguages.Name = "comboBoxFileLanguages";
            this.comboBoxFileLanguages.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFileLanguages.TabIndex = 15;
            this.comboBoxFileLanguages.Tag = " ";
            // 
            // labelTheme
            // 
            this.labelTheme.AutoSize = true;
            this.labelTheme.Location = new System.Drawing.Point(317, 183);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(40, 13);
            this.labelTheme.TabIndex = 16;
            this.labelTheme.Text = "Theme";
            // 
            // textBoxTheme
            // 
            this.textBoxTheme.Location = new System.Drawing.Point(289, 200);
            this.textBoxTheme.Name = "textBoxTheme";
            this.textBoxTheme.Size = new System.Drawing.Size(100, 20);
            this.textBoxTheme.TabIndex = 17;
            this.textBoxTheme.Tag = " ";
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Location = new System.Drawing.Point(180, 200);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(100, 20);
            this.textBoxGenre.TabIndex = 19;
            this.textBoxGenre.Tag = " ";
            // 
            // labelGenre
            // 
            this.labelGenre.AutoSize = true;
            this.labelGenre.Location = new System.Drawing.Point(208, 183);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Size = new System.Drawing.Size(36, 13);
            this.labelGenre.TabIndex = 18;
            this.labelGenre.Text = "Genre";
            // 
            // checkBoxVisibility
            // 
            this.checkBoxVisibility.AutoSize = true;
            this.checkBoxVisibility.Checked = true;
            this.checkBoxVisibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVisibility.Location = new System.Drawing.Point(44, 239);
            this.checkBoxVisibility.Name = "checkBoxVisibility";
            this.checkBoxVisibility.Size = new System.Drawing.Size(137, 17);
            this.checkBoxVisibility.TabIndex = 20;
            this.checkBoxVisibility.Text = "Visible pour les autres ?";
            this.checkBoxVisibility.UseVisualStyleBackColor = true;
            // 
            // labelChoosePicture
            // 
            this.labelChoosePicture.AutoSize = true;
            this.labelChoosePicture.Location = new System.Drawing.Point(44, 275);
            this.labelChoosePicture.Name = "labelChoosePicture";
            this.labelChoosePicture.Size = new System.Drawing.Size(174, 13);
            this.labelChoosePicture.TabIndex = 22;
            this.labelChoosePicture.Text = "Choisissez une image pour le fichier";
            this.labelChoosePicture.Visible = false;
            // 
            // textBoxChooseAdditionalPicture
            // 
            this.textBoxChooseAdditionalPicture.Location = new System.Drawing.Point(44, 291);
            this.textBoxChooseAdditionalPicture.Name = "textBoxChooseAdditionalPicture";
            this.textBoxChooseAdditionalPicture.Size = new System.Drawing.Size(236, 20);
            this.textBoxChooseAdditionalPicture.TabIndex = 23;
            this.textBoxChooseAdditionalPicture.Tag = " ";
            this.textBoxChooseAdditionalPicture.Visible = false;
            // 
            // buttonChooseAdditionalPicture
            // 
            this.buttonChooseAdditionalPicture.Location = new System.Drawing.Point(260, 291);
            this.buttonChooseAdditionalPicture.Name = "buttonChooseAdditionalPicture";
            this.buttonChooseAdditionalPicture.Size = new System.Drawing.Size(20, 20);
            this.buttonChooseAdditionalPicture.TabIndex = 24;
            this.buttonChooseAdditionalPicture.Text = "...";
            this.buttonChooseAdditionalPicture.UseVisualStyleBackColor = true;
            this.buttonChooseAdditionalPicture.Visible = false;
            this.buttonChooseAdditionalPicture.Click += new System.EventHandler(this.buttonChooseAdditionalPicture_Click);
            // 
            // pictureBoxDisplayChoosenPicture
            // 
            this.pictureBoxDisplayChoosenPicture.Location = new System.Drawing.Point(289, 275);
            this.pictureBoxDisplayChoosenPicture.Name = "pictureBoxDisplayChoosenPicture";
            this.pictureBoxDisplayChoosenPicture.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxDisplayChoosenPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDisplayChoosenPicture.TabIndex = 25;
            this.pictureBoxDisplayChoosenPicture.TabStop = false;
            this.pictureBoxDisplayChoosenPicture.Visible = false;
            // 
            // labelTitleUpload
            // 
            this.labelTitleUpload.AutoSize = true;
            this.labelTitleUpload.Location = new System.Drawing.Point(44, 86);
            this.labelTitleUpload.Name = "labelTitleUpload";
            this.labelTitleUpload.Size = new System.Drawing.Size(28, 13);
            this.labelTitleUpload.TabIndex = 26;
            this.labelTitleUpload.Text = "Titre";
            // 
            // textBoxTitleUpload
            // 
            this.textBoxTitleUpload.Location = new System.Drawing.Point(44, 102);
            this.textBoxTitleUpload.Name = "textBoxTitleUpload";
            this.textBoxTitleUpload.Size = new System.Drawing.Size(200, 20);
            this.textBoxTitleUpload.TabIndex = 27;
            this.textBoxTitleUpload.Tag = "Mandatory";
            // 
            // buttonUploadFile
            // 
            this.buttonUploadFile.Location = new System.Drawing.Point(180, 390);
            this.buttonUploadFile.Name = "buttonUploadFile";
            this.buttonUploadFile.Size = new System.Drawing.Size(110, 39);
            this.buttonUploadFile.TabIndex = 28;
            this.buttonUploadFile.Text = "Ajouter le fichier";
            this.buttonUploadFile.UseVisualStyleBackColor = true;
            this.buttonUploadFile.Click += new System.EventHandler(this.buttonUploadFile_Click);
            // 
            // labelFileFormat
            // 
            this.labelFileFormat.AutoSize = true;
            this.labelFileFormat.Location = new System.Drawing.Point(401, 16);
            this.labelFileFormat.Name = "labelFileFormat";
            this.labelFileFormat.Size = new System.Drawing.Size(35, 13);
            this.labelFileFormat.TabIndex = 29;
            this.labelFileFormat.Text = "label1";
            this.labelFileFormat.Visible = false;
            // 
            // labelGetFilePath
            // 
            this.labelGetFilePath.AutoSize = true;
            this.labelGetFilePath.Location = new System.Drawing.Point(404, 33);
            this.labelGetFilePath.Name = "labelGetFilePath";
            this.labelGetFilePath.Size = new System.Drawing.Size(35, 13);
            this.labelGetFilePath.TabIndex = 30;
            this.labelGetFilePath.Text = "label1";
            this.labelGetFilePath.Visible = false;
            // 
            // FormAddNewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.labelGetFilePath);
            this.Controls.Add(this.labelFileFormat);
            this.Controls.Add(this.buttonUploadFile);
            this.Controls.Add(this.textBoxTitleUpload);
            this.Controls.Add(this.labelTitleUpload);
            this.Controls.Add(this.pictureBoxDisplayChoosenPicture);
            this.Controls.Add(this.buttonChooseAdditionalPicture);
            this.Controls.Add(this.textBoxChooseAdditionalPicture);
            this.Controls.Add(this.labelChoosePicture);
            this.Controls.Add(this.checkBoxVisibility);
            this.Controls.Add(this.textBoxGenre);
            this.Controls.Add(this.labelGenre);
            this.Controls.Add(this.textBoxTheme);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.comboBoxFileLanguages);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.dateTimePickerMetadata);
            this.Controls.Add(this.textBoxMetadata2);
            this.Controls.Add(this.labelMetadata2);
            this.Controls.Add(this.textBoxMetadata1);
            this.Controls.Add(this.textBoxFileType);
            this.Controls.Add(this.labelMetadata1);
            this.Controls.Add(this.labelFileTypeDesc);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelChooseFile);
            this.Controls.Add(this.buttonChooseFile);
            this.Controls.Add(this.textBoxFilePath);
            this.Name = "FormAddNewFile";
            this.Text = "Ajouter un fichier";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplayChoosenPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonChooseFile;
        private System.Windows.Forms.Label labelChooseFile;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelFileTypeDesc;
        private System.Windows.Forms.Label labelMetadata1;
        private System.Windows.Forms.TextBox textBoxFileType;
        private System.Windows.Forms.TextBox textBoxMetadata1;
        private System.Windows.Forms.TextBox textBoxMetadata2;
        private System.Windows.Forms.Label labelMetadata2;
        private System.Windows.Forms.DateTimePicker dateTimePickerMetadata;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox comboBoxFileLanguages;
        private System.Windows.Forms.Label labelTheme;
        private System.Windows.Forms.TextBox textBoxTheme;
        private System.Windows.Forms.TextBox textBoxGenre;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.CheckBox checkBoxVisibility;
        private System.Windows.Forms.Label labelChoosePicture;
        private System.Windows.Forms.TextBox textBoxChooseAdditionalPicture;
        private System.Windows.Forms.Button buttonChooseAdditionalPicture;
        private System.Windows.Forms.PictureBox pictureBoxDisplayChoosenPicture;
        private System.Windows.Forms.Label labelTitleUpload;
        private System.Windows.Forms.TextBox textBoxTitleUpload;
        private System.Windows.Forms.Button buttonUploadFile;
        private System.Windows.Forms.Label labelFileFormat;
        private System.Windows.Forms.Label labelGetFilePath;
    }
}