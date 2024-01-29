using AutoMapper;
using AutoMapper.Data;
using BrightIdeasSoftware;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TCE_Tools
{
    public partial class Form1 : Form
    {
        #region Fields

        public List<public_category> PublicCategory = new List<public_category>();

        public List<public_good> PublicGoods = new List<public_good>();

        //public List<public_marketTradeItem> PublicMarketPrices = new List<public_marketTradeItem>();

        public List<public_market> PublicMarkets = new List<public_market>();

        public List<public_star> PublicStars = new List<public_star>();

        internal public_market SelectedMarket;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void cmdLoadTCEData_Click(object sender, EventArgs e)
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
                cfg.CreateMap<IDataRecord, public_star>();
                cfg.CreateMap<IDataRecord, public_market>();
                cfg.CreateMap<IDataRecord, public_good>();
                cfg.CreateMap<IDataRecord, public_category>();
            });
            var mapper = config.CreateMapper();


            //// =========== TEST CODE TO PRINT COLUMN TYPES =========== 
            //var tableNames = new List<string>() { "public_markets", "" };
            //foreach (var tbl in tableNames)
            //{
            //    DataColumnCollection columns = myDataSet.Tables[tbl].Columns;
            //    Debug.WriteLine(string.Empty);
            //    Debug.WriteLine($"public class {tbl}\r\n{{");
            //    for (int i = 0; i < columns.Count; i++)
            //    {
            //        var datatype = columns[i].DataType.ToString().Replace("System.", "");
            //        var columnName = columns[i].ColumnName;
            //        var shortColumnName = columns[i].ColumnName.Replace(" ", "");
            //        Debug.WriteLine($"public {datatype} {shortColumnName}  {{ get; set; }}");
            //    }
            //    Debug.WriteLine("}");
            //    for (int i = 0; i < columns.Count; i++)
            //    {
            //        var datatype = columns[i].DataType.ToString().Replace("System.", "");
            //        var columnName = columns[i].ColumnName;
            //        var shortColumnName = columns[i].ColumnName.Replace(" ", "");
            //        if (datatype == "String")
            //            Debug.WriteLine($"{shortColumnName} = Convert.IsDBNull(row[\"{columnName}\"]) ? default(string) : Convert.ToString(row[\"{columnName}\"]);");
            //        else if (datatype == "Double")
            //        {
            //            Debug.WriteLine($"{shortColumnName} = Convert.IsDBNull(row[\"{columnName}\"]) ? default(int?) : Convert.ToInt32(row[\"{columnName}\"]);");
            //            Debug.WriteLine($"{shortColumnName} = Convert.IsDBNull(row[\"{columnName}\"]) ? default(double?) : Convert.ToDouble(row[\"{columnName}\"]);");
            //        }
            //        else
            //        {
            //            Debug.WriteLine($"{shortColumnName} = Convert.IsDBNull(row[\"{columnName}\"]) ? default({datatype}) : Convert.To{datatype}(row[\"{columnName}\"]);");
            //            Debug.WriteLine($"{shortColumnName} = Convert.IsDBNull(row[\"{columnName}\"]) ? default(string) : Convert.ToString(row[\"{columnName}\"]);");
            //        }
            //    }
            //    Debug.WriteLine(string.Empty);
            //}
            //// =========== END TEST CODE TO PRINT COLUMN TYPES ===========


            // Read in public_stars table
            PublicStars = mapper.Map<IDataReader,
                List<public_star>>(myDataSet.Tables["public_stars"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicStars)
                item.Parent = this;

            // Read in public_markets table
            PublicMarkets = mapper.Map<IDataReader,
                List<public_market>>(myDataSet.Tables["public_markets"].CreateDataReader());
            // Define parent so it can examine the other tables read
            foreach (var item in PublicMarkets)
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

            //lvwStars.DataSource = PublicStars;
            Helper.UpdateObjectListViewColumns(lvwStars);

            //lvwMarkets.DataSource = PublicMarkets;
            Helper.UpdateObjectListViewColumns(lvwMarkets);

            //lvwCategories.DataSource = PublicCategory;
            Helper.UpdateObjectListViewColumns(lvwCategories);

            lvwGoods.DataSource = PublicGoods;
            Helper.UpdateObjectListViewColumns(lvwGoods);

            lvwGoods2.DataSource = PublicGoods;
            Helper.UpdateObjectListViewColumns(lvwGoods2);

            //List<string> myList =  PublicMarkets.Select(x => (x.ToString())).ToList();
            //myList.Sort();
            //comboBox1.DataSource = myList;

            List<public_market> myList = new List<public_market>(PublicMarkets);
            myList.Sort((a, b) => a.StarAndMarketName.CompareTo(b.StarAndMarketName));
            comboBox1.DataSource = myList;

            //public_markets[] comboList = new public_markets[PublicMarkets.Count];
            //PublicMarkets.CopyTo(comboList);
            //comboList.ToList();
            //comboBox1.DataSource= comboList;
            SelectedMarket = (public_market)comboBox1.SelectedItem;
        }

        #endregion Methods

        private void button2_Click(object sender, EventArgs e)
        {
            List<public_good> selectedGoods = new List<public_good>();
            foreach (var item in lvwGoods2.SelectedObjects)
            {
                selectedGoods.Add((public_good)item);
            }

            if (radioButtonBuy.Checked)
            {
                List<public_market> public_market = new List<public_market>();

                //bool removeMarket = true;
                foreach (var market in SelectedMarket.ClosestMarkets)
                {
                    foreach (var selectedGood in selectedGoods)
                    {
                        foreach (var pricing in market.MarketPrices)
                        {
                            if (selectedGood.Tradegood == pricing.GoodName)
                            {
                                public_market.Add(market);
                            }
                        }
                    }
                }

                fastDataListView6.DataSource = public_market;

                //fastDataListView6.DataSource = public_Goods.MarketPrices.Where(x => x.Stock > 0).ToList();
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
            SelectedMarket = (public_market)comboBox1.SelectedItem;

            foreach (var item in PublicMarkets)
            {
                item.ReferenceMarket = SelectedMarket;
            }

               // TODO - fot test only'
            lvwCategories.DataSource = SelectedMarket.TradeItemsList;
            Helper.UpdateObjectListViewColumns(lvwCategories);
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

            EDDB_Prices eDDB_Prices = new EDDB_Prices();
            eDDB_Prices.ReadFile();

            fastDataListView5.DataSource = eDDB_Prices.prices;
            Helper.UpdateObjectListViewColumns(fastDataListView5);

            // Creating and initializing threads
            //Thread thr1 = new Thread(eDDB_StarSystem.ReadFile);
            //Thread thr2 = new Thread(method2);
            //Thread thr3 = new Thread(method2);

            //thr1.Start();
            //thr2.Start();
            //thr3.Start();

            //wait for t1 to fimish
            //thr1.Join();
            //thr2.Join();
            //thr3.Join();
        }

        private bool disableStationListView;
        private bool disableStarSystmListView;

        public string SystemNameFilter = "";

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (SelectedMarket != null)
            {
                dataListView1.DataSource = SelectedMarket.ImportMarkets;
                Helper.UpdateObjectListViewColumns(dataListView1);

                dataListView2.DataSource = SelectedMarket.ExportMarkets;
                Helper.UpdateObjectListViewColumns(dataListView2);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ObjectListViewMenuFilterBuilder.SELECT_ALL_LABEL = "[Select All]";

            eDDBStarSystemListLiew.UseFiltering = true;
            eDDBStarSystemListLiew.UseFilterIndicator = true;
            eDDBStarSystemListLiew.ShowCommandMenuOnRightClick = true;
            eDDBStarSystemListLiew.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.ModelDialog;
            eDDBStarSystemListLiew.FilterMenuBuildStrategy = new ObjectListViewMenuFilterBuilder();
            eDDBStarSystemListLiew.FilterMenuBuildStrategy.TreatNullAsDataValue = true;
            eDDBStarSystemListLiew.SortGroupItemsByPrimaryColumn = false;

            eDDBStarSystemListLiew.AdditionalFilter = new TagnameFilter(this);
        }

        private void SystemNameTextBox_TextChanged(object sender, EventArgs e)
        {
            SystemNameFilter = SystemNameTextBox.Text;
            eDDBStarSystemListLiew.UpdateColumnFiltering();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            // deserialize JSON directly from a file
            string filepathIn = @"C:\Temp\galaxy.json";
            string filepathOut = @"C:\Temp\galaxy.jsonl";
            //string filepathIn = @"C:\Temp\galaxy_1day.json";
            //string filepathOut = @"C:\Temp\galaxy_1day.jsonl";
            //string filepathIn = @"C:\Temp\GalaxySample.json";
            //string filepathOut = @"C:\Temp\GalaxySample.jsonl";

            // Create JSONL if it does not exist
            if (!File.Exists(filepathOut))
                OtherStuff.ConvertJSONLtoJSON(filepathIn, filepathOut);

            //string filename = @"C:\Temp\GalaxySample.jsonl";
            //using (StreamReader file = File.OpenText(filename))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Class1[] rootobject = (Class1[])serializer.Deserialize(file, typeof(Class1[]));
            //}
            MaterialsLists materialsLists = new MaterialsLists();

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(filepathOut);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                Class1 myDeserializedClass = JsonConvert.DeserializeObject<Class1>(line);

                foreach (var body in myDeserializedClass.bodies)
                {
                    if (body.materials != null)
                    {
                        Materials materials = body.materials;

                        if (materials.Cadmium > 0) materialsLists.Cadmium.Add(materials.Cadmium);
                        if (materials.Carbon > 0) materialsLists.Carbon.Add(materials.Carbon);
                        if (materials.Chromium > 0) materialsLists.Chromium.Add(materials.Chromium);
                        if (materials.Iron > 0) materialsLists.Iron.Add(materials.Iron);
                        if (materials.Manganese > 0) materialsLists.Manganese.Add(materials.Manganese);
                        if (materials.Nickel > 0) materialsLists.Nickel.Add(materials.Nickel);
                        if (materials.Niobium > 0) materialsLists.Niobium.Add(materials.Niobium);
                        if (materials.Phosphorus > 0) materialsLists.Phosphorus.Add(materials.Phosphorus);
                        if (materials.Ruthenium > 0) materialsLists.Ruthenium.Add(materials.Ruthenium);
                        if (materials.Sulphur > 0) materialsLists.Sulphur.Add(materials.Sulphur);
                        if (materials.Vanadium > 0) materialsLists.Vanadium.Add(materials.Vanadium);
                        if (materials.Germanium > 0) materialsLists.Germanium.Add(materials.Germanium);
                        if (materials.Molybdenum > 0) materialsLists.Molybdenum.Add(materials.Molybdenum);
                        if (materials.Tellurium > 0) materialsLists.Tellurium.Add(materials.Tellurium);
                        if (materials.Technetium > 0) materialsLists.Technetium.Add(materials.Technetium);
                        if (materials.Antimony > 0) materialsLists.Antimony.Add(materials.Antimony);
                        if (materials.Tungsten > 0) materialsLists.Tungsten.Add(materials.Tungsten);
                        if (materials.Zinc > 0) materialsLists.Zinc.Add(materials.Zinc);
                        if (materials.Selenium > 0) materialsLists.Selenium.Add(materials.Selenium);
                        if (materials.Tin > 0) materialsLists.Tin.Add(materials.Tin);
                        if (materials.Yttrium > 0) materialsLists.Yttrium.Add(materials.Yttrium);
                        if (materials.Mercury > 0) materialsLists.Mercury.Add(materials.Mercury);
                        if (materials.Polonium > 0) materialsLists.Polonium.Add(materials.Polonium);
                        if (materials.Zirconium > 0) materialsLists.Zirconium.Add(materials.Zirconium);
                        if (materials.Arsenic > 0) materialsLists.Arsenic.Add(materials.Arsenic);
                    }
                }
            }

            // Sort values
            materialsLists.Cadmium.Sort();
            materialsLists.Carbon.Sort();
            materialsLists.Chromium.Sort();
            materialsLists.Iron.Sort();
            materialsLists.Manganese.Sort();
            materialsLists.Nickel.Sort();
            materialsLists.Niobium.Sort();
            materialsLists.Phosphorus.Sort();
            materialsLists.Ruthenium.Sort();
            materialsLists.Sulphur.Sort();
            materialsLists.Vanadium.Sort();
            materialsLists.Germanium.Sort();
            materialsLists.Molybdenum.Sort();
            materialsLists.Tellurium.Sort();
            materialsLists.Technetium.Sort();
            materialsLists.Antimony.Sort();
            materialsLists.Tungsten.Sort();
            materialsLists.Zinc.Sort();
            materialsLists.Selenium.Sort();
            materialsLists.Tin.Sort();
            materialsLists.Yttrium.Sort();
            materialsLists.Mercury.Sort();
            materialsLists.Polonium.Sort();
            materialsLists.Zirconium.Sort();
            materialsLists.Arsenic.Sort();

            //GetPercentileValues(materialsLists.Cadmium, "Cadmium");
            //GetPercentileValues(materialsLists.Carbon, "Carbon");
            //GetPercentileValues(materialsLists.Chromium, "Chromium");
            //GetPercentileValues(materialsLists.Iron, "Iron");
            //GetPercentileValues(materialsLists.Manganese, "Manganese");
            //GetPercentileValues(materialsLists.Nickel, "Nickel");
            //GetPercentileValues(materialsLists.Niobium, "Niobium");
            //GetPercentileValues(materialsLists.Phosphorus, "Phosphorus");
            //GetPercentileValues(materialsLists.Ruthenium, "Ruthenium");
            //GetPercentileValues(materialsLists.Sulphur, "Sulphur");
            //GetPercentileValues(materialsLists.Vanadium, "Vanadium");
            //GetPercentileValues(materialsLists.Germanium, "Germanium");
            //GetPercentileValues(materialsLists.Molybdenum, "Molybdenum");
            //GetPercentileValues(materialsLists.Tellurium, "Tellurium");
            //GetPercentileValues(materialsLists.Technetium, "Technetium");
            //GetPercentileValues(materialsLists.Antimony, "Antimony");
            //GetPercentileValues(materialsLists.Tungsten, "Tungsten");
            //GetPercentileValues(materialsLists.Zinc, "Zinc");
            //GetPercentileValues(materialsLists.Selenium, "Selenium");
            //GetPercentileValues(materialsLists.Tin, "Tin");
            //GetPercentileValues(materialsLists.Yttrium, "Yttrium");
            //GetPercentileValues(materialsLists.Mercury, "Mercury");
            //GetPercentileValues(materialsLists.Polonium, "Polonium");
            //GetPercentileValues(materialsLists.Zirconium, "Zirconium");
            //GetPercentileValues(materialsLists.Arsenic, "Arsenic");

            double[] percentiles = { 100, 99, 97, 95, 90, 85, 75, 50 };

            foreach (var pct in percentiles)
            {
                double pctUnity = pct / 100;

                SortedDictionary<string, double> MaterialsPercentile = new SortedDictionary<string, double>();
                MaterialsPercentile.Add("cadmium", GetPercentileValue(materialsLists.Cadmium, pctUnity));
                MaterialsPercentile.Add("carbon", GetPercentileValue(materialsLists.Carbon, pctUnity));
                MaterialsPercentile.Add("chromium", GetPercentileValue(materialsLists.Chromium, pctUnity));
                MaterialsPercentile.Add("iron", GetPercentileValue(materialsLists.Iron, pctUnity));
                MaterialsPercentile.Add("manganese", GetPercentileValue(materialsLists.Manganese, pctUnity));
                MaterialsPercentile.Add("nickel", GetPercentileValue(materialsLists.Nickel, pctUnity));
                MaterialsPercentile.Add("niobium", GetPercentileValue(materialsLists.Niobium, pctUnity));
                MaterialsPercentile.Add("phosphorus", GetPercentileValue(materialsLists.Phosphorus, pctUnity));
                MaterialsPercentile.Add("ruthenium", GetPercentileValue(materialsLists.Ruthenium, pctUnity));
                MaterialsPercentile.Add("sulphur", GetPercentileValue(materialsLists.Sulphur, pctUnity));
                MaterialsPercentile.Add("vanadium", GetPercentileValue(materialsLists.Vanadium, pctUnity));
                MaterialsPercentile.Add("germanium", GetPercentileValue(materialsLists.Germanium, pctUnity));
                MaterialsPercentile.Add("molybdenum", GetPercentileValue(materialsLists.Molybdenum, pctUnity));
                MaterialsPercentile.Add("tellurium", GetPercentileValue(materialsLists.Tellurium, pctUnity));
                MaterialsPercentile.Add("technetium", GetPercentileValue(materialsLists.Technetium, pctUnity));
                MaterialsPercentile.Add("antimony", GetPercentileValue(materialsLists.Antimony, pctUnity));
                MaterialsPercentile.Add("tungsten", GetPercentileValue(materialsLists.Tungsten, pctUnity));
                MaterialsPercentile.Add("zinc", GetPercentileValue(materialsLists.Zinc, pctUnity));
                MaterialsPercentile.Add("selenium", GetPercentileValue(materialsLists.Selenium, pctUnity));
                MaterialsPercentile.Add("tin", GetPercentileValue(materialsLists.Tin, pctUnity));
                MaterialsPercentile.Add("yttrium", GetPercentileValue(materialsLists.Yttrium, pctUnity));
                MaterialsPercentile.Add("mercury", GetPercentileValue(materialsLists.Mercury, pctUnity));
                MaterialsPercentile.Add("polonium", GetPercentileValue(materialsLists.Polonium, pctUnity));
                MaterialsPercentile.Add("zirconium", GetPercentileValue(materialsLists.Zirconium, pctUnity));
                MaterialsPercentile.Add("arsenic", GetPercentileValue(materialsLists.Arsenic, pctUnity));

                Debug.WriteLine("");
                Debug.WriteLine($"\t\tSortedDictionary<string, double> Materials{pct}Percentile = new SortedDictionary<string, double>");
                Debug.WriteLine("\t\t{");
                foreach (var item in MaterialsPercentile)
                {
                    Debug.WriteLine($"\t\t\t{{ \"{item.Key}\", {item.Value:f} }},");
                }
                Debug.WriteLine("\t\t};");
            }

            //Clipboard.SetText(string.Join("\r\n", materialsLists.Carbon));
            //Clipboard.SetText(CarbonPct.ToString());

            //Debug.WriteLine(CarbonPct.ToString());

            //double antimonyAvg = materialsLists.Antimony.Average();
            //double antimonySD = OtherStuff.StandardDeviation(materialsLists.Antimony.AsEnumerable());
            //double pct = OtherStuff.F(antimonyAvg, antimonyAvg, antimonySD);

            //materialsLists.Arsenic.Sort((x, y) => (y.CompareTo(x)));
            MessageBox.Show("Finished.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public PercentileValues GetPercentileValues(List<double> materialsLists, string Name)
        {
            double[] values = materialsLists.ToArray();
            Array.Sort(values);

            var pctVals = new PercentileValues();
            pctVals.Name = Name;
            pctVals.ValAt100Pct = OtherStuff.Percentile(values, pctVals.Pct100);
            pctVals.ValAt99Pct = OtherStuff.Percentile(values, pctVals.Pct99);
            pctVals.ValAt97Pct = OtherStuff.Percentile(values, pctVals.Pct97);
            pctVals.ValAt95Pct = OtherStuff.Percentile(values, pctVals.Pct95);
            pctVals.ValAt90Pct = OtherStuff.Percentile(values, pctVals.Pct90);
            pctVals.ValAt85Pct = OtherStuff.Percentile(values, pctVals.Pct85);
            pctVals.ValAt75Pct = OtherStuff.Percentile(values, pctVals.Pct75);
            pctVals.ValAt50Pct = OtherStuff.Percentile(values, pctVals.Pct50);

            Debug.WriteLine("");
            Debug.WriteLine(pctVals.ToString());

            return pctVals;
        }

        public double GetPercentileValue(List<double> materialsLists, double Percentile)
        {
            double[] values = materialsLists.ToArray();
            return OtherStuff.Percentile(values, Percentile);
        }

        public class PercentileValues
        {
            public double ValAt100Pct;
            public double ValAt99Pct;
            public double ValAt97Pct;
            public double ValAt95Pct;
            public double ValAt90Pct;
            public double ValAt85Pct;
            public double ValAt75Pct;
            public double ValAt50Pct;

            public double Pct100 { get; set; } = 1.0;
            public double Pct99 { get; set; } = 0.99;
            public double Pct97 { get; set; } = 0.97;
            public double Pct95 { get; set; } = 0.95;
            public double Pct90 { get; set; } = 0.90;
            public double Pct85 { get; set; } = 0.85;
            public double Pct75 { get; set; } = 0.75;
            public double Pct50 { get; set; } = 0.50;
            public string Name { get; internal set; }

            public string EDName { get { return Name.ToLower(); } }

            public override string ToString()
            {
                string[] list = new string[]
                {
                    $"public struct {Name}PercentileValues",
                    $"{{",
                    $"public const double ValAt100Pct = {ValAt100Pct:f};",
                    $"public const double ValAt99Pct = {ValAt99Pct:f};",
                    $"public const double ValAt97Pct = {ValAt97Pct:f};",
                    $"public const double ValAt95Pct = {ValAt95Pct:f};",
                    $"public const double ValAt90Pct = {ValAt90Pct:f};",
                    $"public const double ValAt85Pct = {ValAt85Pct:f};",
                    $"public const double ValAt75Pct = {ValAt75Pct:f};",
                    $"public const double ValAt50Pct = {ValAt50Pct:f};",
                    $"}}",
                 };

                return string.Join("\r\n", list);
            }
        }
    }
}