using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Honam_Ticketing.Controllers
{
    public class HomeController : Controller
    {
        TicketingContext db = new TicketingContext();
        private ITicketRepository _ticketRepository;
        
        public HomeController()
        {
            _ticketRepository = new TicketService(db);
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();

        }


        public ActionResult slider() 
        {
            return PartialView(_ticketRepository.TicketsInSlider());
        

        }




     
    }
}