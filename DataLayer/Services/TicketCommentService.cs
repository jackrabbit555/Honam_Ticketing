using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TicketCommentService:ITicketCommentRepository
    {
        //opennig Context
        private TicketingContext db;
        //dependency Injection by ctor
        public TicketCommentService(TicketingContext context)
        {
            this.db = context;
        }


        public IEnumerable<TicketComment> GetCommentByTicketId(int ticketId)
        {
            return db.TicketComments.Where(c => c.TicketID == ticketId);
        }

        public bool AddComment(TicketComment comment)
        {

            try
            {
                 db.TicketComments.Add(comment);
                 db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }

        public void Dispose()
        {
            db.Dispose();
        }

     
    }
}
