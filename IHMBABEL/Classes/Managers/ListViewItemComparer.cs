using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHMBABEL.Classes.Managers
{
    class ListViewItemComparer : IComparer
    {
        private int col;
        private bool isInt = false;
        private int sort;

        public ListViewItemComparer()
        {
            col = 0;
        }

        public ListViewItemComparer(int L_col)
        {
            col = L_col;
            sort = 1;
        }

        public ListViewItemComparer(int L_col, int L_sort, bool L_isInt)
        {
            col = L_col;
            sort = L_sort;
            isInt = L_isInt;
        }

        public int Compare(object x, object y)
        {
            if (isInt)
            {
                int col1;
                int col2;

                if (((ListViewItem)x).SubItems[col].Text == "NN")
                {
                    col1 = -1;
                }
                else
                {
                    col1 = Int32.Parse(((ListViewItem)x).SubItems[col].Text);
                }

                if (((ListViewItem)y).SubItems[col].Text == "NN")
                {
                    col2 = -1;
                }
                else
                {
                    col2 = Int32.Parse(((ListViewItem)y).SubItems[col].Text);
                }

                return sort * col1.CompareTo(col2);
            }
            else
            {
                return sort * String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }

        }
    }
}
