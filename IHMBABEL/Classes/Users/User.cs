using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Users
{
    public class User
    {

        #region proprietes
        private int idUser;
        private string pseudo;
        private string password;
        private string firstName;
        private string lastName;
        private string mail;
        private string pic;
        private DateTime accountCreated;
        #endregion 

        #region accesseurs
        public int IdUser
        {
            get
            {
                return idUser;
            }

            set
            {
                idUser = value;
            }
        }

        public string Pseudo
        {
            get
            {
                return pseudo;
            }

            set
            {
                pseudo = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }

            set
            {
                mail = value;
            }
        }

        public string Pic
        {
            get
            {
                return pic;
            }

            set
            {
                pic = value;
            }
        }

        public DateTime AccountCreated
        {
            get
            {
                return accountCreated;
            }

            set
            {
                accountCreated = value;
            }
        }

        public User()
        {

        }

        public User(string pseudo, string password, string firstName, string lastName, string mail, string pic)
        {
            this.IdUser = idUser;
            this.Pseudo = pseudo;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mail = mail;
            if (pic == "")
            {
                this.Pic = "http://perso.montpellier.epsi.fr/~luc.rodiere/Babel/Avatar/joshua.png";
            }
            else
            {
                this.Pic = pic;
            }
            this.AccountCreated = DateTime.Now;
        }

        public User(int idUser, string pseudo, string password, string firstName, string lastName, string mail, string pic)
        {
            this.IdUser = idUser;
            this.Pseudo = pseudo;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mail = mail;
            this.Pic = pic;
        }

        public User(int idUser, string pseudo, string password, string firstName, string lastName, string mail, string pic, DateTime dateCreated)
        {
            this.IdUser = idUser;
            this.Pseudo = pseudo;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mail = mail;
            this.Pic = pic;
            this.AccountCreated = dateCreated;
        }

        #endregion

        #region methodes

        #endregion
    }
}
