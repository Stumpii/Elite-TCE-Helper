using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
}