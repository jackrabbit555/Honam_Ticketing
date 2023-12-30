using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TicketGroupService:ITicketGroupRepository
    {
        //opennig Context
        private TicketingContext db;
        //dependency Injection by ctor
        public TicketGroupService(TicketingContext context)
        {
            this.db = context;
        }

        //Enums
        public IEnumerable<TicketGroup> GetAllTicketGroups()
        {
           return db.TicketGroups;
        }
        public IEnumerable<ShowGroupViewModel> GetGroupsForView()
        {
            return db.TicketGroups.Select(  g => new ShowGroupViewModel()
            {
                TicketGroupID = g.GroupID,
                GroupTitle = g.GroupTitle,
                PageCount = g.Ticketnav.Count,
            });
        }



        public TicketGroup GetGroupById(int groupId)
        {
            return db.TicketGroups.Find(groupId);
        }



        //...C...R...U...D...

        public bool InsertGroup(TicketGroup ticketGroup)
        {
            try
            { db.TicketGroups.Add(ticketGroup);return true; } 
            catch
            {  return false; }
        }

        public bool UpdateGroup(TicketGroup ticketGroup)
        {
            try
            { db.Entry(ticketGroup).State = EntityState.Modified; return true; }
            catch
            { return false; }
        }

        public bool DeleteGroup(TicketGroup ticketGroup)
        {
            try
            { db.Entry(ticketGroup).State= EntityState.Deleted; return true; }
            catch
            { return false; }
        }

        public bool DeleteGroup(int ticketGroupid)
        {
            try
            {
                var ticketGroup = GetGroupById(ticketGroupid);
                DeleteGroup(ticketGroup);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void save()
        {
             db.SaveChanges();
        }
        public void Dispose()
        {
             db.Dispose();
        }


      
    }
}
