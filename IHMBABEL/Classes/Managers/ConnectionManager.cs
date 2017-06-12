using IHMBABEL.Classes.Files.FileTypes;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Managers
{
    class ConnectionManager
    {

        private string connectionString;
        private SqlConnection connection;

        public ConnectionManager()
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

        public List<User> GetUsers(SqlCommand command)
        {
            try
            {
                connection.Open();
                command.Connection = this.connection;

                SqlDataReader reader = command.ExecuteReader();

                List<User> listUsers = new List<User>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User
                            (
                                reader.GetInt32(reader.GetOrdinal("idUser")),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("pseudo")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("password")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("FirstName")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("LastName")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("mail")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("pic")))),
                                reader.GetDateTime(reader.GetOrdinal("account_created"))
                            );
                        listUsers.Add(user);
                    }
                }
                Close();
                return listUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Connection.Close();
            }
        }

        public User GetUser(SqlCommand command)
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
                reader.Read();

                User user = new User
                            (
                                reader.GetInt32(reader.GetOrdinal("idUser")),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("pseudo")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("password")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("FirstName")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("LastName")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("mail")))),
                                Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("pic")))),
                                reader.GetDateTime(reader.GetOrdinal("account_created"))
                            );
                Close();
                return user;
            }
            catch (Exception ex)
            {
                connection.Close();
                command.Connection.Close();
                User user = new User();
                return user;
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Connection.Close();
            }
        }

        internal Book GetAdditionalBookInfo(string fileType, int idFile)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM files, {0}, users where files.idFile = {0}.id{0} AND {0}.id{0} = {1} AND users.idUser = files.fileOwner ", fileType, idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                Book book = new Book(
                    reader.GetInt32(reader.GetOrdinal("idFile")),
                    reader.GetString(reader.GetOrdinal("title")),
                    reader.GetString(reader.GetOrdinal("accessPath")),
                    reader.GetString(reader.GetOrdinal("fileFormat")),
                    reader.GetDateTime(reader.GetOrdinal("created")),
                    reader.GetInt32(reader.GetOrdinal("notation")),
                    reader.GetInt32(reader.GetOrdinal("visibility")),
                    reader.GetString(reader.GetOrdinal("genre")),
                    reader.GetString(reader.GetOrdinal("fileLanguage")),
                    reader.GetString(reader.GetOrdinal("theme")),
                    reader.GetInt32(reader.GetOrdinal("nbViews")),
                    reader.GetString(reader.GetOrdinal("picture")),
                    user,
                    reader.GetString(reader.GetOrdinal("editor")),
                    reader.GetDateTime(reader.GetOrdinal("releaseDate"))
                    );
                command.Connection.Close();
                return book;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal Song GetAdditionalSongInfo(string fileType, int idFile)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM files, {0}, users where files.idFile = {0}.id{0} AND {0}.id{0} = {1} AND users.idUser = files.fileOwner ", fileType, idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                Song song = new Song(
                    reader.GetInt32(reader.GetOrdinal("idFile")),
                    reader.GetString(reader.GetOrdinal("title")),
                    reader.GetString(reader.GetOrdinal("accessPath")),
                    reader.GetString(reader.GetOrdinal("fileFormat")),
                    reader.GetDateTime(reader.GetOrdinal("created")),
                    reader.GetInt32(reader.GetOrdinal("notation")),
                    reader.GetInt32(reader.GetOrdinal("visibility")),
                    reader.GetString(reader.GetOrdinal("genre")),
                    reader.GetString(reader.GetOrdinal("fileLanguage")),
                    reader.GetString(reader.GetOrdinal("theme")),
                    reader.GetInt32(reader.GetOrdinal("nbViews")),
                    reader.GetString(reader.GetOrdinal("picture")),
                    user,
                    reader.GetInt32(reader.GetOrdinal("duration"))
                    );
                command.Connection.Close();
                return song;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal Video GetAdditionalVideoInfo(string fileType, int idFile)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM files, {0}, users where files.idFile = {0}.id{0} AND {0}.id{0} = {1} AND users.idUser = files.fileOwner ", fileType, idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                Video video = new Video(
                    reader.GetInt32(reader.GetOrdinal("idFile")),
                    reader.GetString(reader.GetOrdinal("title")),
                    reader.GetString(reader.GetOrdinal("accessPath")),
                    reader.GetString(reader.GetOrdinal("fileFormat")),
                    reader.GetDateTime(reader.GetOrdinal("created")),
                    reader.GetInt32(reader.GetOrdinal("notation")),
                    reader.GetInt32(reader.GetOrdinal("visibility")),
                    reader.GetString(reader.GetOrdinal("genre")),
                    reader.GetString(reader.GetOrdinal("fileLanguage")),
                    reader.GetString(reader.GetOrdinal("theme")),
                    reader.GetInt32(reader.GetOrdinal("nbViews")),
                    reader.GetString(reader.GetOrdinal("picture")),
                    user,
                    reader.GetInt32(reader.GetOrdinal("duration"))
                    );
                command.Connection.Close();
                return video;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal Picture GetAdditionalPhotoInfo(string fileType, int idFile)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM files, {0}, users where files.idFile = {0}.id{0} AND {0}.id{0} = {1} AND users.idUser = files.fileOwner ", fileType, idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                User user = new User(reader.GetInt32(reader.GetOrdinal("fileOwner")), reader.GetString(reader.GetOrdinal("pseudo")), null, null, null, null, null);

                string size = reader.GetString(reader.GetOrdinal("size"));
                string width = size.Substring(0, size.IndexOf('x'));
                string height = size.Substring(size.IndexOf('x') + 1);

                Picture picture = new Picture(
                    reader.GetInt32(reader.GetOrdinal("idFile")),
                    reader.GetString(reader.GetOrdinal("title")),
                    reader.GetString(reader.GetOrdinal("accessPath")),
                    reader.GetString(reader.GetOrdinal("fileFormat")),
                    reader.GetDateTime(reader.GetOrdinal("created")),
                    reader.GetInt32(reader.GetOrdinal("notation")),
                    reader.GetInt32(reader.GetOrdinal("visibility")),
                    reader.GetString(reader.GetOrdinal("genre")),
                    reader.GetString(reader.GetOrdinal("fileLanguage")),
                    reader.GetString(reader.GetOrdinal("theme")),
                    reader.GetInt32(reader.GetOrdinal("nbViews")),
                    reader.GetString(reader.GetOrdinal("picture")),
                    user,
                    Convert.ToInt32(width),
                    Convert.ToInt32(height)
                    );
                command.Connection.Close();
                return picture;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        internal Dictionary<Comment, User> GetListComment(int idFile, ref bool anyComment)
        {
            try
            {
                Dictionary<Comment, User> dictionaryComments = new Dictionary<Comment, User>();
                SqlCommand command = new SqlCommand(string.Format("SELECT * FROM comments, users  where file_ = {0} and idUser = user_Poster", idFile));
                connection.Open();
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings["DataBaseAccess"];

                connectionString = settings.ConnectionString;
                command.Connection = new SqlConnection(connectionString);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    anyComment = true;
                    while (reader.Read())
                    {
                        dictionaryComments.Add(
                                new Comment(
                                        reader.GetInt32(reader.GetOrdinal("idComment")),
                                        reader.GetString(reader.GetOrdinal("content")),
                                        reader.GetInt32(reader.GetOrdinal("user_Poster")),
                                        reader.GetInt32(reader.GetOrdinal("file_"))
                                    ),
                                new User(
                                        reader.GetInt32(reader.GetOrdinal("idUser")),
                                        reader.GetString(reader.GetOrdinal("pseudo")),
                                        reader.GetString(reader.GetOrdinal("password")),
                                        reader.GetString(reader.GetOrdinal("firstName")),
                                        reader.GetString(reader.GetOrdinal("lastName")),
                                        reader.GetString(reader.GetOrdinal("mail")),
                                        reader.GetString(reader.GetOrdinal("pic")),
                                        reader.GetDateTime(reader.GetOrdinal("account_Created"))
                                    )
                                );
                    }
                }
                command.Connection.Close();
                return dictionaryComments;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddComment(int idComment, string content, int idUserPoster, int idFile)
        {

            SqlCommand command1 = new SqlCommand("INSERT INTO comments (content, user_Poster, file_) VALUES (@content, @user_Poster, @file_)");

            try
            {
                connection.Open();
                command1.Connection = this.connection;

                command1.Parameters.AddWithValue("@content", content);
                command1.Parameters.AddWithValue("@user_Poster", idUserPoster);
                command1.Parameters.AddWithValue("@file_", idFile);


                command1.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command1.Connection.Close();
            }
        }

    }
}
