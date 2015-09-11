using Slime.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Slime.DAL
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Hull> Hulls { get; set; }


    }
}