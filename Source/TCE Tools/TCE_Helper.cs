using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
    public class TCE_Helper
    {
        private DataSet myDataSet = new DataSet();

        public bool ReadStars()
        {
            string cs = @"URI=file:C:\TCE\DB\TCE_Stars.db";
            var con = new SQLiteConnection(cs);
            con.Open();

            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter("SELECT * FROM public_stars", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_stars");
            con.Close();

            return true;
        }
    }
}