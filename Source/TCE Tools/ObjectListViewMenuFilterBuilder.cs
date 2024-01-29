using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using BrightIdeasSoftware;

    /// <summary>
    /// Filter builder for ObjectListView
    /// v1.0.0 S.Towner 26Jun17
    /// </summary>
    public class ObjectListViewMenuFilterBuilder : FilterMenuBuilder
    {
        #region Fields

        private bool alreadyInHandleItemChecked = false;
        private ToolStripCheckedListBox checkedList = new ToolStripCheckedListBox();
        private List<ICluster> clusters;
        private OLVColumn column;
        private SimpleSearchBox filter = new SimpleSearchBox();
        private ToolStripControlHost filterHost;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Creates a filter menu popup
        /// </summary>
        /// <param name="column">The column to which the filter applies.</param>
        /// <param name="clusters"></param>
        /// <returns></returns>
        protected override ToolStripMenuItem CreateFilteringMenuItem(OLVColumn column, List<ICluster> clusters)
        {
            this.clusters = clusters;
            this.column = column;

            //ToolStripCheckedListBox checkedList = new ToolStripCheckedListBox();
            checkedList.Items.Clear();
            checkedList.Tag = column;

            foreach (ICluster cluster in clusters)
                checkedList.AddItem(cluster, column.ValuesChosenForFiltering.Contains(cluster.ClusterKey) || column.ValuesChosenForFiltering.Count == 0);

            if (!String.IsNullOrEmpty(SELECT_ALL_LABEL))
            {
                int checkedCount = checkedList.CheckedItems.Count;
                if (checkedCount == 0)
                {
                    checkedList.Items.Insert(0, SELECT_ALL_LABEL);
                    checkedList.SetItemState(0, CheckState.Unchecked);
                }
                else
                {
                    checkedList.Items.Insert(0, SELECT_ALL_LABEL);
                    checkedList.SetItemState(0, checkedCount == clusters.Count ? CheckState.Checked : CheckState.Indeterminate);
                }
            }

            checkedList.ItemCheck += new ItemCheckEventHandler(HandleItemCheckedWrapped);

            ToolStripMenuItem clearAll = new ToolStripMenuItem(CLEAR_ALL_FILTERS_LABEL, ClearFilteringImage,
            delegate (object sender, EventArgs args)
            {
                this.ClearAllFilters(column);
            });

            #region Text Filters Sub Menu

            ToolStripMenuItem textFiltersBeginsWith = new ToolStripMenuItem("Begins W&ith...", null,
            delegate (object sender, EventArgs args)
            {
                string value = "";
                DialogResult res = InputForms.InputBox("Custom Filter", "Show rows that begin with...", ref value);
                if (res == DialogResult.OK)
                {
                    checkedList.UncheckAll();
                    for (int i = 0; i < checkedList.Items.Count; i++)
                    {
                        if (checkedList.Items[i] is ICluster)
                        {
                            if (((ICluster)checkedList.Items[i]).ClusterKey.ToString().StartsWith(value, StringComparison.OrdinalIgnoreCase))
                            {
                                checkedList.SetItemState(i, CheckState.Checked);
                            }
                        }
                    }

                    this.EnactFilter(checkedList, column);
                }
            });

            ToolStripMenuItem textFiltersEndsWith = new ToolStripMenuItem("Ends Wi&th...", null,
            delegate (object sender, EventArgs args)
            {
                string value = "";
                DialogResult res = InputForms.InputBox("Custom Filter", "Show rows that end with...", ref value);
                if (res == DialogResult.OK)
                {
                    checkedList.UncheckAll();
                    for (int i = 0; i < checkedList.Items.Count; i++)
                    {
                        if (checkedList.Items[i] is ICluster)
                        {
                            if (((ICluster)checkedList.Items[i]).ClusterKey.ToString().EndsWith(value, StringComparison.OrdinalIgnoreCase))
                            {
                                checkedList.SetItemState(i, CheckState.Checked);
                            }
                        }
                    }

                    this.EnactFilter(checkedList, column);
                }
            });

            ToolStripMenuItem textFiltersContains = new ToolStripMenuItem("Cont&ains...", null,
            delegate (object sender, EventArgs args)
            {
                string value = "";
                DialogResult res = InputForms.InputBox("Custom Filter", "Show rows that contain...", ref value);
                if (res == DialogResult.OK)
                {
                    checkedList.UncheckAll();
                    for (int i = 0; i < checkedList.Items.Count; i++)
                    {
                        if (checkedList.Items[i] is ICluster)
                        {
                            if (((ICluster)checkedList.Items[i]).ClusterKey.ToString().ToUpper().Contains(value.ToUpper()))
                            {
                                checkedList.SetItemState(i, CheckState.Checked);
                            }
                        }
                    }

                    this.EnactFilter(checkedList, column);
                }
            });

            ToolStripMenuItem textFiltersDoesNotContain = new ToolStripMenuItem("&Does Not Contain...", null,
            delegate (object sender, EventArgs args)
            {
                string value = "";
                DialogResult res = InputForms.InputBox("Custom Filter", "Show rows that do not contain...", ref value);
                if (res == DialogResult.OK)
                {
                    checkedList.UncheckAll();
                    for (int i = 0; i < checkedList.Items.Count; i++)
                    {
                        if (checkedList.Items[i] is ICluster)
                        {
                            if (!((ICluster)checkedList.Items[i]).ClusterKey.ToString().ToUpper().Contains(value.ToUpper()))
                            {
                                checkedList.SetItemState(i, CheckState.Checked);
                            }
                        }
                    }

                    this.EnactFilter(checkedList, column);
                }
            });

            ToolStripMenuItem textFiltersFromClipboard = new ToolStripMenuItem("Select from clipboard", null,
            delegate (object sender, EventArgs args)
            {
                checkedList.UncheckAll();

                if (!Clipboard.ContainsText())
                    return;

                string[] lines = Clipboard.GetText().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int i = 0; i < checkedList.Items.Count; i++)
                {
                    if (checkedList.Items[i] is ICluster)
                    {
                        foreach (var clipItem in lines)
                        {
                            if (((ICluster)checkedList.Items[i]).ClusterKey.ToString().ToUpper().Equals(clipItem.ToUpper()))
                            {
                                checkedList.SetItemState(i, CheckState.Checked);
                            }
                        }
                    }
                }

                this.EnactFilter(checkedList, column);
            });

            ToolStripMenuItem textFilters = new ToolStripMenuItem("Text &Filters...", null,
            new ToolStripItem[] { textFiltersBeginsWith, textFiltersEndsWith, new ToolStripSeparator(),
  textFiltersContains, textFiltersDoesNotContain, new ToolStripSeparator() , textFiltersFromClipboard });

            #endregion Text Filters Sub Menu

            ToolStripMenuItem apply = new ToolStripMenuItem(APPLY_LABEL, FilteringImage,
            delegate (object sender, EventArgs args)
            {
                this.EnactFilter(checkedList, column);
            });

            filterHost = new ToolStripControlHost(filter);
            filter.SearchChanged += filter_SearchChanged;

            ToolStripMenuItem subMenu = new ToolStripMenuItem(FILTERING_LABEL, null,
            new ToolStripItem[] { clearAll, new ToolStripSeparator(), textFilters, filterHost, checkedList, apply });

            //To resize the filter when the dropdown changes size.
            subMenu.Paint += subMenu_Paint;

            return subMenu;
        }

        private void filter_SearchChanged(string SearchString)
        {
            // Clear all items from the checked list
            checkedList.Items.Clear();

            // Only add those items to the checked list that are not filtered out
            foreach (ICluster cluster in clusters)
            {
                if ((cluster.ClusterKey ?? "").ToString().ToUpper().Contains(filter.Text.ToUpper()))
                {
                    checkedList.AddItem(cluster, column.ValuesChosenForFiltering.Contains(cluster.ClusterKey) || column.ValuesChosenForFiltering.Count == 0);
                }
            }

            // Add 'Select All' item to the top
            if (!String.IsNullOrEmpty(SELECT_ALL_LABEL))
            {
                int checkedCount = checkedList.CheckedItems.Count;
                if (checkedCount == 0)
                {
                    checkedList.Items.Insert(0, SELECT_ALL_LABEL);
                    checkedList.SetItemState(0, CheckState.Unchecked);
                }
                else
                {
                    checkedList.Items.Insert(0, SELECT_ALL_LABEL);
                    checkedList.SetItemState(0, checkedCount == clusters.Count ? CheckState.Checked : CheckState.Indeterminate);
                }
            }
        }

        /// <summary>
        /// Wrap a protected section around the real HandleItemChecked method, so that if
        /// that method tries to change a "checkedness" of an item, we don't get a recursive
        /// stack error. Effectively, this ensure that HandleItemChecked is only called
        /// in response to a user action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleItemCheckedWrapped(object sender, ItemCheckEventArgs e)
        {
            if (alreadyInHandleItemChecked)
                return;

            try
            {
                alreadyInHandleItemChecked = true;
                this.HandleItemChecked(sender, e);
            }
            finally
            {
                alreadyInHandleItemChecked = false;
            }
        }

        private void subMenu_Paint(object sender, PaintEventArgs e)
        {
            //Lazy - make filter width the same as the checklist.
            filter.Width = checkedList.Width;
        }

        #endregion Methods
    }
}