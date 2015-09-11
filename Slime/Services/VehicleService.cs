using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Slime.Models;
using Slime.DAL;
using Slime.ViewModels;
using System.Data.Entity;

namespace Slime.Services
{
    public class VehicleService :   IDisposable
    {
        private VehicleContext db = new VehicleContext();

        public VehicleService()
        {
        }

        public List<Vehicle> Get(QueryOptions qo)
        {
            int start = QueryUtility.GetStart(qo);
            var vehicles = db.Vehicles.
                OrderBy(qo.Sort).
                Skip(start).
                Take(qo.PageSize);

            qo.TotalPages = QueryUtility.GetTotal(db.Vehicles.Count(), qo.PageSize);

            return vehicles.ToList();
        }

        public Vehicle Find(int? id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException
                    (string.Format("Vehicle with id {0} not found", id));
            }
            return vehicle;
        }

        public void Insert(Vehicle vehicle)
        {
            db.Vehicles.Add(vehicle);
            db.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}