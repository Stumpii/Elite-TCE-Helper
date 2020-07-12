using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
    internal class EDDB_StarSystem
    {
        internal List<StarSystem> starSystems;

        internal bool ReadFile()
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
}