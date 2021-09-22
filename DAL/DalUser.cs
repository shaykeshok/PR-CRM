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
                    var companies = new List<KeyValuePair<int, string>>();

                    var result = (from usersComp in _context.UsersCompanies
                                  join comps in _context.Companies
                                   on new { usersComp.CompanyId }
                                   equals new { comps.CompanyId }
                                  where usersComp.UserId == user.Moneln
                                  select new
                                  {
                                      companyId = usersComp.CompanyId,
                                      companyName = comps.CompanyName
                                  }).ToList();
                    result.ForEach(item => companies.Add(new KeyValuePair<int, string>(item.companyId, item.companyName)));
                    response.user = new UsrWebEntity();
                    response.user.userCompanies = companies;
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
