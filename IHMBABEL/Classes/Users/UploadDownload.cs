using IHMBABEL.Classes.Enums.EnumFileFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHMBABEL.Classes.Users
{
    public class UploadDownload
    {

        public bool CreateUserDirectory(string name)
        {
            string[] pathMedia = { "Sound", "Video", "Picture", "Book", "Avatar" };
            for (int i = 0; i < pathMedia.Length; i++)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/" + pathMedia[i] + "/" + name);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode.ToString() != "PathnameCreated")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool DeleteUserFile()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/Frauzen/test.txt");
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                if (resp.StatusCode.ToString() != "FileActionOK")
                {
                    return false;
                }
            }
            return true;
        }

        public List<string> GetContent(string path)
        {
            List<string> fileList = new List<string>();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            fileList.Add(reader.ReadToEnd());

            reader.Close();
            response.Close();

            return fileList;
        }

        public string UploadFile(User user, string inputfilepath, string windowFilePath, int idFile)
        {

            string ftpfullpath = null;
            string deleteWords = null;
            string[] words = inputfilepath.Split('\\');
            string fileName = words[words.Length - 1];

            words = fileName.Split('.');
            fileName = string.Format("{0}{1}", idFile, words[0]);
            string fileType = words[words.Length - 1];

            if (fileType == FileFormats.mp3.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.Pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.Pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.Pseudo + "/" + fileName + "/" + fileName + "." + fileType;
                        }
                    }
                }
            }
            else if (fileType == FileFormats.avi.ToString() || fileType == FileFormats.mp4.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.Pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.Pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.Pseudo + "/" + fileName + "/" + fileName + "." + fileType;
                        }
                    }
                }
            }
            else if (fileType == FileFormats.jpg.ToString() || fileType == FileFormats.png.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.Pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.Pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.Pseudo + "/" + fileName + "/" + fileName + "." + fileType;
                        }
                    }
                }
            }
            else if (fileType == FileFormats.pdf.ToString() || fileType == FileFormats.txt.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.Pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.Pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.Pseudo + "/" + fileName + "/" + fileName + "." + fileType;
                        }
                    }
                }
            }
            else
            {
                return "type du fichier non valide (png, txt ...)";
            }

            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                    request.UploadFile(ftpfullpath, windowFilePath);
                }
                return "Tout c'est bien passé, votre fichier est en ligne";
            }
            catch
            {
                words = ftpfullpath.Split('/');
                deleteWords = words[words.Length - 4];
                DeleteDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/" + deleteWords + "/" + user.Pseudo + "/" + fileName);
                return "une erreur c'est produite veuillez réessayer ultérieurement";
            }
        }

        public string UploadAvatar(string pseudo, string inputfilepath)
        {
            string ftpfullpath = null;
            string deleteWords = null;
            string[] words = inputfilepath.Split('\\');
            string fileName = words[words.Length - 1];
            fileName = "avatar.png";

            foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Avatar/" + pseudo))
            {
                ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Avatar/" + pseudo + "/" + fileName;
            }

            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                    request.UploadFile(ftpfullpath, inputfilepath);
                }
                return "Tout c'est bien passé, votre fichier est en ligne";
            }
            catch
            {
                words = ftpfullpath.Split('/');
                deleteWords = words[words.Length - 4];
                DeleteDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Avatar/" + pseudo + "/" + fileName);
                return "une erreur c'est produite veuillez réessayer ultérieurement";
            }
        }

        private bool DeleteDirectory(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                if (resp.StatusCode.ToString() != "PathnameCreated")
                {
                    return false;
                }
            }
            return true;
        }

        private bool CreateDirectory(string path)
        {
            string[] words = path.Split('/');
            string directory = words[words.Length - 1];
            string contentPath = words[0] + "/" + words[1] + "/" + words[2] + "/" + words[3] + "/" + words[4] + "/" + words[5];

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
            try
            {
                FtpWebResponse resp = request.GetResponse() as FtpWebResponse;
                if (resp.StatusCode.ToString() != "PathnameCreated")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                foreach (var key in GetContent(contentPath))
                {
                    if (key.ToString() == directory)
                    {
                        return true;
                    }
                }
                return false;
                throw ex;
            }
        }

        public void DownloadFile(string inputfilepath, string ftpfullpath)
        {

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                byte[] fileData = request.DownloadData(ftpfullpath);

                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
                Console.WriteLine("Download Complete");
            }
        }

        public string UploadAdditionPicture(string ftpFilePath, string uploaderFilePath, PictureBox picture)
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("luc.rodiere", "765GHW");
                    request.UploadFile(ftpFilePath, uploaderFilePath);
                }
                return "Tout s'est bien passé, votre fichier est en ligne";
            }
            catch
            {
                return "Une erreur s'est produite veuillez réessayer ultérieurement";
            }
        }

    }
}
