using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalUser : IDisposable
    {
        private DbPrCrmEntities _context;
        public DalUser()
        {
            _context = new DbPrCrmEntities();
        }
        void IDisposable.Dispose()
        {
        }

        public LoginResponse Login(long userId, string pass)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                user user = _context.users.Where((r) => (r.UserId == userId && r.Pass == pass)).FirstOrDefault();
                if (user != null)
                {
                    response.rc = 0;
                    response.title = "Login success";
                }
                else
                {
                    response.rc = 1;
                    response.title = "Invalid details";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }
    }
}
