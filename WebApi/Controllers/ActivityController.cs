using BL;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Text;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActivityController : ApiController
    {
        // POST: api/Activity/InsertActivity/
        [HttpPost]
        [Route("api/Activity/InsertActivity")]
        public CrmResponse InsertActivity([FromBody] Activity activity)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                using (var manager = new ActivityManager())
                {
                    response = manager.InsertActivity(activity);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in InsertActivity function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // GET: api/Activity
        public ActivitiesListResponse GetActivitiesList(int company = 0, string fromDate = "", string toDate = "")
        {
            ActivityFilters filters = new ActivityFilters();
            if (company != 0)
                filters.company = company;

            if (fromDate != "")
                filters.fromDate = fromDate;

            if (toDate != "")
                filters.toDate = toDate;

            ActivitiesListResponse response = new ActivitiesListResponse();

            try
            {
                using (var manager = new ActivityManager())
                {
                    response = manager.GetActivitiesList(filters);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in GetActivitiesList function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }

        // GET: api/Activity/GetTemplates
        [HttpGet]
        [Route("api/Activity/GetTemplates")]
        public TemplateListResponse GetTemplates()
        {


            TemplateListResponse response = new TemplateListResponse();

            try
            {
                using (var manager = new ActivityManager())
                {
                    response = manager.GetTemplates();
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
                response.title = "Error in GetTemplates function";
                response.body = ex.Message;
                response.rc = 99;
            }
            return response;
        }
    }
}
