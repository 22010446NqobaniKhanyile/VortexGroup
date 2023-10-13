using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vortex.Models;

namespace Vortex.Controllers
{
    public class PointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        public ActionResult RewardPic()
        {
            var UserName = System.Web.HttpContext.Current.User.Identity.Name;
            
            

                var point = db.Point.Include(p => p.Report);
            return View(point.ToList().Where(a => a.Email == UserName));

        }
        // GET: Points
        public ActionResult Index()
        {
            var point = db.Point.Include(p => p.Report);
            return View(point.ToList());
        }

        // GET: Points/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Point.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // GET: Points/Create
        public ActionResult Create()
        {
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "Email");
            return View();
        }

        // POST: Points/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PointsID,Point,Reward,ReportID,Email,Date")] Points points)
        {
            if (ModelState.IsValid)
            {
                if (points.Point>=50 & points.Point <100)
				{
                    points.Reward = "key holder";
                }
                else if (points.Point >= 100)
				{
                    points.Reward = "Dut bag";
                }
                Report report = new Report();
                var Email = db.Reports.Where(o => o.ReportID == points.ReportID) .Select(o => o.Email).FirstOrDefault();
                points.Email = Email;
                db.Point.Add(points);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "Email", points.ReportID);
            return View(points);
        }

        // GET: Points/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Point.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "FaultTyp", points.ReportID);
            return View(points);
        }

        // POST: Points/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PointsID,Point,Reward,ReportID,Email,Date")] Points points)
        {
            if (ModelState.IsValid)
            {
                db.Entry(points).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "FaultTyp", points.ReportID);
            return View(points);
        }

        // GET: Points/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Point.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Points points = db.Point.Find(id);
            db.Point.Remove(points);
            db.SaveChanges();
            return RedirectToAction("AdminHomePage", "Home");
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
