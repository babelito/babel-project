using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Enums.EnumFileLanguage;
using IHMBABEL.Classes.Users;
using System.Net;
using IHMBABEL.Classes.Files.FileTypes;

namespace IHMBABEL.Classes.Managers
{
    public class FileManager
    {
        private string connectionString;
        private SqlConnection connection;

        public FileManager()
        {
            try
            {
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Open()
        {
            if (connection.State == System.Data.ConnectionState.Closed && connection != null)
                connection.Open();
        }

        private void Close()
        {
            if (connection.State == System.Data.ConnectionState.Open && connection != null)
                connection.Close();
        }

        public int GetId()
        {
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT max(idFile) FROM files");
            command1.Connection = this.connection;
            int id = Convert.ToInt32(command1.ExecuteScalar()) + 1;
            return id;
        }

        public List<BabelFiles> GetFiles(SqlCommand command)
        {
            try
            {
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<BabelFiles> listFiles = new List<BabelFiles>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                        BabelFiles file = new BabelFiles
                            (
                                reader.GetInt32(reader.GetOrdinal("idFile")),
                                reader.GetString(reader.GetOrdinal("title")),
                                String.Empty,
                                String.Empty,
                                null,
                                reader.GetInt32(reader.GetOrdinal("notation")),
                                1,
                                String.Empty,
                                reader.GetString(reader.GetOrdinal("fileLanguage")),
                                string.Empty,
                                reader.GetInt32(reader.GetOrdinal("nbViews")),
                                reader.GetString(reader.GetOrdinal("picture")),
                                user
                            );
                        listFiles.Add(file);
                    }
                }
                Close();
                return listFiles;
            }
            catch (Exception ex)
            {
                connection.Close();
                command.Connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Connection.Close();
            }
        }
        public string GetFileType(SqlCommand command, int id)
        {
            try
            {
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                string fileType = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            if (reader.GetInt32(i) == id)
                            {
                                fileType = reader.GetName(i);
                            }
                        }
                    }
                }
                Close();
                return fileType;
            }
            catch (Exception ex)
            {
                connection.Close();
                command.Connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Connection.Close();
            }
        }

        public string CreateBook(Book book, string filePath, string windowFilePath)
        {
            try
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT max(idFile) FROM files");
                command1.Connection = this.connection;
                book.IdFile = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                if (book.Picture == "")
                {
                    book.Picture = string.Format("http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Book/Default/defaultBook.png");
                }


                String queryFile = @"INSERT INTO files VALUES (@idFile, @accessPath, @fileFormat, @created, @notation, @visibility, 
                                        @title, @genre, @fileLanguage, @theme, @nbViews, @fileOwner, @picture);";
                String queryBook = "INSERT INTO book VALUES (@idBook, @editor, @releaseDate);";
                SqlCommand commandFile = new SqlCommand(queryFile, connection);
                commandFile.Parameters.AddWithValue("@idFile", book.IdFile);
                commandFile.Parameters.AddWithValue("@accessPath", book.Directory);
                commandFile.Parameters.AddWithValue("@fileFormat", book.FileFormat.ToString());
                commandFile.Parameters.AddWithValue("@created", book.DateCreated);
                commandFile.Parameters.AddWithValue("@notation", book.Notation);
                commandFile.Parameters.AddWithValue("@visibility", book.Visibility);
                commandFile.Parameters.AddWithValue("@title", book.Title);
                commandFile.Parameters.AddWithValue("@genre", book.Genre);
                commandFile.Parameters.AddWithValue("@fileLanguage", book.FileLanguage);
                commandFile.Parameters.AddWithValue("@theme", book.Theme);
                commandFile.Parameters.AddWithValue("@nbViews", book.NbViews);
                commandFile.Parameters.AddWithValue("@fileOwner", book.FileOwner.IdUser);
                commandFile.Parameters.AddWithValue("@picture", book.Picture);
                commandFile.ExecuteNonQuery();

                SqlCommand commandBook = new SqlCommand(queryBook, connection);
                commandBook.Parameters.AddWithValue("@idBook", book.IdFile);
                commandBook.Parameters.AddWithValue("@editor", book.Editor);
                commandBook.Parameters.AddWithValue("@releaseDate", book.ReleaseDate);
                commandBook.ExecuteNonQuery();

                UploadDownload ud = new UploadDownload();
                ud.UploadFile(book.FileOwner, filePath, windowFilePath, book.IdFile);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal string CreateSong(Song song, string filePath, string windowFilePath)
        {
            try
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT max(idFile) FROM files");
                command1.Connection = this.connection;
                song.IdFile = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                if (song.Picture == "")
                {
                    song.Picture = string.Format("http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Sound/Default/defaultBook.png");
                }


                String queryFile = @"INSERT INTO files VALUES (@idFile, @accessPath, @fileFormat, @created, @notation, @visibility, 
                                        @title, @genre, @fileLanguage, @theme, @nbViews, @fileOwner, @picture);";
                String queryBook = "INSERT INTO song VALUES (@idSong, @duration);";
                SqlCommand commandFile = new SqlCommand(queryFile, connection);
                commandFile.Parameters.AddWithValue("@idFile", song.IdFile);
                commandFile.Parameters.AddWithValue("@accessPath", song.Directory);
                commandFile.Parameters.AddWithValue("@fileFormat", song.FileFormat.ToString());
                commandFile.Parameters.AddWithValue("@created", song.DateCreated);
                commandFile.Parameters.AddWithValue("@notation", song.Notation);
                commandFile.Parameters.AddWithValue("@visibility", song.Visibility);
                commandFile.Parameters.AddWithValue("@title", song.Title);
                commandFile.Parameters.AddWithValue("@genre", song.Genre);
                commandFile.Parameters.AddWithValue("@fileLanguage", song.FileLanguage);
                commandFile.Parameters.AddWithValue("@theme", song.Theme);
                commandFile.Parameters.AddWithValue("@nbViews", song.NbViews);
                commandFile.Parameters.AddWithValue("@fileOwner", song.FileOwner.IdUser);
                commandFile.Parameters.AddWithValue("@picture", song.Picture);
                commandFile.ExecuteNonQuery();

                SqlCommand commandBook = new SqlCommand(queryBook, connection);
                commandBook.Parameters.AddWithValue("@idSong", song.IdFile);
                commandBook.Parameters.AddWithValue("@duration", song.Duration);
                commandBook.ExecuteNonQuery();

                UploadDownload ud = new UploadDownload();
                ud.UploadFile(song.FileOwner, filePath, windowFilePath, song.IdFile);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal string CreateVideo(Video video, string filePath, string windowFilePath)
        {
            try
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT max(idFile) FROM files");
                command1.Connection = this.connection;
                video.IdFile = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                if (video.Picture == "")
                {
                    video.Picture = string.Format("http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Book/Default/defaultBook.png");
                }


                String queryFile = @"INSERT INTO files VALUES (@idFile, @accessPath, @fileFormat, @created, @notation, @visibility, 
                                        @title, @genre, @fileLanguage, @theme, @nbViews, @fileOwner, @picture);";
                String queryBook = "INSERT INTO video VALUES (@idVideo, @duration);";
                SqlCommand commandFile = new SqlCommand(queryFile, connection);
                commandFile.Parameters.AddWithValue("@idFile", video.IdFile);
                commandFile.Parameters.AddWithValue("@accessPath", video.Directory);
                commandFile.Parameters.AddWithValue("@fileFormat", video.FileFormat.ToString());
                commandFile.Parameters.AddWithValue("@created", video.DateCreated);
                commandFile.Parameters.AddWithValue("@notation", video.Notation);
                commandFile.Parameters.AddWithValue("@visibility", video.Visibility);
                commandFile.Parameters.AddWithValue("@title", video.Title);
                commandFile.Parameters.AddWithValue("@genre", video.Genre);
                commandFile.Parameters.AddWithValue("@fileLanguage", video.FileLanguage);
                commandFile.Parameters.AddWithValue("@theme", video.Theme);
                commandFile.Parameters.AddWithValue("@nbViews", video.NbViews);
                commandFile.Parameters.AddWithValue("@fileOwner", video.FileOwner.IdUser);
                commandFile.Parameters.AddWithValue("@picture", video.Picture);
                commandFile.ExecuteNonQuery();

                SqlCommand commandBook = new SqlCommand(queryBook, connection);
                commandBook.Parameters.AddWithValue("@idVideo", video.IdFile);
                commandBook.Parameters.AddWithValue("@duration", video.Duration);
                commandBook.ExecuteNonQuery();

                UploadDownload ud = new UploadDownload();
                ud.UploadFile(video.FileOwner, filePath, windowFilePath, video.IdFile);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal string CreatePicture(Picture picture, string filePath, string windowFilePath)
        {
            try
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT max(idFile) FROM files");
                command1.Connection = this.connection;
                picture.IdFile = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                if (picture.Picture == "")
                {
                    picture.Picture = string.Format("http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Book/Default/defaultBook.png");
                }


                String queryFile = @"INSERT INTO files VALUES (@idFile, @accessPath, @fileFormat, @created, @notation, @visibility, 
                                        @title, @genre, @fileLanguage, @theme, @nbViews, @fileOwner, @picture);";
                String queryBook = "INSERT INTO photo VALUES (@idPicture, @size);";
                SqlCommand commandFile = new SqlCommand(queryFile, connection);
                commandFile.Parameters.AddWithValue("@idFile", picture.IdFile);
                commandFile.Parameters.AddWithValue("@accessPath", picture.Directory);
                commandFile.Parameters.AddWithValue("@fileFormat", picture.FileFormat.ToString());
                commandFile.Parameters.AddWithValue("@created", picture.DateCreated);
                commandFile.Parameters.AddWithValue("@notation", picture.Notation);
                commandFile.Parameters.AddWithValue("@visibility", picture.Visibility);
                commandFile.Parameters.AddWithValue("@title", picture.Title);
                commandFile.Parameters.AddWithValue("@genre", picture.Genre);
                commandFile.Parameters.AddWithValue("@fileLanguage", picture.FileLanguage);
                commandFile.Parameters.AddWithValue("@theme", picture.Theme);
                commandFile.Parameters.AddWithValue("@nbViews", picture.NbViews);
                commandFile.Parameters.AddWithValue("@fileOwner", picture.FileOwner.IdUser);
                commandFile.Parameters.AddWithValue("@picture", picture.Picture);
                commandFile.ExecuteNonQuery();

                string size = string.Format("{0}x{1}", picture.Width, picture.Height);

                SqlCommand commandBook = new SqlCommand(queryBook, connection);
                commandBook.Parameters.AddWithValue("@idPicture", picture.IdFile);
                commandBook.Parameters.AddWithValue("@size", size);
                commandBook.ExecuteNonQuery();

                UploadDownload ud = new UploadDownload();
                ud.UploadFile(picture.FileOwner, filePath, windowFilePath, picture.IdFile);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal void IncrementNbViews(int idFile)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Format("UPDATE files SET nbViews = nbViews + 1 WHERE idFile = {0}", idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];
                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<BabelFiles> GetALLFiles(SqlCommand command)
        {
            try
            {
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<BabelFiles> listFiles = new List<BabelFiles>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                        BabelFiles file = new BabelFiles
                            (
                                reader.GetInt32(reader.GetOrdinal("idFile")),
                                reader.GetString(reader.GetOrdinal("title")),
                                String.Empty,
                                String.Empty,
                                reader.GetDateTime(reader.GetOrdinal("created")),
                                reader.GetInt32(reader.GetOrdinal("notation")),
                                reader.GetInt32(reader.GetOrdinal("visibility")),
                                reader.GetString(reader.GetOrdinal("genre")),
                                reader.GetString(reader.GetOrdinal("fileLanguage")),
                                reader.GetString(reader.GetOrdinal("theme")),
                                reader.GetInt32(reader.GetOrdinal("nbViews")),
                                reader.GetString(reader.GetOrdinal("picture")),
                                user
                            );
                        listFiles.Add(file);
                    }
                }
                Close();
                return listFiles;
            }
            catch (Exception ex)
            {
                connection.Close();
                command.Connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Connection.Close();
            }
        }

        public bool UpdateNote(int note, int idFile, int idUser)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM files_notation");

            connection.Open();
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["DataBaseAccess"];

            connectionString = settings.ConnectionString;
            command.Connection = new SqlConnection(connectionString);
            command.Connection.Open();

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (idUser == reader.GetInt32(reader.GetOrdinal("notation_IdUser")))
                        {
                            if (idFile == reader.GetInt32(reader.GetOrdinal("notation_IdFile")))
                            {

                                String query = string.Format("UPDATE files_notation SET notation_Notation = {0} WHERE notation_IdUser = {1} AND notation_IdFile = {2}", note, idUser, idFile);
                                SqlCommand command2 = new SqlCommand(query, connection);

                                try
                                {
                                    command2.ExecuteNonQuery();
                                     return true;
                                }
                                catch (Exception ex)
                                {
                                    return false;
                                    throw ex;
                                }

                            }
                        }

                    }
                }

                String query2 = "INSERT INTO files_notation VALUES (@notation_IdFile, @notation_IdUser, @notation_Notation)";
                SqlCommand command3 = new SqlCommand(query2, connection);

                try
                {
                    command3.Parameters.AddWithValue("@notation_IdFile", idFile);
                    command3.Parameters.AddWithValue("@notation_IdUser", idUser);
                    command3.Parameters.AddWithValue("@notation_Notation", note);

                    command3.ExecuteNonQuery();
                    return true;

                } catch (Exception ex)
                {
                    return false;
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                connection.Close();
            }
            
        }

    }
}
