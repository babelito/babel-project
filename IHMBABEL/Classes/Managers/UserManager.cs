using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Managers
{
    public class UserManager
    {

        private string connectionString;
        private SqlConnection connection;
        private SqlCommand command1;
        private SqlCommand command2;

        public UserManager()
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

        public string SetUser(string pseudo, string password, string firstName, string lastName, string mail, string pic)
        {
            string pic2 = null;
            command1 = new SqlCommand("SELECT max(idUser) FROM Users");

            try
            {
                connection.Open();
                command1.Connection = this.connection;
                int idUser = Convert.ToInt32(command1.ExecuteScalar()) + 1;

                if (testPseudo(pseudo))
                {
                    if (testMail(mail))
                    {
                        string[] words = pic.Split('\\');
                        string fileName = words[words.Length - 1];

                        words = fileName.Split('.');
                        string fileType = words[words.Length - 1];

                        if (pic == "")
                        {
                            pic2 = "http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Avatar/joshua.png";
                        } else
                        {
                            pic2 = "http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Avatar/" + pseudo + "/" + "avatar.png";
                        }

                        String query = "INSERT INTO Users VALUES (@idUser, @pseudo, @lastName, @firstName, @mail, @pic2, @date, @password)";

                        command2 = new SqlCommand(query, connection);
                        command2.Parameters.AddWithValue("@idUser", idUser);
                        command2.Parameters.AddWithValue("@pseudo", pseudo);
                        command2.Parameters.AddWithValue("@password", password);
                        command2.Parameters.AddWithValue("@lastName", lastName);
                        command2.Parameters.AddWithValue("@firstName", firstName);
                        command2.Parameters.AddWithValue("@mail", mail);
                        command2.Parameters.AddWithValue("@pic2", pic2);
                        command2.Parameters.AddWithValue("@date", DateTime.Now);

                        command2.ExecuteNonQuery();

                        UploadDownload ud = new UploadDownload();
                        ud.CreateUserDirectory(pseudo);

                        if (pic != "")
                        {
                            ud.UploadAvatar(pseudo, pic);
                        }
                        
                        return "Votre compte a bien été crée";

                    } else
                    {
                        return "Ce mail existe déjà";
                    }
                } else
                {
                    return "Ce pseudo existe déjà";
                }
            
            }
            catch (Exception ex)
            {
                return "Il y a eu une erreur, veuillez réessayer ultérieurement";
                throw ex;
            }
            finally
            {
                if (command1 != null)
                {
                    if (command1.Connection != null && command1.Connection.State == ConnectionState.Open)
                    {
                        command1.Connection.Close();
                    }
                }
                if (command2 != null)
                {
                    if (command2.Connection != null && command2.Connection.State == ConnectionState.Open)
                    {
                        command2.Connection.Close();
                    }
                }
                connection.Close();
            }
        }

        private List<string> getPseudo()
        {
            SqlCommand command = new SqlCommand("SELECT pseudo FROM Users");
            try
            {
                command.Connection = this.connection;

                SqlDataReader reader = command.ExecuteReader();

                List<string> listPseudo = new List<string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listPseudo.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("pseudo")))));
                    }
                }
                return listPseudo;
            } catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool testPseudo(string pseudo)
        {
            foreach (var a in getPseudo())
            {
                if (a == pseudo)
                {
                    return false;
                }
            }
            return true;
        }

        private List<string> getMail()
        {
            SqlCommand command = new SqlCommand("SELECT mail FROM Users");
            try
            {
                command.Connection = this.connection;

                SqlDataReader reader = command.ExecuteReader();

                List<string> listMail = new List<string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listMail.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.GetString(reader.GetOrdinal("mail")))));
                    }
                }
                return listMail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool testMail(string mail)
        {
            foreach (var a in getMail())
            {
                if (a == mail)
                {
                    return false;
                }
            }
            return true;
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
    }
}
