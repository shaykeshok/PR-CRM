using BL;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JBIController : ApiController
    {

        // GET: api/JBI
        public JBIListResponse GetAllJBI()
        {
            JBIListResponse response = new JBIListResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.GetAllJBI();
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in GetAllJBI function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // GET: api/JBI/5
        public JBISpecificResponse GetSpecificJBI(int idJBI)
        {
            JBISpecificResponse response = new JBISpecificResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.GetSpecificJBI(idJBI);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in GetSpecificJBI function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // GET: api/JBI/GetFilterJBIList
        [HttpGet]
        [Route("api/JBI/GetFilterJBIList")]
        public JBIListResponse GetFilterJBIList(int role = 0, string name = "")
        {
            JBIListResponse response = new JBIListResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.GetFilterJBIlist(role, name);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in GetFilterJBIlist function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // GET: api/JBI/DeleteJBI/5
        [HttpGet]
        [Route("api/JBI/DeleteJBI/{idJBI}")]
        public CrmResponse DeleteJBI(int idJBI)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.DeleteJBI(idJBI);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in DeleteJBI function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // POST: api/JBI/UpdateJBI/
        [HttpPost]
        [Route("api/JBI/UpdateJBI")]
        public CrmResponse UpdateJBI([FromBody] JBI request)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.UpdateJBI(request);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in DeleteJBI function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // POST: api/JBI/AddNewJbi/
        [HttpPost]
        [Route("api/JBI/AddNewJbi")]
        public CrmResponse AddNewJbi([FromBody] JBI request)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                using (var manager = new JBIManager())
                {
                    response = manager.AddNewJbi(request);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in DeleteJBI function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }
    }
}
