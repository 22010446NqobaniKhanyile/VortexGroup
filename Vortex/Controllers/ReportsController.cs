using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vortex.Models;

namespace Vortex.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ChooseUpp()

        {
            return View();
        }
       
        public ActionResult SuccessMessage()


        {
            return View();
        }

            [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Report reportObject)

        {
            string fileName = Path.GetFileNameWithoutExtension(reportObject.ImageFile.FileName);
            string extension = Path.GetExtension(reportObject.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reportObject.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            reportObject.ImageFile.SaveAs(fileName);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Reports.Add(reportObject);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        public PartialViewResult ReportIndex()

        {

            return PartialView(db.Reports.ToList());
        }
        public PartialViewResult NoImagIndex()


        {

            return PartialView(db.Reports.ToList().Where(x => x.ImagePath == null));
        }
        // GET: Reports
        public ActionResult TrackIndex()
        {

            return View(db.Reports.ToList().Where(x => x.ImagePath != null));
        }


        // GET: Reports
        public PartialViewResult Index()
        {

            return PartialView(db.Reports.ToList().Where(x => x.ImagePath != null));
        }
        public ActionResult NullimgIndex()

        {

            return View(db.Reports.ToList().Where(x => x.ImagePath == null));
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }
       
        public ActionResult ReportNoImg()

        {
            return View();
        }
        [HttpPost]
        public ActionResult ReportNoImg([Bind(Include = "ReportTwoID,FaultTyp,FaultDescr,Location,OptionalLocation,ImagePath,Faultprgrss,Name,Surname,Email,Date")] Report report)

        {
            var UserName = System.Web.HttpContext.Current.User.Identity.Name;
            if (ModelState.IsValid)
            {
                report.Email = UserName;
                report.Faultprgrss = "Submitted";
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("SuccessMessage", "Reports");
            }

            return View(report);
        }


        [HttpGet]
        public ActionResult EditImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: ReportTwoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult EditImage(Report report)

        {

            string fileName = Path.GetFileNameWithoutExtension(report.ImageFile.FileName);
            string extension = Path.GetExtension(report.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            report.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            report.ImageFile.SaveAs(fileName);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                
                db.Reports.Add(report);
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminHomePage", "Home");

            }




        }


        [HttpGet]
        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( Report report)
        {
            var UserName = System.Web.HttpContext.Current.User.Identity.Name;
            string fileName = Path.GetFileNameWithoutExtension(report.ImageFile.FileName);
            string extension = Path.GetExtension(report.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
           
            report.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            report.ImageFile.SaveAs(fileName);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                report.Email = UserName;
                report.Faultprgrss = "Submitted";
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("SuccessMessage", "Reports");
            }
            
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,FaultTyp,FaultDescr,Location,OptionalLocation,ImagePath,Faultprgrss,Name,Surname,Email,Date")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminHomePage","Home");
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            return RedirectToAction("AdminHomePage","Home");
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
