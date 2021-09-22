using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class CrmResponse
    {
        public CrmResponse()
        {
            rc = 0;
        }
        public int rc;
        public string desc;
        public string body;
        public string title;
    }

    public class JBIListResponse : CrmResponse
    {
        public List<JBI> JBI;
        public JBIListResponse()
        {
            JBI = new List<JBI>();
        }
    }
    public class JBISpecificResponse : CrmResponse
    {
        public JBI JBI;
    }

    public class ActivitiesListResponse : CrmResponse
    {
        public List<Activity> Activities;

        public ActivitiesListResponse()
        {
            Activities = new List<Activity>();
        }
    }
    public class TemplateListResponse : CrmResponse
    {
        public List<Template> Templates;

        public TemplateListResponse()
        {
            Templates = new List<Template>();
        }
    }
}

   
