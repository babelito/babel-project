using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHMBABEL.Classes.Enums.EnumFileFormats;
using IHMBABEL.Classes.Enums.EnumFileLanguage;
using IHMBABEL.Classes.Users;

namespace IHMBABEL.Classes.Files.FileTypes
{
    class Picture : BabelFiles
    {
        #region proprietes

        private int idPhoto;
        private int width;
        private int height;

        #endregion
        #region accesseurs
        public int IdPhoto
        {
            get
            {
                return idPhoto;
            }

            set
            {
                idPhoto = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public Picture(int idFile, string title, string directory, string fileFormat, DateTime dateCreated, float notation, int visibility, string genre, string fileLanguage, string theme, int nbViews, string picture, User fileOwner,
            int width, int height)
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

            Width = width;
            Height = height;
        }
        #endregion

        #region methodes

        #endregion
    }
}
