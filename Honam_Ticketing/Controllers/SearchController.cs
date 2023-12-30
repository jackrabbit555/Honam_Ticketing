using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Honam_Ticketing.Controllers
{
    public class SearchController : Controller
    {
        TicketingContext db = new TicketingContext();
        private ITicketRepository _ticketRepository;
        public SearchController()
        {
            _ticketRepository = new TicketService(db);
        }



        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.name = q;
            return View(_ticketRepository.SerachTicket(q));
        }
    }
}