using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Enums.EnumFileLanguage;
using IHMBABEL.Classes.Users;

namespace IHMBABEL
{
    public class BabelFiles
    {
        #region propriete
        private int idFile;
        private string title;
        private string directory;
        private DateTime? dateCreated;
        private float notation;
        private int visibility;
        private string genre;
        private FileLanguages fileLanguage;
        private string theme;
        private int nbViews;
        private string picture;
        private User fileOwner;
        private FileFormats fileFormats;
        #endregion

        #region accesseurs
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

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Directory
        {
            get
            {
                return directory;
            }

            set
            {
                directory = value;
            }
        }

        public DateTime? DateCreated
        {
            get
            {
                return dateCreated;
            }

            set
            {
                dateCreated = value;
            }
        }

        public float Notation
        {
            get
            {
                return notation;
            }

            set
            {
                notation = value;
            }
        }

        public int Visibility
        {
            get
            {
                return visibility;
            }

            set
            {
                visibility = value;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }

            set
            {
                genre = value;
            }
        }

        internal FileLanguages FileLanguage
        {
            get
            {
                return fileLanguage;
            }

            set
            {
                fileLanguage = value;
            }
        }

        internal virtual FileFormats FileFormat
        {
            get
            {
                return fileFormats;
            }

            set
            {
                fileFormats = value;
            }
        }

        public string Theme
        {
            get
            {
                return theme;
            }

            set
            {
                theme = value;
            }
        }

        public int NbViews
        {
            get
            {
                return nbViews;
            }

            set
            {
                nbViews = value;
            }
        }

        public string Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
            }
        }

        public User FileOwner
        {
            get
            {
                return fileOwner;
            }

            set
            {
                fileOwner = value;
            }
        }

        public BabelFiles() { }

        public BabelFiles(int idFile, string title, string directory, string fileFormat, DateTime? dateCreated, float notation, int visibility, string genre, string fileLanguage, string theme, int nbViews, string picture, User fileOwner)
        {

            FileFormats outResultFF;
            FileLanguages outResultFL;

            IdFile = idFile;
            Title = title;
            Directory = directory;

            if(Enum.TryParse(fileFormat, out outResultFF))
                FileFormat = outResultFF;

            DateCreated = dateCreated;
            Notation = notation;
            Visibility = visibility;
            Genre = genre;

            if (Enum.TryParse(fileLanguage, out outResultFL))
                FileLanguage = outResultFL;

            Theme = theme;
            NbViews = nbViews;
            Picture = picture;
            FileOwner = fileOwner;
    }

        #endregion

        #region methodes
        #endregion
    }
}
