using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Slime.Models;

namespace Slime.ViewModels
{
    public class EngineViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mobility")]
        public int Mobility { get; set; }

        [Required]
        [JsonProperty(PropertyName = "stage")]
        public Stage Stage { get; set; }
    }
}