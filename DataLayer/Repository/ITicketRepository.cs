using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITicketRepository:IDisposable
    {


        Ticket GetTicketByID(int ticketid);

        //IEnums

        IEnumerable<Ticket> GetAllTickets();
        IEnumerable<Ticket> TopTickets(int take = 4);
        IEnumerable<Ticket> TicketsInSlider();
        IEnumerable<Ticket> LastTickets(int take = 4);
        IEnumerable<Ticket> ShowTicketByGroupID(int groupId);
        IEnumerable<Ticket> SerachTicket(string search);


        //...C...R...U...D...

       
        bool InsertTicket(Ticket ticket);
        bool UpdateTicket(Ticket ticket);
        bool DeleteTicket(Ticket ticket);
        bool DeleteTicket(int id);
        void Save();

    }
}
