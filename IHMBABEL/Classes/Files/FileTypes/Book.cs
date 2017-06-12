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
    public class Book : BabelFiles
    {
        #region proprietes
        private string editor;
        private DateTime releaseDate;

        public string Editor
        {
            get
            {
                return editor;
            }

            set
            {
                editor = value;
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return releaseDate;
            }

            set
            {
                releaseDate = value;
            }
        }
        #endregion

        #region accesseurs
        public Book(int idFile, string title, string directory, string fileFormat, DateTime dateCreated, float notation, int visibility, string genre, string fileLanguage, string theme, int nbViews, string picture, User fileOwner,
            string editor, DateTime releaseDate)
            : base(idFile, title, directory, fileFormat, dateCreated, notation, visibility, genre, fileLanguage, theme, nbViews, picture, fileOwner)
        {
            FileFormats outResultFF;
            FileLanguages outResultFL;

            IdFile = idFile;
            Title = title;
            Directory = directory;
            if (Enum.TryParse(fileFormat, out outResultFF))
                FileFormat = outResultFF;
            DateCreated = dateCreated;
            Notation = notation;
            Visibility = visibility;
            Genre = genre;
            if (Enum.TryParse(fileLanguage, out outResultFL))
                FileLanguage = outResultFL;
            Theme = theme;
            NbViews = nbViews;
            Picture = Picture;
            FileOwner = fileOwner;

            Editor = editor;
            ReleaseDate = releaseDate;
        }
        #endregion

        #region methodes

        #endregion
    }
}
