using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class ActivityFilters
    {
        public int company;
        public string fromDate;
        public string toDate;
        public ActivityFilters()
        {
            company = 0;
            fromDate = "";
            toDate = "";
        }
    }
}
