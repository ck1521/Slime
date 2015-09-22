using Slime.DAL;
using Slime.Models;
using Slime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Slime.Services
{
    public class EngineService:  IDisposable
    {
        private VehicleContext db = new VehicleContext();

        public EngineService()
        {
        }

        public List<Engine> Get(QueryOptions qo)
        {
            int start = QueryUtility.GetStart(qo);
            var engines = db.Engines.
                OrderBy(qo.Sort).
                Skip(start).
                Take(qo.PageSize);

            qo.TotalPages = QueryUtility.GetTotal(db.Engines.Count(), qo.PageSize);

            return engines.ToList();
        }

        public Engine Find(int? id)
        {
            Engine engine = db.Engines.Find(id);
            if (engine == null)
            {
                throw new System.Data.Entity.Core.ObjectNotFoundException
                    (string.Format("engines with id {0} not found", id));
            }
            return engine;
        }

        public void Insert(Engine engine)
        {
            db.Engines.Add(engine);
            db.SaveChanges();
        }

        public void Update(Engine engine)
        {
            db.Entry(engine).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Engine> GetAllEngines()
        {
            return db.Engines.Where(e => e.Stage == Stage.Low)
                .Select(e => new { e.Id, e.Name }).AsEnumerable()
                .Select(x => new Engine { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}