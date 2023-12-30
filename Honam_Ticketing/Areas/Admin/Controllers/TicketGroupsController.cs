using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Honam_Ticketing.Areas.Admin.Controllers
{
    [Authorize]
    public class TicketGroupsController : Controller
    {

        //openning context
        private TicketingContext db = new TicketingContext();
        //dependency injection by ctor
        private ITicketGroupRepository _ticketGroupRepository;
        public TicketGroupsController()
        {
            _ticketGroupRepository = new TicketGroupService(db);
        }




        // GET: Admin/TicketGroups
        public ActionResult Index()
        {
            return View(_ticketGroupRepository.GetAllTicketGroups());
        }

        // GET: Admin/TicketGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = _ticketGroupRepository.GetGroupById(id.Value);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticketGroup);
        }

        // GET: Admin/TicketGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/TicketGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] TicketGroup ticketGroup)
        {
            if (ModelState.IsValid)
            {

                _ticketGroupRepository.InsertGroup(ticketGroup);
                _ticketGroupRepository.save();
                //db.TicketGroups.Add(ticketGroup);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(ticketGroup);
        }

        // GET: Admin/TicketGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = _ticketGroupRepository.GetGroupById(id.Value);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticketGroup);
        }

        // POST: Admin/TicketGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle")] TicketGroup ticketGroup)
        {
            if (ModelState.IsValid)
            {
                _ticketGroupRepository.UpdateGroup(ticketGroup);
                _ticketGroupRepository.save();  
                //db.Entry(ticketGroup).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketGroup);
        }

        // GET: Admin/TicketGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketGroup = _ticketGroupRepository.GetGroupById(id.Value);
            if (ticketGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticketGroup);
        }

        // POST: Admin/TicketGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _ticketGroupRepository.DeleteGroup(id);
            _ticketGroupRepository.save();
            //TicketGroup ticketGroup = _ticketGroupRepository.GetGroupById();
            //db.TicketGroups.Remove(ticketGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ticketGroupRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
