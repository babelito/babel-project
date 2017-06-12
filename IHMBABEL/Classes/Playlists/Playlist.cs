using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHMBABEL.Classes.Managers;

namespace IHMBABEL.Classes.Users
{
    class Playlist
    {
        #region propriete
        private int idPlaylist;
        private string name;
        private int visibility;
        private int nbViews;
        private string theme;
        private User owner;
        private List<BabelFiles> list = new List<BabelFiles>();

        #endregion

        #region accesseur
        public int IdPlaylist
        {
            get
            {
                return idPlaylist;
            }
            set
            {
                idPlaylist = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
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

        public User Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public List<BabelFiles> List
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }
        #endregion

        #region constructeur
        public Playlist()
        {

        }

        public Playlist(int L_idPlaylist, string L_name, string L_theme, User L_owner, List<BabelFiles> L_list)
        {
            IdPlaylist = L_idPlaylist;
            Name = L_name;
            Theme = L_theme;
            Owner = L_owner;
            List = L_list;
        }

        public Playlist(int L_idPlaylist)
        {
            PlaylistManager pm = new PlaylistManager();
            Playlist p = pm.GetPlaylist(L_idPlaylist);
            IdPlaylist = p.idPlaylist;
            Name = p.name;
            Visibility = p.visibility;
            NbViews = p.nbViews;
            Theme = p.theme;
            Owner = p.owner;
            List = p.list;
        }

        public Playlist(int L_idPlaylist, string L_name, int L_visibility, string L_theme, User L_owner, int L_nbViews, List<BabelFiles> L_list)
        {
            IdPlaylist = L_idPlaylist;
            Name = L_name;
            Visibility = L_visibility;
            NbViews = L_nbViews;
            Theme = L_theme;
            Owner = L_owner;
            List = L_list;
        }
        #endregion
    }
}
