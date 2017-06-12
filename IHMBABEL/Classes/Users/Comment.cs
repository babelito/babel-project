using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Users
{
    class Comment
    {
        #region proprietes

        private int idComment;
        private string content;
        private int idUserPoster;
        private int idFile;

        #endregion

        #region accesseurs

        public int IdComment
        {
            get
            {
                return idComment;
            }

            set
            {
                idComment = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public int IdUserPoster
        {
            get
            {
                return idUserPoster;
            }

            set
            {
                idUserPoster = value;
            }
        }

        public int IdFile
        {
            get
            {
                return idFile;
            }

            set
            {
                idFile = value;
            }
        }

        public Comment(int idComment, string content, int idUser, int idFile)
        {
            IdComment = idComment;
            Content = content;
            IdUserPoster = idUser;
            IdFile = idFile;
        }

        #endregion

        #region methodes
        #endregion
    }
}