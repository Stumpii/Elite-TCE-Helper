using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCE_Tools
{
    public static class Helper
    {
        public static void UpdateObjectListViewColumns(BrightIdeasSoftware.ObjectListView olv)
        {
            olv.BeginUpdate();

            //Fix columns
            for (int i = 0; i < olv.Columns.Count; i++)
            {
                OLVColumn col = olv.GetColumn(i);

                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                int colWidthAfterAutoResizeByHeader = col.Width;

                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int colWidthAfterAutoResizeByContent = col.Width;

                if (!col.IsHeaderVertical && (colWidthAfterAutoResizeByHeader > colWidthAfterAutoResizeByContent))
                    col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            olv.EndUpdate();
        }
    }
}