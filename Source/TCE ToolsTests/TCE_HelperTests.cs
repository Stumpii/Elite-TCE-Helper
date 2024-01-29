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
    public class TCE_HelperTests
    {
        [TestMethod()]
        public void ReadStarsTest()
        {
            bool res;

            TCE_Helper tCE_Helper = new TCE_Helper();
            res = tCE_Helper.ReadStars();

            Assert.IsTrue(res, "Could not real Star DB");
        }
    }
}