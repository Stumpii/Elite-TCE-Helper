using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{

    public static class OtherStuff
    {
        public static double Percentile(double[] SortedList, double excelPercentile)
        {
            //Array.Sort(SortedList);
            int N = SortedList.Length;
            double n = (N - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return SortedList[0];
            else if (n == N) return SortedList[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return SortedList[k - 1] + d * (SortedList[k] - SortedList[k - 1]);
            }
        }

        public static bool ConvertJSONLtoJSON(string FilepathIn, string FilepathOut)
        {

            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader fileIn = new System.IO.StreamReader(FilepathIn);
            System.IO.StreamWriter fileOut = new System.IO.StreamWriter(FilepathOut, false);
            while ((line = fileIn.ReadLine()) != null)
            {
                // Ignore start and end brackets of JSON
                if (line == "[" || line == "]") continue;

                // Trim end of line comma from JSON
                fileOut.WriteLine(line.TrimEnd(','));
            }

            fileIn.Close();
            fileOut.Close();

            return true;
        }

        public static double StandardDeviation(this IEnumerable<double> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }

        // The normal distribution function.
        public static double F(double x, double mean, double stddev)
        {
            double one_over_2pi = (double)(1.0 / (stddev * Math.Sqrt(2 * Math.PI)));

            return (double)(one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * stddev * stddev)));
        }

    }

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public long id64 { get; set; }
        public string name { get; set; }
        public Coords coords { get; set; }
        public string allegiance { get; set; }
        public string government { get; set; }
        public string primaryEconomy { get; set; }
        public string secondaryEconomy { get; set; }
        public string security { get; set; }
        public long population { get; set; }
        public string date { get; set; }
        public Body[] bodies { get; set; }
        public Station1[] stations { get; set; }
        public Controllingfaction controllingFaction { get; set; }
        public Faction[] factions { get; set; }
        public string[] powers { get; set; }
        public string powerState { get; set; }
    }

    public class Coords
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class Controllingfaction
    {
        public string name { get; set; }
        public string government { get; set; }
        public string allegiance { get; set; }
    }

    public class Body
    {
        public long id64 { get; set; }
        public int bodyId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public float distanceToArrival { get; set; }
        public bool mainStar { get; set; }
        public int age { get; set; }
        public string spectralClass { get; set; }
        public string luminosity { get; set; }
        public float absoluteMagnitude { get; set; }
        public float solarMasses { get; set; }
        public float solarRadius { get; set; }
        public float surfaceTemperature { get; set; }
        public float orbitalPeriod { get; set; }
        public float semiMajorAxis { get; set; }
        public float orbitalEccentricity { get; set; }
        public float orbitalInclination { get; set; }
        public float argOfPeriapsis { get; set; }
        public float rotationalPeriod { get; set; }
        public bool rotationalPeriodTidallyLocked { get; set; }
        public float axialTilt { get; set; }
        public Station1[] stations { get; set; }
        public string updateTime { get; set; }
        public Ring[] rings { get; set; }
        public bool isLandable { get; set; }
        public float gravity { get; set; }
        public float earthMasses { get; set; }
        public float radius { get; set; }
        public float surfacePressure { get; set; }
        public string volcanismType { get; set; }
        public string atmosphereType { get; set; }
        public Parent[] parents { get; set; }
        public Solidcomposition solidComposition { get; set; }
        public string terraformingState { get; set; }
        public Materials materials { get; set; }
        public Atmospherecomposition atmosphereComposition { get; set; }
        public Belt[] belts { get; set; }
        public string reserveLevel { get; set; }
        public Signals signals { get; set; }
    }

    public class Solidcomposition
    {
        public float Ice { get; set; }
        public float Metal { get; set; }
        public float Rock { get; set; }
    }

    public class Materials
    {
        public float Cadmium { get; set; }
        public float Carbon { get; set; }
        public float Chromium { get; set; }
        public float Iron { get; set; }
        public float Manganese { get; set; }
        public float Nickel { get; set; }
        public float Niobium { get; set; }
        public float Phosphorus { get; set; }
        public float Ruthenium { get; set; }
        public float Sulphur { get; set; }
        public float Vanadium { get; set; }
        public float Germanium { get; set; }
        public float Molybdenum { get; set; }
        public float Tellurium { get; set; }
        public float Technetium { get; set; }
        public float Antimony { get; set; }
        public float Tungsten { get; set; }
        public float Zinc { get; set; }
        public float Selenium { get; set; }
        public float Tin { get; set; }
        public float Yttrium { get; set; }
        public float Mercury { get; set; }
        public float Polonium { get; set; }
        public float Zirconium { get; set; }
        public float Arsenic { get; set; }
    }

    public class Atmospherecomposition
    {
        public float Oxygen { get; set; }
        public float Silicates { get; set; }
        public float Sulphurdioxide { get; set; }
        public float Methane { get; set; }
        public float Helium { get; set; }
        public float Hydrogen { get; set; }
        public float Carbondioxide { get; set; }
        public float Nitrogen { get; set; }
        public float Water { get; set; }
        public float Ammonia { get; set; }
        public float Iron { get; set; }
        public float Neon { get; set; }
        public float Argon { get; set; }
    }

    public class Signals
    {
        public Signals1 signals { get; set; }
        public string updateTime { get; set; }
    }

    public class Signals1
    {
        public int SAA_SignalType_Biological { get; set; }
        public int SAA_SignalType_Geological { get; set; }
        public int SAA_SignalType_Human { get; set; }
        public int SAA_SignalType_Other { get; set; }
        public int SAA_SignalType_Thargoid { get; set; }
        public int SAA_SignalType_Guardian { get; set; }
    }

    public class Station1
    {
        public string name { get; set; }
        public string controllingFaction { get; set; }
        public object controllingFactionState { get; set; }
        public float distanceToArrival { get; set; }
        public string primaryEconomy { get; set; }
        public string allegiance { get; set; }
        public string government { get; set; }
        public string[] services { get; set; }
        public string type { get; set; }
        public long id { get; set; }
        public string updateTime { get; set; }
        public Market market { get; set; }
        public Outfitting outfitting { get; set; }
        public Shipyard shipyard { get; set; }
    }

    public class Market
    {
        public Commodity[] commodities { get; set; }
        public string[] prohibitedCommodities { get; set; }
        public string updateTime { get; set; }
    }

    public class Commodity
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string category { get; set; }
        public int commodityId { get; set; }
        public int demand { get; set; }
        public int supply { get; set; }
        public int buyPrice { get; set; }
        public int sellPrice { get; set; }
    }

    public class Outfitting
    {
        public Module[] modules { get; set; }
        public string updateTime { get; set; }
    }

    public class Module
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public int moduleId { get; set; }
        public int _class { get; set; }
        public string rating { get; set; }
        public string category { get; set; }
        public string ship { get; set; }
    }

    public class Shipyard
    {
        public Ship[] ships { get; set; }
        public string updateTime { get; set; }
    }

    public class Ship
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public int shipId { get; set; }
    }

    public class Ring
    {
        public string name { get; set; }
        public string type { get; set; }
        public float mass { get; set; }
        public float innerRadius { get; set; }
        public float outerRadius { get; set; }
        public Signals2 signals { get; set; }
    }

    public class Signals2
    {
        public Signals3 signals { get; set; }
        public string updateTime { get; set; }
    }

    public class Signals3
    {
        public int Monazite { get; set; }
        public int Musgravite { get; set; }
        public int Serendibite { get; set; }
        public int Platinum { get; set; }
        public int Rhodplumsite { get; set; }
        public int Alexandrite { get; set; }
        public int Benitoite { get; set; }
        public int Painite { get; set; }
        public int Bromellite { get; set; }
        public int Grandidierite { get; set; }
        public int Tritium { get; set; }
        public int LowTemperatureDiamond { get; set; }
        public int Opal { get; set; }
        public int Bauxite { get; set; }
    }

    public class Parent
    {
        public int Star { get; set; }
        public int Null { get; set; }
        public int Planet { get; set; }
    }

    public class Belt
    {
        public string name { get; set; }
        public string type { get; set; }
        public float mass { get; set; }
        public float innerRadius { get; set; }
        public float outerRadius { get; set; }
    }

    public class Faction
    {
        public string name { get; set; }
        public string allegiance { get; set; }
        public string government { get; set; }
        public float influence { get; set; }
        public string state { get; set; }
    }


    public class MaterialsLists
    {
        public List<double> Cadmium { get; set; } = new List<double>();
        public List<double> Carbon { get; set; } = new List<double>();
        public List<double> Chromium { get; set; } = new List<double>();
        public List<double> Iron { get; set; } = new List<double>();
        public List<double> Manganese { get; set; } = new List<double>();
        public List<double> Nickel { get; set; } = new List<double>();
        public List<double> Niobium { get; set; } = new List<double>();
        public List<double> Phosphorus { get; set; } = new List<double>();
        public List<double> Ruthenium { get; set; } = new List<double>();
        public List<double> Sulphur { get; set; } = new List<double>();
        public List<double> Vanadium { get; set; } = new List<double>();
        public List<double> Germanium { get; set; } = new List<double>();
        public List<double> Molybdenum { get; set; } = new List<double>();
        public List<double> Tellurium { get; set; } = new List<double>();
        public List<double> Technetium { get; set; } = new List<double>();
        public List<double> Antimony { get; set; } = new List<double>();
        public List<double> Tungsten { get; set; } = new List<double>();
        public List<double> Zinc { get; set; } = new List<double>();
        public List<double> Selenium { get; set; } = new List<double>();
        public List<double> Tin { get; set; } = new List<double>();
        public List<double> Yttrium { get; set; } = new List<double>();
        public List<double> Mercury { get; set; } = new List<double>();
        public List<double> Polonium { get; set; } = new List<double>();
        public List<double> Zirconium { get; set; } = new List<double>();
        public List<double> Arsenic { get; set; } = new List<double>();
    }


}
