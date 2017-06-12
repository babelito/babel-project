using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Enums.EnumFileLanguage;
using IHMBABEL.Classes.Files.FileTypes;
using IHMBABEL.Classes.Managers;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace IHMBABEL
{
    public partial class FormAddNewFile : Form
    {
        public FormAddNewFile()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(FileLanguages)))
            {
                comboBoxFileLanguages.Items.Add(item);
            }
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Selectionner un fichier";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxMetadata1.Visible = false;
                textBoxMetadata2.Visible = false;
                textBoxMetadata1.Text = "";
                textBoxMetadata2.Text = "";
                labelMetadata1.Visible = false;
                labelMetadata2.Visible = false;
                dateTimePickerMetadata.Visible = false;
                labelChoosePicture.Visible = false;
                buttonChooseAdditionalPicture.Visible = false;
                textBoxChooseAdditionalPicture.Visible = false;
                pictureBoxDisplayChoosenPicture.Visible = false;


                textBoxFilePath.Text = dlg.FileName;
                UploadDownload ud = new UploadDownload();
                string ext = Path.GetExtension(dlg.FileName);
                string extWithoutDot = ext.Remove(0, 1);
                labelFileFormat.Text = extWithoutDot;
                if (extWithoutDot == FileFormats.txt.ToString() || extWithoutDot == FileFormats.pdf.ToString())
                {
                    textBoxFileType.Text = "Livre";
                    textBoxFileType.Tag = "book";
                    labelGetFilePath.Text = "Book";

                    labelMetadata1.Text = "Editeur";
                    labelMetadata1.Visible = true;

                    textBoxMetadata1.Visible = true;

                    labelMetadata2.Text = "Date de sortie";
                    labelMetadata2.Visible = true;
                    dateTimePickerMetadata.Visible = true;

                    labelChoosePicture.Visible = true;
                    buttonChooseAdditionalPicture.Visible = true;
                    textBoxChooseAdditionalPicture.Visible = true;
                    pictureBoxDisplayChoosenPicture.Visible = true;
                }
                else if (extWithoutDot == FileFormats.jpg.ToString() || extWithoutDot == FileFormats.png.ToString())
                {
                    textBoxFileType.Text = "Image";
                    textBoxFileType.Tag = "picture";
                    labelGetFilePath.Text = "Picture";


                    FileInfo file = new FileInfo(dlg.FileName);

                    Bitmap img = new Bitmap(dlg.FileName);

                    var imageHeight = img.Height;
                    var imageWidth = img.Width;

                    labelMetadata1.Text = "Hauteur";
                    labelMetadata1.Visible = true;
                    textBoxMetadata1.Visible = true;
                    textBoxMetadata1.Text = imageHeight.ToString();


                    labelMetadata2.Text = "Largeur";
                    labelMetadata2.Visible = true;
                    textBoxMetadata2.Visible = true;
                    textBoxMetadata2.Text = imageWidth.ToString();
                }
                else if (extWithoutDot == FileFormats.avi.ToString() || extWithoutDot == FileFormats.mp4.ToString())
                {
                    WindowsMediaPlayer wmp = new WindowsMediaPlayer();
                    IWMPMedia mediainfo = wmp.newMedia(dlg.FileName);
                    textBoxFileType.Text = "Video";
                    textBoxFileType.Tag = "video";
                    labelGetFilePath.Text = "Video";

                    labelMetadata1.Text = "Durée (sec)";
                    labelMetadata1.Visible = true;
                    textBoxMetadata1.Text = mediainfo.duration.ToString();
                    textBoxMetadata1.Visible = true;


                    labelChoosePicture.Visible = true;
                    buttonChooseAdditionalPicture.Visible = true;
                    textBoxChooseAdditionalPicture.Visible = true;
                    pictureBoxDisplayChoosenPicture.Visible = true;
                }
                else if (extWithoutDot == FileFormats.mp3.ToString())
                {
                    WindowsMediaPlayer wmp = new WindowsMediaPlayer();
                    IWMPMedia mediainfo = wmp.newMedia(dlg.FileName);
                    textBoxFileType.Text = "Musique/Son";
                    textBoxFileType.Tag = "song";
                    labelGetFilePath.Text = "Sound";

                    labelMetadata1.Text = "Durée (sec)";
                    labelMetadata1.Visible = true;
                    textBoxMetadata1.Text = mediainfo.duration.ToString();

                    textBoxMetadata1.Visible = true;


                    labelChoosePicture.Visible = true;
                    buttonChooseAdditionalPicture.Visible = true;
                    textBoxChooseAdditionalPicture.Visible = true;
                    pictureBoxDisplayChoosenPicture.Visible = true;
                }
                else
                {
                    textBoxFileType.Text = "Format inconnu, vérifiez";
                    textBoxFileType.Tag = "unknown";
                }
            }
        }

        private void buttonChooseAdditionalPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Selectionner une image additionnelle";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                byte[] fileData = File.ReadAllBytes(dlg.FileName);
                pictureBoxDisplayChoosenPicture.Image = ByteToImage(fileData);
                textBoxChooseAdditionalPicture.Text = dlg.FileName;
            }
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

        private void buttonUploadFile_Click(object sender, EventArgs e)
        {
            bool isUploadable = false;
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.BackColor = Color.White;
                if (tb.Tag.ToString() == "Mandatory" && tb.Text.ToString() == "")
                {
                    tb.BackColor = Color.Red;
                }
                else
                {
                    isUploadable = true;
                }

                if (tb.Name.ToString() == "textBoxFileType" && tb.Tag.ToString() == " ")
                {
                    isUploadable = false;
                    tb.BackColor = Color.Red;
                }
            }
            if (isUploadable)
            {
                UploadDownload ud = new UploadDownload();
                Book book = null;
                Song song = null;
                Video video = null;
                Picture picture = null;
                
                FileManager fm = new FileManager();
                int idFile = fm.GetId();
                string fileFormat = labelFileFormat.Text.ToString();
                DateTime created = DateTime.Now;
                float notation = -2f;
                int visibility = 1;
                if(!checkBoxVisibility.Checked)
                {
                    visibility = 0;
                }
                string title = textBoxTitleUpload.Text.ToString();
                string genre = textBoxGenre.Text.ToString();
                string fileLanguage = comboBoxFileLanguages.Text.ToString();
                string theme = textBoxTheme.Text.ToString();
                int nbViews = 0;
                string filePath = textBoxFilePath.Text.ToString();
                string filePathSlash = filePath.Replace('\\', '/');
                string windowsAccessPath = string.Format("{0}", filePathSlash.Remove(filePathSlash.LastIndexOf('/'), filePathSlash.LastIndexOf('.') - filePathSlash.LastIndexOf('/')));
                windowsAccessPath = windowsAccessPath.Insert(windowsAccessPath.IndexOf('.'), "\\" + title);
                string accessPath = string.Format("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/{0}/{1}/{4}{2}/{4}{2}.{3}", labelGetFilePath.Text.ToString(), ConnectedUser.user.Pseudo, textBoxTitleUpload.Text.ToString(), fileFormat, idFile);
                User fileOwner = ConnectedUser.user;
                string additionalPicture = null;

                if (textBoxChooseAdditionalPicture.Text != "")
                {
                    additionalPicture = string.Format("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/{0}/{1}/{3}{2}/picture.png", labelGetFilePath.Text.ToString(), ConnectedUser.user.Pseudo, textBoxTitleUpload.Text.ToString(), idFile);
                } else
                {
                    additionalPicture = string.Format("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Avatar/joshua.png");
                }

                if (textBoxFileType.Tag.ToString() == "book")
                {
                    string editor = labelMetadata1.Text.ToString();
                    DateTime release = Convert.ToDateTime(dateTimePickerMetadata.Text.ToString());
                    book = new Book(idFile, title, accessPath, fileFormat, created, notation, visibility, genre, fileLanguage, theme, nbViews, additionalPicture, fileOwner, 
                        editor, release);

                    CreateFileEntryDBBook(book, windowsAccessPath, filePath);
                    ud.UploadAdditionPicture(book.Picture, textBoxChooseAdditionalPicture.Text.ToString(), pictureBoxDisplayChoosenPicture);
                }
                else if (textBoxFileType.Tag.ToString() == "song")
                {
                    int duration = Convert.ToInt32(textBoxMetadata1.Text.ToString().Remove(textBoxMetadata1.Text.ToString().IndexOf(',')));
                    song = new Song(idFile, title, accessPath, fileFormat, created, notation, visibility, genre, fileLanguage, theme, nbViews, additionalPicture, fileOwner,
                        duration);
                    CreateFileEntryDBSong(song, windowsAccessPath, filePath);
                    ud.UploadAdditionPicture(song.Picture, textBoxChooseAdditionalPicture.Text.ToString(), pictureBoxDisplayChoosenPicture);
                }
                else if (textBoxFileType.Tag.ToString() == "video")
                {
                    int duration = Convert.ToInt32(textBoxMetadata1.Text.ToString());
                    video = new Video(idFile, title, accessPath, fileFormat, created, notation, visibility, genre, fileLanguage, theme, nbViews, additionalPicture, fileOwner,
                        duration);
                    CreateFileEntryDBVideo(video, windowsAccessPath, filePath);
                    ud.UploadAdditionPicture(video.Picture, textBoxChooseAdditionalPicture.Text.ToString(), pictureBoxDisplayChoosenPicture);
                }
                if (textBoxFileType.Tag.ToString() == "picture")
                {
                    additionalPicture = accessPath;

                    int width = Convert.ToInt32(textBoxMetadata1.Text.ToString());
                    int height = Convert.ToInt32(textBoxMetadata2.Text.ToString());
                    picture = new Picture(idFile, title, accessPath, fileFormat, created, notation, visibility, genre, fileLanguage, theme, nbViews, additionalPicture, fileOwner,
                        width, height);
                    CreateFileEntryDBPicture(picture, windowsAccessPath, filePath);
                }

                Label labelOK = new Label();
                labelOK.Size = new Size(200, 25);
                labelOK.AutoSize = true;
                labelOK.Font = new System.Drawing.Font("Verdana", 12F);
                labelOK.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                labelOK.Text = string.Format("Fichier ajouter avec succès.");
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
                    this.Close();
                }

            }
        }

        private void CreateFileEntryDBPicture(Picture picture, string filePath, string windowFilePath)
        {
            FileManager fileManager = new FileManager();
            fileManager.CreatePicture(picture, filePath, windowFilePath);
        }

        private void CreateFileEntryDBVideo(Video video, string filePath, string windowFilePath)
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateVideo(video, filePath, windowFilePath);
        }

        private void CreateFileEntryDBSong(Song song, string filePath, string windowFilePath)
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateSong(song, filePath, windowFilePath);
        }

        private void CreateFileEntryDBBook(Book book, string filePath, string windowFilePath)
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateBook(book, filePath, windowFilePath);
        }
    }
}
