using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SoloOverhaul.Models.Config
{
    public class SOHConfig
    {
        [JsonPropertyName("General")]
        public GeneralConfig General { get; set; } = new();
    }
    public class GeneralConfig
    {
        public bool RemoveHideoutTimers { get; set; } = true;
        public bool RemoveFlea { get; set; } = true;
    }
}
