using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DataLayer
{
    public class TicketService:ITicketRepository
    {
        //opennig Context
        private TicketingContext db;

        //dependency Injection by ctor
        public TicketService(TicketingContext context)
        {
            this.db = context;
        }





        public Ticket GetTicketByID(int ticketid)
        {
            return db.Tickets.Find(ticketid);
        } 

        //Enums

        public IEnumerable<Ticket> GetAllTickets()
        {
            return db.Tickets;
        }

        public IEnumerable<Ticket> TopTickets(int take=4)
        {
            return db.Tickets.OrderByDescending
                (
                t => t.Visit).Take(take);
        }

        public IEnumerable<Ticket> TicketsInSlider()
        {
            return db.Tickets.Where
                (
                t => t.ShowSlider == true
                );
        }

        public IEnumerable<Ticket> LastTickets(int take)
        {
            return db.Tickets.OrderByDescending
                (
                t => t.CreateDate).Take(take = 4
                );
        }
        public IEnumerable<Ticket> ShowTicketByGroupID(int groupId)
        {
            return db.Tickets.Where
                (
                t => t.GroupID == groupId
                );
        }
        public IEnumerable<Ticket> SerachTicket(string search)
        {
            return db.Tickets.Where(t => t.Title.      Contains(search)     ||
                                         t.Discription.Contains(search)     ||
                                         t.Text.       Contains(search)     )
                                                       .Distinct();
        }




      


        //...C...R...U...D...

        public bool InsertTicket(Ticket ticket)
        {
            try
            {db.Tickets.Add(ticket); return true;}   
            catch { return false;}  
        }

        public bool UpdateTicket(Ticket ticket)
        {
            try
            { db.Entry(ticket).State = EntityState.Modified; ; return true; }
            catch { return false; }
        }


        public bool DeleteTicket(Ticket ticket)
        {
            try
            { db.Entry(ticket).State = EntityState.Deleted; ; return true; }
            catch { return false; }
        }

        public bool DeleteTicket(int id)
        {
            try
            {
               var ticket = GetTicketByID(id);
               DeleteTicket(ticket);   
               return true;

            }
            catch (Exception)
            {

               return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
          db.Dispose();
        }

       

       
       

        
    }
}
