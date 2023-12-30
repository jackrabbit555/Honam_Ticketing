using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITicketGroupRepository:IDisposable
    {
        //Enums
        IEnumerable<TicketGroup> GetAllTicketGroups();
        IEnumerable<ShowGroupViewModel> GetGroupsForView();


        TicketGroup GetGroupById(int groupId);


        //...C...R...U...D...
        bool InsertGroup(TicketGroup ticketGroup);
        bool UpdateGroup(TicketGroup ticketGroup);
        bool DeleteGroup(TicketGroup ticketGroup);
        bool DeleteGroup(int ticketGroupid);
        void save();

    }
}
