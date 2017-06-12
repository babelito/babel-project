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
    class SearchManager
    {
        private string connectionString;
        private SqlConnection connection;

        public SearchManager()
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

        public List<BabelFiles> resultSearchFile(string search)
        {
            if (search != "")
            {
                search = "%" + search + "%";
                SqlCommand commandFile = new SqlCommand("SELECT * FROM Files WHERE title LIKE @search");
                commandFile.Parameters.AddWithValue("@search", search);
                try
                {
                    Open();
                    commandFile.Connection = connection;

                    SqlDataReader readerFile = commandFile.ExecuteReader();

                    List<BabelFiles> listResult = new List<BabelFiles>();


                    if (readerFile.HasRows)
                    {
                        while (readerFile.Read())
                        {
                            ConnectionManager cm = new ConnectionManager();
                            SqlCommand commandUser = new SqlCommand("SELECT * FROM Users WHERE idUser = @idUser");
                            commandUser.Parameters.AddWithValue("@idUser", readerFile.GetInt32(readerFile.GetOrdinal("fileOwner")));
                            listResult.Add(new BabelFiles(
                                readerFile.GetInt32(readerFile.GetOrdinal("idFile")),
                                readerFile.GetString(readerFile.GetOrdinal("title")),
                                readerFile.GetString(readerFile.GetOrdinal("accessPath")),
                                readerFile.GetString(readerFile.GetOrdinal("fileFormat")),
                                readerFile.GetDateTime(readerFile.GetOrdinal("created")),
                                readerFile.GetInt32(readerFile.GetOrdinal("notation")),
                                readerFile.GetInt32(readerFile.GetOrdinal("visibility")),
                                readerFile.GetString(readerFile.GetOrdinal("genre")),
                                readerFile.GetString(readerFile.GetOrdinal("fileLanguage")),
                                readerFile.GetString(readerFile.GetOrdinal("theme")),
                                readerFile.GetInt32(readerFile.GetOrdinal("nbViews")),
                                readerFile.GetString(readerFile.GetOrdinal("picture")),
                                cm.GetUser(commandUser)
                            ));
                        }
                    }

                    return listResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new List<BabelFiles>();
            }
        }

        public List<string> resultSearchUser(string search)
        {
            if (search != "")
            {
                search = "%" + search + "%";
                SqlCommand commandPseudo = new SqlCommand("SELECT pseudo FROM Users WHERE pseudo LIKE @search");
                commandPseudo.Parameters.AddWithValue("@search", search);
                try
                {
                    Open();
                    commandPseudo.Connection = connection;

                    SqlDataReader readerPseudo = commandPseudo.ExecuteReader();

                    List<string> listResult = new List<string>();
                    if (readerPseudo.HasRows)
                    {
                        while (readerPseudo.Read())
                        {
                            listResult.Add(readerPseudo.GetString(readerPseudo.GetOrdinal("pseudo")));
                        }
                    }

                    return listResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new List<string>();
            }
        }

        public List<string> resultSearchFileAndUser(string search)
        {
            if (search != "")
            {
                search = "%" + search + "%";
                SqlCommand commandPseudo = new SqlCommand("SELECT pseudo FROM Users WHERE pseudo LIKE @search");
                commandPseudo.Parameters.AddWithValue("@search", search);
                SqlCommand commandTitle = new SqlCommand("SELECT title FROM Files WHERE title LIKE @search");
                commandTitle.Parameters.AddWithValue("@search", search);
                try
                {
                    Open();
                    commandPseudo.Connection = connection;
                    commandTitle.Connection = connection;

                    SqlDataReader readerPseudo = commandPseudo.ExecuteReader();
                    SqlDataReader readerTitle = commandTitle.ExecuteReader();

                    List<string> listResult = new List<string>();
                    if (readerPseudo.HasRows)
                    {
                        while (readerPseudo.Read())
                        {
                            listResult.Add(readerPseudo.GetString(readerPseudo.GetOrdinal("pseudo")));
                        }
                    }

                    if (readerTitle.HasRows)
                    {
                        while (readerTitle.Read())
                        {
                            listResult.Add(readerTitle.GetString(readerTitle.GetOrdinal("title")));
                        }
                    }

                    return listResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new List<string>();
            }
        }
    }
}