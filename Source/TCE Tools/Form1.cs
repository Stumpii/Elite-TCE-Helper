using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Data;
using BrightIdeasSoftware;
using Newtonsoft.Json;
using PropertyChanged;

namespace TCE_Tools
{
    public partial class Form1 : Form
    {
        #region Fields

        public List<public_category> PublicCategory = new List<public_category>();

        public List<public_good> PublicGoods = new List<public_good>();

        public List<public_marketprices> PublicMarketPrices = new List<public_marketprices>();

        public List<public_markets> PublicMarkets = new List<public_markets>();

        public List<public_stars> PublicStars = new List<public_stars>();

        private public_markets SelectedMarket;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet myDataSet = new DataSet();

            string cs = @"URI=file:C:\TCE\DB\TCE_Stars.db";
            var con = new SQLiteConnection(cs);
            con.Open();

            SQLiteDataAdapter myAdapter = new SQLiteDataAdapter("SELECT * FROM public_stars", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_stars");
            con.Close();

            cs = @"URI=file:C:\TCE\DB\TCE_RMarkets.db";
            con = new SQLiteConnection(cs);
            con.Open();

            myAdapter = new SQLiteDataAdapter("SELECT * FROM public_markets", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_markets");
            con.Close();

            cs = @"URI=file:C:\TCE\DB\TCE_Prices.db";
            con = new SQLiteConnection(cs);
            con.Open();

            myAdapter = new SQLiteDataAdapter("SELECT * FROM public_marketprices", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_marketprices");
            con.Close();

            cs = @"URI=file:C:\TCE\DB\Resources.db";
            con = new SQLiteConnection(cs);
            con.Open();

            myAdapter = new SQLiteDataAdapter("SELECT * FROM public_goods", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_goods");
            myAdapter = new SQLiteDataAdapter("SELECT * FROM public_category", con);
            myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            myAdapter.Fill(myDataSet, "public_category");
            con.Close();

            // Setup Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.CreateMap<IDataRecord, public_stars>();
                cfg.CreateMap<IDataRecord, public_markets>();
                cfg.CreateMap<IDataRecord, public_marketprices>();
                cfg.CreateMap<IDataRecord, public_good>();
                cfg.CreateMap<IDataRecord, public_category>();
            });
            var mapper = config.CreateMapper();

            //// =========== TEST CODE TO PRINT COLUMN TYPES ===========
            //var tableNames = new List<string>() { "public_category" };
            //foreach (var tbl in tableNames)
            //{
            //    DataColumnCollection colColl = myDataSet.Tables[tbl].Columns;
            //    Debug.WriteLine(string.Empty);
            //    Debug.WriteLine($"public class {tbl}\r\n{{");
            //    for (int i = 0; i < colColl.Count; i++)
            //    {
            //        Debug.WriteLine($"public {colColl[i].DataType.ToString().Replace("System.", "")}" +
            //            $" {colColl[i]}  {{ get; set; }}");
            //    }
            //    Debug.WriteLine("}");
            //    Debug.WriteLine(string.Empty);
            //}
            //// =========== END TEST CODE TO PRINT COLUMN TYPES ===========

            // Read in public_stars table
            PublicStars = mapper.Map<IDataReader,
                List<public_stars>>(myDataSet.Tables["public_stars"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicStars)
                item.Parent = this;

            // Read in public_markets table
            PublicMarkets = mapper.Map<IDataReader,
                List<public_markets>>(myDataSet.Tables["public_markets"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicMarkets)
                item.Parent = this;

            // Read in public_marketprices table
            PublicMarketPrices = mapper.Map<IDataReader,
                List<public_marketprices>>(myDataSet.Tables["public_marketprices"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicMarketPrices)
                item.Parent = this;

            // Read in public_goods table
            PublicGoods = mapper.Map<IDataReader,
                List<public_good>>(myDataSet.Tables["public_goods"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicGoods)
                item.Parent = this;

            // Read in public_category table
            PublicCategory = mapper.Map<IDataReader,
                List<public_category>>(myDataSet.Tables["public_category"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicCategory)
                item.Parent = this;

            fastDataListView1.DataSource = PublicStars;
            Helper.UpdateObjectListViewColumns(fastDataListView1);

            fastDataListView2.DataSource = PublicMarkets;
            Helper.UpdateObjectListViewColumns(fastDataListView2);

            fastDataListView3.DataSource = PublicMarketPrices;
            Helper.UpdateObjectListViewColumns(fastDataListView3);

            fastDataListView4.DataSource = PublicGoods;
            Helper.UpdateObjectListViewColumns(fastDataListView4);

            dataListView5.DataSource = PublicGoods;
            Helper.UpdateObjectListViewColumns(dataListView5);

            //List<string> myList =  PublicMarkets.Select(x => (x.ToString())).ToList();
            //myList.Sort();
            //comboBox1.DataSource = myList;

            List<public_markets> myList = new List<public_markets>(PublicMarkets);
            myList.Sort((a, b) => a.StarAndMarketName.CompareTo(b.StarAndMarketName));
            comboBox1.DataSource = myList;

            //public_markets[] comboList = new public_markets[PublicMarkets.Count];
            //PublicMarkets.CopyTo(comboList);
            //comboList.ToList();
            //comboBox1.DataSource= comboList;
            SelectedMarket = (public_markets)comboBox1.SelectedItem;
        }

        #endregion Methods

        private void button2_Click(object sender, EventArgs e)
        {
            public_good public_Good = (public_good)dataListView5.SelectedObject;

            if (radioButtonBuy.Checked)
            {
                foreach (var item in SelectedMarket.ClosestMarkets)
                {
                    item.ReferenceMarket = SelectedMarket;
                }

                fastDataListView6.DataSource = public_Good.MarketPrices.Where(x => x.Stock > 0).ToList();
                Helper.UpdateObjectListViewColumns(fastDataListView6);
            }
            else
            {
                foreach (var item in SelectedMarket.ClosestMarkets)
                {
                    item.ReferenceMarket = SelectedMarket;
                }

                fastDataListView6.DataSource = SelectedMarket.ClosestMarkets;
                //fastDataListView6.DataSource = public_Good.MarketPrices;
                Helper.UpdateObjectListViewColumns(fastDataListView6);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMarket = (public_markets)comboBox1.SelectedItem;
        }

        private void button_Click(object sender, EventArgs e)
        {
            EDDB_StarSystem eDDB_StarSystem = new EDDB_StarSystem();
            eDDB_StarSystem.ReadFile();

            eDDBStarSystemListLiew.DataSource = eDDB_StarSystem.starSystems;
            Helper.UpdateObjectListViewColumns(eDDBStarSystemListLiew);

            EDDB_Station eDDB_Station = new EDDB_Station();
            eDDB_Station.ReadFile();

            eDDBStationListView.DataSource = eDDB_Station.stations;
            Helper.UpdateObjectListViewColumns(eDDBStationListView);
        }

        private bool disableStationListView;
        private bool disableStarSystmListView;

        private void eDDBStationListView_SelectionChanged(object sender, EventArgs e)
        {
            if (disableStationListView) return;

            //Disable the updates of the other listview
            disableStarSystmListView = true;

            if (eDDBStationListView.SelectedObjects.Count == 1)
            {
                Station station = (Station)eDDBStationListView.SelectedObject;
                var systemID = station.system_id;

                foreach (var item in eDDBStarSystemListLiew.Objects)
                {
                    StarSystem starSystem = (StarSystem)item;
                    if (starSystem.id == systemID)
                    {
                        eDDBStarSystemListLiew.SelectedObject = item;
                        eDDBStarSystemListLiew.EnsureVisible(eDDBStarSystemListLiew.GetItemCount() - 1);
                        eDDBStarSystemListLiew.EnsureVisible(eDDBStarSystemListLiew.SelectedIndex);
                        break;
                    }
                }
            }

            disableStarSystmListView = false;
        }

        private void eDDBStarSystemListLiew_SelectionChanged(object sender, EventArgs e)
        {
            if (disableStarSystmListView) return;

            //Disable the updates of the other listview
            disableStationListView = true;

            if (eDDBStarSystemListLiew.SelectedObjects.Count == 1)
            {
                StarSystem starSystem = (StarSystem)eDDBStarSystemListLiew.SelectedObject;

                List<Station> stations = new List<Station>();
                foreach (var item in eDDBStationListView.Objects)
                {
                    Station station = (Station)item;
                    if (station.system_id == starSystem.id)
                    {
                        stations.Add(station);
                    }
                }
                eDDBStationListView.SelectedObjects = stations;
                eDDBStationListView.EnsureVisible(eDDBStationListView.GetItemCount() - 1);
                eDDBStationListView.EnsureVisible(eDDBStationListView.SelectedIndices[0]);
            }

            disableStationListView = false;
        }
    }
}