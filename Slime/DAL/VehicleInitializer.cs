using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Slime.Models;

namespace Slime.DAL
{
    public class VehicleInitializer : DropCreateDatabaseIfModelChanges<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {
            var engine = new Engine()
            {
                Name = "TestEng",
                Mobility = 123,
                Stage = Stage.Low,
            };

            var engine2 = new Engine()
            {
                Name = "Breaking",
                Mobility = 20,
                Stage = Stage.Low,
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Name = "num one",
                    DR = 10,
                    Rank = Models.Rank.Purple,
                    T1Engine = engine,
                },

                new Vehicle
                {
                    Name = "trick",
                    DR= 5,
                    Rank = Models.Rank.Blue,
                    T1Engine = engine2,
                }
            };
            vehicles.ForEach(v => context.Vehicles.Add(v));
            context.SaveChanges();
        }
    }
}