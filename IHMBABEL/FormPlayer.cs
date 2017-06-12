using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHMBABEL
{
    public partial class FormPlayer : Form
    {
        private string filePath;
        private string fileType;
        public FormPlayer()
        {
            InitializeComponent();
        }

        public void GetFtp(string filePath)
        {
            this.filePath = filePath;
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = filePath;
        }

        private void FormPlayer_StopPlayer(object sender, FormClosedEventArgs e)
        {
            axWindowsMediaPlayer.close();
            string downloadFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            downloadFilePath = downloadFilePath.Substring(0, downloadFilePath.LastIndexOf('\\'));
            downloadFilePath = downloadFilePath.Substring(0, downloadFilePath.LastIndexOf('\\'));
            downloadFilePath = downloadFilePath + "\\temp";
            string tempFileComputer = downloadFilePath + "\\tempFile.mp3";
            File.Delete(tempFileComputer);
        }

        internal void GetFileType(string fileType)
        {
            this.fileType = fileType;
        }
    }
}
