using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TicketingContext:DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketGroup> TicketGroups { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet <AdminLogin> adminLogins { get; set; }

    }
}
