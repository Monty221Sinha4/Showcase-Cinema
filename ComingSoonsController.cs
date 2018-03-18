using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieApp.com;

namespace MovieApp.com.Controllers
{
    public class ComingSoonsController : Controller
    {
        private ComingSoonDb db = new ComingSoonDb();

        // GET: ComingSoons
        public ActionResult Index()
        {
            var comingSoons = db.ComingSoons.Include(c => c.MoviesComingSoon);
            return View(comingSoons.ToList());
        }

        // GET: ComingSoons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComingSoon comingSoon = db.ComingSoons.Find(id);
            if (comingSoon == null)
            {
                return HttpNotFound();
            }
            return View(comingSoon);
        }

        // GET: ComingSoons/Create
        public ActionResult Create()
        {
            ViewBag.Movie_Id = new SelectList(db.MoviesComingSoons, "Movie_Id", "Tilte");
            return View();
        }

        // POST: ComingSoons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Movie_Id,rating,ReleaseDate")] ComingSoon comingSoon)
        {
            if (ModelState.IsValid)
            {
                db.ComingSoons.Add(comingSoon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Movie_Id = new SelectList(db.MoviesComingSoons, "Movie_Id", "Tilte", comingSoon.Movie_Id);
            return View(comingSoon);
        }

        // GET: ComingSoons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComingSoon comingSoon = db.ComingSoons.Find(id);
            if (comingSoon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Movie_Id = new SelectList(db.MoviesComingSoons, "Movie_Id", "Tilte", comingSoon.Movie_Id);
            return View(comingSoon);
        }

        // POST: ComingSoons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Movie_Id,rating,ReleaseDate")] ComingSoon comingSoon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comingSoon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Movie_Id = new SelectList(db.MoviesComingSoons, "Movie_Id", "Tilte", comingSoon.Movie_Id);
            return View(comingSoon);
        }

        // GET: ComingSoons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComingSoon comingSoon = db.ComingSoons.Find(id);
            if (comingSoon == null)
            {
                return HttpNotFound();
            }
            return View(comingSoon);
        }

        // POST: ComingSoons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComingSoon comingSoon = db.ComingSoons.Find(id);
            db.ComingSoons.Remove(comingSoon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
