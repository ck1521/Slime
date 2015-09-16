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
    public class VehiclesController : Controller
    {
        private VehicleService vs;

        public VehiclesController()
        {
            vs = new VehicleService();
            AutoMapper.Mapper.CreateMap<Vehicle, VehicleViewModel>();
            AutoMapper.Mapper.CreateMap<VehicleViewModel, Vehicle>();

        }

        //  GET: /Vehicles/
        public ActionResult Index([Form] QueryOptions qo)
        {
            List<Vehicle> vehicles = vs.Get(qo);

            return View(AutoMapper.Mapper.Map<List<Vehicle>, List<VehicleViewModel>>(vehicles));
        }

        //  GET: /Vehicles/Create
        public ActionResult Create()
        {
            AutoMapper.Mapper.CreateMap<Engine, EngineViewModel>();

            List<EngineViewModel> evms = AutoMapper.Mapper.Map<List<Engine>, List<EngineViewModel>>(vs.GetAllEngines());

            return View("Form", new VehicleViewModel() { Engines = evms });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                vs.Insert(AutoMapper.Mapper.Map<VehicleViewModel, Vehicle>(vehicle));
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        //  GET: /Vehicles/Detail/id
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vehicle vehicle = vs.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(AutoMapper.Mapper.Map<Vehicle, VehicleViewModel>(vehicle));
        }

        //  GET: /Authors/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vehicle vehicle = vs.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View("Form", AutoMapper.Mapper.Map<Vehicle, VehicleViewModel>(vehicle));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<VehicleViewModel, Vehicle>();
                vs.Update(AutoMapper.Mapper.Map<VehicleViewModel, Vehicle>(vehicle));
                return RedirectToAction("Index");
            }

            return View("Form", vehicle);
        }

        ////  GET: /Authors/Delete/id
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
        //    return View(AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    Author author = db.Authors.Find(id);
        //    db.Authors.Remove(author);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //  GET: /Authors/IndexAjax
        //public ActionResult IndexAjax([Form] QueryOptions qo)
        //{
        //    var start = (qo.CurrentPage - 1) * qo.PageSize;
        //    var authors = db.Authors.
        //        OrderBy(qo.Sort).
        //        Skip(start).
        //        Take(qo.PageSize);

        //    qo.TotalPages = (int)Math.Ceiling((double)db.Authors.Count() / qo.PageSize);

        //    ViewBag.QueryOptions = qo;

        //    AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();

        //    return View(new ResultList<AuthorViewModel>
        //    {
        //        QueryOptions = qo,
        //        Results = AutoMapper.Mapper.Map<List<Author>, List<AuthorViewModel>>(authors.ToList())
        //    });

        //}


        ////  GET: /Authors/Create
        //public ActionResult CreateAjax()
        //{
        //    return View("FormAjax", new AuthorViewModel());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateAjax(AuthorViewModel author)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
        //        db.Authors.Add(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author));
        //        db.SaveChanges();
        //        return RedirectToAction("IndexAjax");
        //    }

        //    return View(author);
        //}

        ////  GET: /Authors/Edit/id
        //public ActionResult EditAjax(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    AutoMapper.Mapper.CreateMap<Author, AuthorViewModel>();
        //    return View("FormAjax", AutoMapper.Mapper.Map<Author, AuthorViewModel>(author));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditAjax(AuthorViewModel author)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AutoMapper.Mapper.CreateMap<AuthorViewModel, Author>();
        //        db.Entry(AutoMapper.Mapper.Map<AuthorViewModel, Author>(author)).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("IndexAjax");
        //    }

        //    return View("FormAjax", author);
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                vs.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
