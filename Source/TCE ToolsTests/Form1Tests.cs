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
    public class Form1Tests
    {
        [TestMethod()]
        public void PercentileTest()
        {
            double[] sequence = new double[101]; // 0...100 is 101 values, 50 is halfway
            double excelPercentile = 0.75;

            for (int i = 0; i < sequence.Length; i++)
            {
                sequence[i] = i;
            }

            double ret = OtherStuff.Percentile(sequence, excelPercentile);

            Assert.IsTrue(ret == 75, "Percentile incorrect");
        }

        [TestMethod()]
        public void PercentileTest1()
        {
            double[] sequence = new double[5] { 0, 25, 50, 75, 100 };
            double excelPercentile = 0.5;

            double ret = OtherStuff.Percentile(sequence, excelPercentile);

            Assert.IsTrue(ret == 50, "Percentile incorrect");
        }
    }
}