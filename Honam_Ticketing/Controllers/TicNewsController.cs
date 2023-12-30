using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Honam_Ticketing.Controllers
{
    public class TicNewsController : Controller
    {

        TicketingContext db = new TicketingContext();
        private ITicketGroupRepository _ticketGroupRepository;
        private ITicketRepository _ticketRepository;
        private ITicketCommentRepository _ticketCommentRepository;

        public TicNewsController()
        {
            _ticketGroupRepository = new TicketGroupService(db);
            _ticketRepository = new TicketService(db);
            _ticketCommentRepository = new TicketCommentService(db);
           
        }
        // GET: TicNews
        public ActionResult ShowTicGroups()
        {
            return PartialView(_ticketGroupRepository.GetGroupsForView());
        }


        public ActionResult ShowGruopsInMenu() 
        {
            return PartialView(_ticketGroupRepository.GetAllTicketGroups());
        
        }

        public ActionResult TopTickets() 
        
        {
            return PartialView(_ticketRepository.TopTickets());
        
        }

        public ActionResult LastTickets() 
        {
            return PartialView(_ticketRepository.LastTickets());
        
        }
        [Route("Archive")]
        public ActionResult Archive() 
        {
        return View(_ticketRepository.GetAllTickets());
        }

        [Route("Groups/{id}/{title}")]

        public ActionResult showTicNewsByID(int id ,string title, int pageid = 1) 
        {
            ViewBag.name = title;
            return View(_ticketRepository.ShowTicketByGroupID(id));
        }
        [Route("TicNews/{id}")]
        public ActionResult ShowTickets(int id) 
        {
            var Tic = _ticketRepository.GetTicketByID(id);

            if (Tic == null) { return HttpNotFound(); }
            Tic.Visit += 1;
            _ticketRepository.UpdateTicket(Tic);
            _ticketRepository.Save();
            return View(Tic);

        }

        public ActionResult AddComment(int id, string name, string email, string comment) 
        {
            TicketComment addComment = new TicketComment() 
            { 
            CreateDate = DateTime.Now,
            TicketID = id,
            Comment = comment,
            Email = email,
            Name = name,  
            };
            _ticketCommentRepository.AddComment(addComment);

            return PartialView("ShowComments",_ticketCommentRepository.GetCommentByTicketId(id));
        }

        public ActionResult ShowComments(int id) 
        {
        return PartialView(_ticketCommentRepository.GetCommentByTicketId(id));
        
        }


    }
}