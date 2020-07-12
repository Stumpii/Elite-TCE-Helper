using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
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

    public class public_good
    {
        #region Fields

        private public_category _category;
        private List<public_marketprices> _marketprices;

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

        [AutoMapper.IgnoreMap]
        internal List<public_marketprices> MarketPrices
        {
            get
            {
                if (_marketprices == null)
                    _marketprices = Parent.PublicMarketPrices.Where(x => (ID == x.GoodID)).ToList();

                return _marketprices;
            }
        }

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

        private public_good _good;
        private public_markets _market;

        #endregion Fields

        #region Properties

        public Int64 Buy { get; set; }

        [BrightIdeasSoftware.OLVIgnore]
        public public_good Good
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

        private List<public_markets> _closestMarkets;
        private List<public_marketprices> _marketprices;
        private public_stars _star;

        [AutoMapper.IgnoreMap]
        internal public_markets ReferenceMarket { get; set; }

        #endregion Fields

        #region Properties

        public Int64 AllegianceID { get; set; }

        public Int64 Blackmarket { get; set; }

        public String BodyName { get; set; }

        public Int64 Broker { get; set; }

        [AutoMapper.IgnoreMap]
        internal List<public_markets> ClosestMarkets
        {
            get
            {
                if (_closestMarkets == null)
                    _closestMarkets = Parent.PublicMarkets.Where(x => (DistanceTo(x) < 16)).ToList();

                return _closestMarkets;
            }
        }

        [AutoMapper.IgnoreMap]
        public double DistanceToMarket
        {
            get
            {
                if (ReferenceMarket != null)
                {
                    var x = this.Star.X - ReferenceMarket.Star.X;
                    var y = this.Star.Y - ReferenceMarket.Star.Y;
                    var z = this.Star.Z - ReferenceMarket.Star.Z;

                    var xy = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    var xyz = Math.Sqrt(Math.Pow(xy, 2) + Math.Pow(z, 2));

                    return xyz;
                }
                else
                    return 1.0;
            }
        }

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

        public double DistanceTo(public_markets Market)
        {
            var x = this.Star.X - Market.Star.X;
            var y = this.Star.Y - Market.Star.Y;
            var z = this.Star.Z - Market.Star.Z;

            var xy = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var xyz = Math.Sqrt(Math.Pow(xy, 2) + Math.Pow(z, 2));

            return xyz;
        }

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