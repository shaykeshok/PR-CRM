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
    public class ActivityManager : IDisposable
    {

        public CrmResponse InsertActivity(ENTITIES.Activity newActivity)
        {
            CrmResponse response = new CrmResponse();

            using (DalActivity context = new DalActivity())
            {
                response = context.InsertNewActivity(newActivity);
                if (response.rc > -1)
                {
                    //insert lines to schedule email sending
                    response = context.ScheduleEmails(response.rc);

                }

            }


            return response;
        }

        public ActivitiesListResponse GetActivitiesList(ActivityFilters filters)
        {
            ActivitiesListResponse response = new ActivitiesListResponse();

            using (DalActivity context = new DalActivity())
            {
                response = context.GetActivitiesList(filters);
            }


            return response;
        }

       
        public void Dispose()
        {
        }

        public TemplateListResponse GetTemplates()
        {
            TemplateListResponse response = new TemplateListResponse();

            using (DalActivity context = new DalActivity())
            {
                response = context.GetTemplates();
            }


            return response;
        }
    }
}
