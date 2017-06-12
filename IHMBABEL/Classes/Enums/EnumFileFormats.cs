using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Enums.EnumFileFormats
{
    public enum FileFormats { png, jpg, mp4, avi, txt, pdf, mp3, unknown };
    public class EnumFileFormats
    {
        private FileFormats fileFormats;

        internal FileFormats FileFormats
        {
            get
            {
                return fileFormats;
            }

            private set
            {
                fileFormats = value;
            }
        }
    }
}
