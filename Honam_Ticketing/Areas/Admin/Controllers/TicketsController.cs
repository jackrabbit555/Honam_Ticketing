using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Honam_Ticketing.Areas.Admin.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private TicketingContext db = new TicketingContext();
        private ITicketRepository _ticketRepository;
        private ITicketGroupRepository _ticketGroupRepository;
        private ITicketCommentRepository _ticketCommentRepository;
        public TicketsController()
        {
            _ticketRepository = new TicketService(db);
            _ticketGroupRepository = new TicketGroupService(db);
            _ticketCommentRepository = new TicketCommentService(db);
        }

        // GET: Admin/Tickets
        public ActionResult Index()
        {

            var tickets = _ticketRepository.GetAllTickets();
            //var tickets = db.Tickets.Include(t => t.TicketGroupnav);
            return View(tickets.ToList());
        }

        // GET: Admin/Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepository.GetTicketByID(id.Value);
            //Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticket);
        }

        // GET: Admin/Tickets/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(_ticketGroupRepository.GetAllTicketGroups(),"GroupID","GroupTitle");
            //ViewBag.GroupID = new SelectList(db.TicketGroups, "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,GroupID,Title,Discription,Text,Visit,ImageName,ShowSlider,CreateDate,Tags")] Ticket ticket,HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                ticket.Visit = 0;
                ticket.CreateDate = DateTime.Now;
                if (imageUpload!=null)
                {
                    ticket.ImageName = Guid.NewGuid()+Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("~/TicketImages/" + ticket.ImageName));
                }

                //db.Tickets.Add(ticket);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                _ticketRepository.InsertTicket(ticket);
                _ticketRepository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.GrupId = new SelectList(_ticketRepository.GetAllTickets(), "GroupID", "GroupTitle", ticket.GroupID);
            //ViewBag.GroupID = new SelectList(db.TicketGroups, "GroupID", "GroupTitle", ticket.GroupID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepository.GetTicketByID(id.Value);
            //Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(_ticketGroupRepository.GetAllTicketGroups(), "GroupID", "GroupTitle", ticket.GroupID);
            //ViewBag.GroupID = new SelectList(db.TicketGroups, "GroupID", "GroupTitle", ticket.GroupID);
            return PartialView(ticket);
        }

        // POST: Admin/Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,GroupID,Title,Discription,Text,Visit,ImageName,ShowSlider,CreateDate,Tags")] Ticket ticket,HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null)
                {
                    if (ticket.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("~/TicketImages/" + ticket.ImageName));
                    }
                    ticket.ImageName = Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("~/TicketImages/" + ticket.ImageName));
                }


               _ticketRepository.UpdateTicket(ticket);
                _ticketRepository.Save();
                //db.Entry(ticket).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(_ticketRepository.GetAllTickets(), "GroupID", "GroupTitle", ticket.GroupID);
            //ViewBag.GroupID = new SelectList(db.TicketGroups, "GroupID", "GroupTitle", ticket.GroupID);
            return View(ticket);
        }

        // GET: Admin/Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = _ticketRepository.GetTicketByID(id.Value);
            //Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticket);
        }

        // POST: Admin/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Ticket ticket = _ticketRepository.GetTicketByID(id);
            //Ticket ticket = db.Tickets.Find(id);
            if (ticket.ImageName!=null)
            {
                System.IO.File.Delete(Server.MapPath("~/TicketImages/" + ticket.ImageName));
            }
            _ticketRepository.DeleteTicket(ticket);
            _ticketRepository.Save();
            //db.Tickets.Remove(ticket);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ticketCommentRepository.Dispose();
                _ticketGroupRepository.Dispose();
                _ticketRepository.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
