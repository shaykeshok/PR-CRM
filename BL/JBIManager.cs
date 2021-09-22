using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Modules;
using DAL;
using ENTITIES;
namespace BL
{
    public class JBIManager : IDisposable
    {

        public JBIListResponse GetAllJBI()
        {
            JBIListResponse response=new JBIListResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.GetAllJBI();

            }


            return response;
        }

        public JBISpecificResponse GetSpecificJBI(int idJBI)
        {
            JBISpecificResponse response = new JBISpecificResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.GetSpecificJBI(idJBI);

            }


            return response;
        }



        public CrmResponse DeleteJBI(int idJBI)
        {
            CrmResponse response = new CrmResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.DeleteJBI(idJBI);

            }


            return response;
        }

        public CrmResponse UpdateJBI(ENTITIES.JBI jbi)
        {
            CrmResponse response = new CrmResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.UpdateJBI(jbi);

            }


            return response;
        }

        public CrmResponse AddNewJbi(ENTITIES.JBI jbi)
        {
            CrmResponse response = new CrmResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.AddNewJbi(jbi);

            }


            return response;
        }



        public void Dispose()
        {
        }

        public JBIListResponse GetFilterJBIlist(int role,string name)
        {
            JBIListResponse response = new JBIListResponse();

            using (DalJBI context = new DalJBI())
            {
                response = context.GetFilterJBIList(role, name);

            }


            return response;
        }
    }
}
