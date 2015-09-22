using Slime.DAL;
using Slime.Models;
using Slime.Services;
using Slime.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Slime.Controllers
{
    public class EnginesController : Controller
    {
        private EngineService es = null;
        public EnginesController()
        {
            es = new EngineService();
            AutoMapper.Mapper.CreateMap<Engine, EngineViewModel>();
            AutoMapper.Mapper.CreateMap<EngineViewModel, Engine>();
        }

        //  GET: /Engines/
        public ActionResult Index([Form] QueryOptions qo)
        {
            List<Engine> engines = es.Get(qo);

            return View(new ResultList<EngineViewModel>()
            {
                QueryOptions = qo,
                Results = AutoMapper.Mapper.Map<List<Engine>, List<EngineViewModel>>(engines)
            });
        }

        //  GET: /Engines/Create
        public ActionResult Create()
        {
            return View("Form", new EngineViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EngineViewModel engine)
        {
            if (ModelState.IsValid)
            {
                es.Insert(AutoMapper.Mapper.Map<EngineViewModel, Engine>(engine));
                return RedirectToAction("Index");
            }

            return View(engine);
        }

        //  GET: /Engines/Detail/id
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Engine engine = es.Find(id);
            if (engine == null)
            {
                return HttpNotFound();
            }

            return View(AutoMapper.Mapper.Map<Engine, EngineViewModel>(engine));
        }

        //  GET: /Engines/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Engine engine = es.Find(id);
            if (engine == null)
            {
                return HttpNotFound();
            }
            return View("Form", AutoMapper.Mapper.Map<Engine, EngineViewModel>(engine));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EngineViewModel engine)
        {
            if (ModelState.IsValid)
            {
                es.Update(AutoMapper.Mapper.Map<EngineViewModel, Engine>(engine));
                return RedirectToAction("Index");
            }

            return View("Form", engine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                es.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}