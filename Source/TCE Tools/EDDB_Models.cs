using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCE_Tools
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class State
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Station
    {
        public int id { get; set; }
        public string name { get; set; }
        public int system_id { get; set; }
        public int updated_at { get; set; }
        public string max_landing_pad_size { get; set; }
        public int? distance_to_star { get; set; }
        public int? government_id { get; set; }
        public string government { get; set; }
        public int? allegiance_id { get; set; }
        public string allegiance { get; set; }
        public List<State> states { get; set; }
        public int? type_id { get; set; }
        public string type { get; set; }
        public bool has_blackmarket { get; set; }
        public bool has_market { get; set; }
        public bool has_refuel { get; set; }
        public bool has_repair { get; set; }
        public bool has_rearm { get; set; }
        public bool has_outfitting { get; set; }
        public bool has_shipyard { get; set; }
        public bool has_docking { get; set; }
        public bool has_commodities { get; set; }
        public List<string> import_commodities { get; set; }
        public List<string> export_commodities { get; set; }
        public List<string> prohibited_commodities { get; set; }
        public List<string> economies { get; set; }
        public int? shipyard_updated_at { get; set; }
        public int? outfitting_updated_at { get; set; }
        public int? market_updated_at { get; set; }
        public bool is_planetary { get; set; }
        public List<string> selling_ships { get; set; }
        public List<int> selling_modules { get; set; }
        public object settlement_size_id { get; set; }
        public object settlement_size { get; set; }
        public object settlement_security_id { get; set; }
        public object settlement_security { get; set; }
        public int? body_id { get; set; }
        public int? controlling_minor_faction_id { get; set; }
        public long? ed_market_id { get; set; }
    }

    public class ActiveState
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RecoveringState
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class MinorFactionPresence
    {
        public int? happiness_id { get; set; }
        public int? minor_faction_id { get; set; }
        public double? influence { get; set; }
        public List<ActiveState> active_states { get; set; }
        public List<object> pending_states { get; set; }
        public List<RecoveringState> recovering_states { get; set; }
    }

    public class StarSystem
    {
        public int id { get; set; }
        public int? edsm_id { get; set; }
        public string name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public ulong population { get; set; }
        public bool is_populated { get; set; }
        public int? government_id { get; set; }
        public string government { get; set; }
        public int? allegiance_id { get; set; }
        public string allegiance { get; set; }
        public List<State> states { get; set; }
        public int? security_id { get; set; }
        public string security { get; set; }
        public int? primary_economy_id { get; set; }
        public string primary_economy { get; set; }
        public string power { get; set; }
        public string power_state { get; set; }
        public int? power_state_id { get; set; }
        public bool needs_permit { get; set; }
        public int updated_at { get; set; }
        public string simbad_ref { get; set; }
        public int? controlling_minor_faction_id { get; set; }
        public string controlling_minor_faction { get; set; }
        public int? reserve_type_id { get; set; }
        public string reserve_type { get; set; }
        public List<MinorFactionPresence> minor_faction_presences { get; set; }
        public long? ed_system_address { get; set; }
    }
}