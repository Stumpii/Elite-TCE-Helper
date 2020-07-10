using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Data;
using BrightIdeasSoftware;
using PropertyChanged;

namespace TCE_Tools
{
    public partial class Form1 : Form
    {
        #region Fields

        public List<public_category> PublicCategory = new List<public_category>();

        public List<public_goods> PublicGoods = new List<public_goods>();

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
                cfg.CreateMap<IDataRecord, public_goods>();
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
                List<public_goods>>(myDataSet.Tables["public_goods"].CreateDataReader());
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
            UpdateObjectListViewColumns(fastDataListView1);

            fastDataListView2.DataSource = PublicMarkets;
            UpdateObjectListViewColumns(fastDataListView2);

            fastDataListView3.DataSource = PublicMarketPrices;
            UpdateObjectListViewColumns(fastDataListView3);

            fastDataListView4.DataSource = PublicGoods;
            UpdateObjectListViewColumns(fastDataListView4);

            fastDataListView5.DataSource = PublicGoods;
            UpdateObjectListViewColumns(fastDataListView5);

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

        private void UpdateObjectListViewColumns(BrightIdeasSoftware.ObjectListView olv)
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

        #endregion Methods
    }

    public class public_category
    {
        #region Properties

        public String Category { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public Int64 ID { get; set; }

        internal Form1 Parent { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return Category;
        }

        #endregion Methods
    }

    public class public_goods
    {
        #region Fields

        private public_category _category;

        #endregion Fields

        #region Properties

        public Int64 AvgPrice { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public Int64 Category { get; set; }

        public string CategoryName
        {
            get
            {
                return CategoryObj?.Category ?? "";
            }
        }

        [BrightIdeasSoftware.OLVIgnore]
        public public_category CategoryObj
        {
            get
            {
                if (_category == null)
                    _category = Parent.PublicCategory.FirstOrDefault(x => (Category == x.ID));

                return _category;
            }
        }

        [BrightIdeasSoftware.OLVIgnore]
        public Int64 ED_ID { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public Int64 EDDB_ID { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public Int64 ID { get; set; }

        public String Tradegood { get; set; }
        internal Form1 Parent { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return Tradegood;
        }

        #endregion Methods
    }

    public class public_marketprices
    {
        #region Fields

        private public_goods _good;
        private public_markets _market;

        #endregion Fields

        #region Properties

        public Int64 Buy { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public public_goods Good
        {
            get
            {
                if (_good == null)
                    _good = Parent.PublicGoods.FirstOrDefault(x => (GoodID == x.ID));

                return _good;
            }
        }

        public Int64 GoodID { get; set; }

        public string GoodName
        {
            get
            {
                return Good?.Tradegood ?? "";
            }
        }

        public Int64 ID { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public public_markets Market
        {
            get
            {
                if (_market == null)
                    _market = Parent.PublicMarkets.FirstOrDefault(x => (MarketID == x.ID));

                return _market;
            }
        }

        public Int64 MarketID { get; set; }

        public string MarketName
        {
            get
            {
                return Market?.MarketName ?? "";
            }
        }

        public Int64 Sell { get; set; }
        public Int64 Stock { get; set; }
        internal Form1 Parent { get; set; }

        #endregion Properties
    }

    public class public_markets
    {
        #region Fields

        private List<public_marketprices> _marketprices;
        private public_stars _star;

        #endregion Fields

        #region Properties

        public Int64 AllegianceID { get; set; }
        public Int64 Blackmarket { get; set; }
        public String BodyName { get; set; }
        public Int64 Broker { get; set; }
        public Int64 DistanceStar { get; set; }
        public Int64 EDDB_ID { get; set; }
        public String Faction { get; set; }
        public String FactionState { get; set; }
        public String Government { get; set; }
        public Decimal Hangar { get; set; }
        public Int64 ID { get; set; }
        public String IllegalGoods { get; set; }
        public Int64 LastDate { get; set; }
        public String LastTime { get; set; }
        public Int64 LastUpdate { get; set; }
        public String MarketName { get; set; }
        public Int64 MarketType { get; set; }
        public Int64 MatBroker { get; set; }
        public String Notes { get; set; }
        public Int64 Outfitting { get; set; }
        public Decimal PosX { get; set; }
        public Decimal PosY { get; set; }
        public Decimal PosZ { get; set; }
        public Int64 PriEconomy { get; set; }
        public Int64 RareID { get; set; }
        public Int64 Rearm { get; set; }
        public Int64 Refuel { get; set; }
        public Int64 Repair { get; set; }
        public Int64 SecEconomy { get; set; }
        public Int64 SectorID { get; set; }
        public String Security { get; set; }
        public Int64 Shipyard { get; set; }
        public Decimal ShipyardID { get; set; }

        public public_stars Star
        {
            get
            {
                if (_star == null)
                    _star = Parent.PublicStars.FirstOrDefault(x => (StarID == x.ID));

                return _star;
            }
        }

        public string StarAndMarketName
        {
            get
            {
                return $"{StarName}: {MarketName}";
            }
        }

        public Int64 StarID { get; set; }
        public String StarName { get; set; }
        public Int64 TechBroker { get; set; }
        public Int64 Visits { get; set; }

        [AutoMapper.IgnoreMap]
        internal List<public_marketprices> MarketPrices
        {
            get
            {
                if (_marketprices == null)
                    _marketprices = Parent.PublicMarketPrices.Where(x => (ID == x.MarketID)).ToList();

                return _marketprices;
                //return new List<public_marketprices>();
            }
        }

        internal Form1 Parent { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return StarAndMarketName;
        }

        #endregion Methods
    }

    public class public_stars
    {
        #region Fields

        private List<public_markets> _markets;

        #endregion Fields

        #region Properties

        public String Allegiance { get; set; }
        public Int64 Class { get; set; }
        public String Economy { get; set; }
        public String Faction { get; set; }
        public String FactionState { get; set; }
        public String Government { get; set; }
        public Int64 ID { get; set; }
        public Int64 LastUpdate { get; set; }
        public String Note { get; set; }
        public String Population { get; set; }
        public Int64 PowerID { get; set; }
        public String Security { get; set; }
        public String StarName { get; set; }
        public Int64 State { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }
        //public List<public_markets> Markets
        //{
        //    get
        //    {
        //        if (_markets == null)
        //            _markets = Parent.PublicMarkets.Where(x => (ID == x.StarID)).ToList();

        //        return _markets;
        //    }
        //}

        internal Form1 Parent { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return StarName;
        }

        #endregion Methods
    }
}