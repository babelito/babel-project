using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHMBABEL.Classes.Enums.EnumFileLanguage
{
    enum FileLanguages { FR, EN, SP, IT, JP };
    public class EnumFileLanguage
    {
        private FileLanguages fileLanguage;

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
    }
}
