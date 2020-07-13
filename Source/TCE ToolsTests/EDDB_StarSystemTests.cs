using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCE_Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools.Tests
{
    [TestClass()]
    public class EDDB_StarSystemTests
    {
        [TestMethod()]
        public void ReadFileTest()
        {
            EDDB_StarSystem eDDB_StarSystem = new EDDB_StarSystem();
            var result = eDDB_StarSystem.ReadFile();

            Assert.IsTrue(result, "File not read correctly.");
        }
    }

    [TestClass()]
    public class EDDB_StationTests
    {
        [TestMethod()]
        public void ReadFileTest()
        {
            EDDB_Station eDDB_Station = new EDDB_Station();
            var result = eDDB_Station.ReadFile();

            Assert.IsTrue(result, "File not read correctly.");
        }
    }

    [TestClass()]
    public class EDDB_PricesTests
    {
        [TestMethod()]
        public void ReadFileTest()
        {
            EDDB_Prices eDDB_Prices = new EDDB_Prices();
            var result = eDDB_Prices.ReadFile();

            Assert.IsTrue(result, "File not read correctly.");
        }
    }
}