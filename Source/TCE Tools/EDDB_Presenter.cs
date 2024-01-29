using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
    public class EDDB_StarSystem
    {
        public List<StarSystem> starSystems;

        public bool ReadFile()
        {
            starSystems = new List<StarSystem>();

            // Read the file and display it line by line.
            StreamReader file = new global::System.IO.StreamReader(@"C:\TCE\EDDB_Data\EDDB_Systems_populated.jsonl");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                StarSystem myDeserializedClass = JsonConvert.DeserializeObject<StarSystem>(line);
                starSystems.Add(myDeserializedClass);
            }

            return true;
        }
    }

    public class EDDB_Station
    {
        public List<Station> stations;

        public bool ReadFile()
        {
            stations = new List<Station>();

            // Read the file and display it line by line.
            StreamReader file1 = new global::System.IO.StreamReader(@"C:\TCE\EDDB_Data\EDDB_Stations.jsonl");
            string line;
            while ((line = file1.ReadLine()) != null)
            {
                Station myDeserializedClass = JsonConvert.DeserializeObject<Station>(line);
                stations.Add(myDeserializedClass);
            }

            return true;
        }
    }

    public class EDDB_Prices
    {
        public List<Prices> prices;

        public bool ReadFile()
        {
            prices = new List<Prices>();

            using (var reader = new StreamReader(@"C:\TCE\EDDB_Data\EDDB_Prices.csv"))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    prices = csv.GetRecords<Prices>().ToList();
            //}
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var price = csv.GetRecord<Prices>();
                    if (price != null)
                        prices.Add(price);
                }
            }
            return true;
        }
    }
}