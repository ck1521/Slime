using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Slime.Models;
using Slime.DAL;
using Slime.Services;
using Slime.ViewModels;

namespace Slime.Controllers.Api
{
    public class EnginesController : ApiController
    {
        private EngineService es = new EngineService();

        public EnginesController()
        {
            AutoMapper.Mapper.CreateMap<Engine, EngineViewModel>();
            AutoMapper.Mapper.CreateMap<EngineViewModel, Engine>();
        }

        // GET api/Engines
        public ResultList<EngineViewModel> GetEngines([FromUri] QueryOptions qo)
        {
            List<Engine> engines = es.Get(qo);

            return new ResultList<EngineViewModel>()
            {
                QueryOptions = qo,
                Results = AutoMapper.Mapper.Map<List<Engine>, List<EngineViewModel>>(engines),
            };
        }

        // GET api/Engines/5
        [ResponseType(typeof(Engine))]
        public IHttpActionResult GetEngine(int id)
        {
            Engine engine = es.Find(id);
            if (engine == null)
            {
                return NotFound();
            }

            return Ok(engine);
        }

        // PUT api/Engines/5
        public IHttpActionResult PutEngine(int id, EngineViewModel engine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != engine.Id)
            {
                return BadRequest();
            }

            try
            {
                es.Update(AutoMapper.Mapper.Map<EngineViewModel, Engine>(engine));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Engines
        [ResponseType(typeof(Engine))]
        public IHttpActionResult PostEngine(EngineViewModel engine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            es.Insert(AutoMapper.Mapper.Map<EngineViewModel, Engine>(engine));

            return CreatedAtRoute("DefaultApi", new { id = engine.Id }, engine);
        }

        // DELETE api/Engines/5
        [ResponseType(typeof(Engine))]
        public IHttpActionResult DeleteEngine(int id)
        {
            Engine engine = es.Find(id);
            if (engine == null)
            {
                return NotFound();
            }

            // TODO: DELETE

            return Ok(engine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                es.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EngineExists(int id)
        {
            return es.Find(id) != null;
        }
    }
}