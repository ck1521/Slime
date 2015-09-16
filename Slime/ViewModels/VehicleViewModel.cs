using Newtonsoft.Json;
using Slime.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slime.ViewModels
{
    public class VehicleViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "hp")]
        public int HP { get; set; }

        [JsonProperty(PropertyName = "dr")]
        public int DR { get; set; }

        [JsonProperty(PropertyName = "fp")]
        public int FP { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public Rank Rank { get; set; }

        [JsonProperty(PropertyName = "engines")]
        public virtual ICollection<EngineViewModel> Engines { get; set; }

    }
}