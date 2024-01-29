using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace TCE_Tools
{
    internal class MiscClasses
    {
    }

    public class TagnameFilter : BrightIdeasSoftware.IModelFilter
    {
        #region Constructors

        public TagnameFilter(Form1 HostForm)
        {
            this.HostForm = HostForm;
        }

        #endregion Constructors

        #region Properties

        public Form1 HostForm
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        bool IModelFilter.Filter(object modelObject)
        {
            //System.Diagnostics.Debug.WriteLine("Filtering");

            bool tagnameMatch = false;
            bool descriptionMatch = false;
            bool aliasMatch = false;
            StarSystem item = (StarSystem)modelObject;

            #region Tagname

            if (HostForm.SystemNameFilter == "")
                tagnameMatch = true;
            else if (HostForm.SystemNameFilter.IndexOfAny(new char[] { '?', '*', '#', '[' }) != -1)
            {
                if (LikeOperator.LikeString(item.name, HostForm.SystemNameFilter, Microsoft.VisualBasic.CompareMethod.Text))
                {
                    tagnameMatch = true;
                }
            }
            else
            {
                if (LikeOperator.LikeString(item.name, "*" + HostForm.SystemNameFilter + "*", Microsoft.VisualBasic.CompareMethod.Text))
                {
                    tagnameMatch = true;
                }
            }

            #endregion Tagname

            #region Description

            //if (HostForm.DescriptionSearchBox.Text == "")
            //    descriptionMatch = true;
            //else if (HostForm.DescriptionSearchBox.Text.IndexOfAny(new char[] { '?', '*', '#', '[' }) != -1)
            //{
            //    if (LikeOperator.LikeString(item.Description, HostForm.DescriptionSearchBox.Text, Microsoft.VisualBasic.CompareMethod.Text))
            //    {
            //        descriptionMatch = true;
            //    }
            //}
            //else
            //{
            //    if (LikeOperator.LikeString(item.Description, "*" + HostForm.DescriptionSearchBox.Text + "*", Microsoft.VisualBasic.CompareMethod.Text))
            //    {
            //        descriptionMatch = true;
            //    }
            //}

            #endregion Description

            #region Alias

            //if (HostForm.AliasSearchBox.Text == "")
            //    aliasMatch = true;
            //else if (HostForm.AliasSearchBox.Text.IndexOfAny(new char[] { '?', '*', '#', '[' }) != -1)
            //{
            //    if (LikeOperator.LikeString(item.AliasNum.ToString(), HostForm.AliasSearchBox.Text, Microsoft.VisualBasic.CompareMethod.Text))
            //    {
            //        aliasMatch = true;
            //    }
            //}
            //else
            //{
            //    if (LikeOperator.LikeString(item.AliasNum.ToString(), "*" + HostForm.AliasSearchBox.Text + "*", Microsoft.VisualBasic.CompareMethod.Text))
            //    {
            //        aliasMatch = true;
            //    }
            //}

            #endregion Alias

            return tagnameMatch;// && descriptionMatch && aliasMatch;
        }

        #endregion Methods
    }
}