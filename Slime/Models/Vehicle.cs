using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slime.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public int HP { get; set; }
        public int DR { get; set; }
        public int FP { get; set; }
        public int AP { get; set; }
        public Engine T1Engine { get; set; }
    }
}