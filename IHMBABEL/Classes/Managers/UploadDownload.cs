using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace windowsFormUpload
{
    public class UploadDownload
    {

        public bool CreateUserDirectory(string name)
        {
            string[] pathMedia = { "Sound", "Video", "Picture", "Book" };
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

        public string UploadFile(User user, string inputfilepath)
        {

            string ftpfullpath = null;
            string deleteWords = null;
            string[] words = inputfilepath.Split('\\');
            string fileName = words[words.Length - 1];

            words = fileName.Split('.');
            string fileType = words[words.Length - 1];

            if (fileType == EnumFile.FileFormats.mp3.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/" + user.pseudo + "/" + fileName + "/" + fileName;
                        }
                    }
                }
            }
            else if (fileType == EnumFile.FileFormats.avi.ToString() || fileType == EnumFile.FileFormats.mp4.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Video/" + user.pseudo + "/" + fileName + "/" + fileName;
                        }
                    }
                }
            }
            else if (fileType == EnumFile.FileFormats.jpg.ToString() || fileType == EnumFile.FileFormats.png.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Picture/" + user.pseudo + "/" + fileName + "/" + fileName;
                        }
                    }
                }
            }
            else if (fileType == EnumFile.FileFormats.pdf.ToString() || fileType == EnumFile.FileFormats.txt.ToString())
            {
                foreach (var key in GetContent("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.pseudo))
                {
                    if (key.Contains(fileName))
                    {
                        return "le fichier existe déjà";
                    }
                    else
                    {
                        if (CreateDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.pseudo + "/" + fileName))
                        {
                            ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Book/" + user.pseudo + "/" + fileName + "/" + fileName;
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
                    request.UploadFile(ftpfullpath, inputfilepath);
                }
                return "Tout c'est bien passé, votre fichier est en ligne";
            }
            catch
            {
                words = ftpfullpath.Split('/');
                deleteWords = words[words.Length - 4];
                DeleteDirectory("ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/" + deleteWords + "/" + user.pseudo + "/" + fileName);
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
            }
        }

        public void DownloadFile()
        {

            string inputfilepath = @"C:\Users\Luc rodiere\Desktop\ici\test.txt";

            string ftpfullpath = "ftp://luc.rodiere@ftp.montpellier.epsi.fr:4621/Babel/Sound/Frauzen/test.txt";

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

    }
}
