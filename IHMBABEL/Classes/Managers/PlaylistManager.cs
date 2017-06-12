using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHMBABEL.Classes.Users;

namespace IHMBABEL.Classes.Managers
{
    class PlaylistManager
    {
        private string connectionString;
        private SqlConnection connection;

        public PlaylistManager()
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

        public void AddToPlaylist(int idFile, int idPlaylist)
        {
            SqlCommand commandAdd = new SqlCommand("INSERT INTO files_in_playlist VALUES (@idPlaylist, @idFile)");
            commandAdd.Parameters.AddWithValue("@idPlaylist", idPlaylist);
            commandAdd.Parameters.AddWithValue("@idFile", idFile);

            try
            {
                Open();
                commandAdd.Connection = this.connection;
                commandAdd.ExecuteNonQuery();
            }catch(Exception e)
            {
                throw e;
            }
        }

        public void NewPlaylist(string name, string theme, int visibility)
        {
            int idNextPlaylist;
            SqlCommand commandGetId = new SqlCommand("SELECT count(*) as count FROM Playlists");

            try
            {
                Open();
            
                commandGetId.Connection = connection;
                idNextPlaylist = Convert.ToInt32(commandGetId.ExecuteScalar()) + 1;

                SqlCommand commandInsert = new SqlCommand("INSERT INTO Playlists VALUES (@idPlaylist, @name, @nbView, @theme, @visibility, @owner)");
                commandInsert.Parameters.AddWithValue("@idPlaylist", idNextPlaylist);
                commandInsert.Parameters.AddWithValue("@name", name);
                commandInsert.Parameters.AddWithValue("@nbView", 0);
                commandInsert.Parameters.AddWithValue("@theme", theme);
                commandInsert.Parameters.AddWithValue("@visibility", visibility);
                commandInsert.Parameters.AddWithValue("@owner", ConnectedUser.user.IdUser);

                commandInsert.Connection = connection;
                commandInsert.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Playlist> GetPlaylists(int idUser)
        {
            SqlCommand commandPlaylists = new SqlCommand("SELECT * FROM Playlists WHERE userOwner = @id");
            commandPlaylists.Parameters.AddWithValue("@id", idUser);

            try
            {
                Open();
                commandPlaylists.Connection = connection;

                SqlDataReader readerPlaylists = commandPlaylists.ExecuteReader();
                List<Playlist> listPlaylist = new List<Playlist>();

                if (readerPlaylists.HasRows)
                {
                    ConnectionManager cm = new ConnectionManager();
                    SqlCommand commandUser = new SqlCommand("SELECT * FROM Users WHERE idUser = @idUser");
                    commandUser.Parameters.AddWithValue("@idUser", idUser);
                    User user = cm.GetUser(commandUser);
                    while (readerPlaylists.Read())
                    {
                        listPlaylist.Add(new Playlist(
                            readerPlaylists.GetInt32(readerPlaylists.GetOrdinal("idPlaylist")),
                            readerPlaylists.GetString(readerPlaylists.GetOrdinal("name")),
                            readerPlaylists.GetString(readerPlaylists.GetOrdinal("playlistTheme")),
                            user,
                            GetFilesInPlaylist(readerPlaylists.GetInt32(readerPlaylists.GetOrdinal("idPlaylist")), user)
                            ));
                    }

                    return listPlaylist;
                }
                else
                {
                    return new List<Playlist>();
                }
            }
            catch (Exception ex)
            {
                    throw ex;
            }
        }

        public Playlist GetPlaylist(int idPlaylist)
        {
            SqlCommand commandPlaylist = new SqlCommand("SELECT * FROM Playlists WHERE idPLaylist = @id");
            commandPlaylist.Parameters.AddWithValue("@id", idPlaylist);
            try
            {
                Open();
                commandPlaylist.Connection = connection;

                SqlDataReader readerPlaylist = commandPlaylist.ExecuteReader();

                if (readerPlaylist.HasRows)
                {
                    readerPlaylist.Read();
                    ConnectionManager cm = new ConnectionManager();
                    SqlCommand commandUser = new SqlCommand("SELECT * FROM Users WHERE idUser = @idUser");
                    commandUser.Parameters.AddWithValue("@idUser", readerPlaylist.GetInt32(readerPlaylist.GetOrdinal("userOwner")));

                    Playlist playlist = new Playlist(
                        readerPlaylist.GetInt32(readerPlaylist.GetOrdinal("idPlaylist")),
                        readerPlaylist.GetString(readerPlaylist.GetOrdinal("name")),
                        readerPlaylist.GetInt32(readerPlaylist.GetOrdinal("visibility")),
                        readerPlaylist.GetString(readerPlaylist.GetOrdinal("playlistTheme")),
                        cm.GetUser(commandUser),
                        readerPlaylist.GetInt32(readerPlaylist.GetOrdinal("nbView")),
                        GetFilesInPlaylist(idPlaylist, cm.GetUser(commandUser))
                    );

                    return playlist;
                }
                else
                {
                    return new Playlist();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BabelFiles> GetFilesInPlaylist(int idPlaylist, User user)
        {
            SqlCommand commandFiles = new SqlCommand("SELECT * FROM files_in_playlist as fip, files as f WHERE fip.idPLaylist_ = @id AND fip.idFile_ = f.idFile");
            commandFiles.Parameters.AddWithValue("@id", idPlaylist);
            try
            {
                Open();
                commandFiles.Connection = connection;

                SqlDataReader readerFiles = commandFiles.ExecuteReader();
                List<BabelFiles> files = new List<BabelFiles>();

                if (readerFiles.HasRows)
                {
                    while (readerFiles.Read())
                    {
                        files.Add(new BabelFiles(
                            readerFiles.GetInt32(readerFiles.GetOrdinal("idFile")),
                            readerFiles.GetString(readerFiles.GetOrdinal("title")),
                            readerFiles.GetString(readerFiles.GetOrdinal("accessPath")),
                            readerFiles.GetString(readerFiles.GetOrdinal("fileFormat")),
                            readerFiles.GetDateTime(readerFiles.GetOrdinal("created")),
                            readerFiles.GetInt32(readerFiles.GetOrdinal("notation")),
                            readerFiles.GetInt32(readerFiles.GetOrdinal("visibility")),
                            readerFiles.GetString(readerFiles.GetOrdinal("genre")),
                            readerFiles.GetString(readerFiles.GetOrdinal("fileLanguage")),
                            readerFiles.GetString(readerFiles.GetOrdinal("theme")),
                            readerFiles.GetInt32(readerFiles.GetOrdinal("nbViews")),
                            readerFiles.GetString(readerFiles.GetOrdinal("picture")),
                            user
                            ));
                    }

                    return files;
                }
                else
                {
                    return new List<BabelFiles>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}