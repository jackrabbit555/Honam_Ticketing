using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginService:ILoginRepository
    {
        //opennig Context
        private TicketingContext db;
        //dependency Injection by ctor
        public LoginService(TicketingContext context)
        {
           this.db = context;
        }

        public bool IsExistUser(string username, string password)
        {
            return db.adminLogins.Any(u => u.UserName == username && u.Password == password);
        }
    }
}
