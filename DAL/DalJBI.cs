using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalJBI : IDisposable
    {
        private DbPrCrmEntities _context;
        public DalJBI()
        {
            _context = new DbPrCrmEntities();
        }
        void IDisposable.Dispose()
        {
        }

        public JBIListResponse GetAllJBI()
        {
            JBIListResponse response = new JBIListResponse();
            try
            {
                List<JBI> lst = _context.JBIs.ToList();
                if (lst != null)
                {
                    foreach (JBI item in lst)
                    {
                        response.JBI.Add(ConvertJBI(item));
                    }
                    response.rc = 0;
                    response.desc = "_context.JBIs.ToList()";
                    response.title = "Get list of JBI success";
                }
                else
                {
                    response.rc = 1;
                    response.title = "List is empty";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }

        public JBISpecificResponse GetSpecificJBI(int idJBI)
        {
            JBISpecificResponse response = new JBISpecificResponse();
            try
            {
                JBI one = _context.JBIs.Where((x) => x.Moneln == idJBI).FirstOrDefault();

                if (one != null)
                {
                    response.JBI = ConvertJBI(one);
                    response.rc = 0;
                    response.title = "Get one JBI success";
                }
                else
                {
                    response.rc = 1;
                    response.title = "Id is not found";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }

        public JBIListResponse GetFilterJBIList(int role, string name)
        {
            JBIListResponse response = new JBIListResponse();
            try
            {
                List<JBI> lst = _context.JBIs.ToList();
                //List<JBI> lst = _context.JBIs.Where((x) => x.Moneln == idJBI).ToList();
                if (lst != null)
                {
                    foreach (JBI item in lst)
                    {
                        response.JBI.Add(ConvertJBI(item));
                    }
                    response.rc = 0;
                    response.desc = "_context.JBIs.ToList()";
                    response.title = "Get list of JBI success";
                }
                else
                {
                    response.rc = 1;
                    response.title = "List is empty";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }



        public CrmResponse DeleteJBI(int idJBI)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                JBI one = _context.JBIs.Where((x) => x.Moneln == idJBI).FirstOrDefault();
                if (one != null)
                {
                    _context.JBIs.Remove(one);
                    _context.SaveChanges();
                    response.rc = 0;
                    response.title = "Delete one JBI success";
                }
                else
                {
                    response.rc = 1;
                    response.title = "Id is not found";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }

        public CrmResponse UpdateJBI(ENTITIES.JBI jbi)
        {
            CrmResponse response = new CrmResponse();
            try
            {

                JBI updatedRow = _context.JBIs.Where((x) => x.Moneln == jbi.Moneln).FirstOrDefault();
                if (updatedRow != null)
                {
                    Type myType = updatedRow.GetType();
                    PropertyInfo[] props = myType.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        //object oneKey = prop.Name;
                        //object onevalue = prop.GetValue(updatedRow, new object[] { });

                        object jbiValue = jbi.GetType().GetProperty(prop.Name).GetValue(jbi);
                        updatedRow.GetType().GetProperty(prop.Name).SetValue(updatedRow, jbiValue);
                    }

                    _context.SaveChanges();

                }
                else
                {
                    response.rc = 1;
                    response.title = "Id is not found";
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }

        public CrmResponse AddNewJbi(ENTITIES.JBI jbi)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                JBI newJbi = new JBI();
                Type myType = newJbi.GetType();
                PropertyInfo[] props = myType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object jbiValue = jbi.GetType().GetProperty(prop.Name).GetValue(jbi);
                    newJbi.GetType().GetProperty(prop.Name).SetValue(newJbi, jbiValue);
                }
                _context.JBIs.Add(newJbi);
                _context.SaveChanges();
                jbi.Moneln= _context.JBIs.Max().Moneln;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
            }

            return response;
        }

        private ENTITIES.JBI ConvertJBI(JBI item)
        {
            ENTITIES.JBI response = new ENTITIES.JBI();
            response.Moneln = item.Moneln;
            response.FirstName = item.FirstName;
            response.LastName = item.LastName;
            response.Phone = item.Phone;
            response.Facebook = item.Facebook;
            response.Linkedin = item.Linkedin;
            response.Email = item.Email;
            return response;

        }
    }
}
