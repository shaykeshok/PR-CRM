using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TrackActivityManager : IDisposable
    {
        
        public void Dispose()
        {
        }

        public void SetButtonClicked(int id, int button)
        {
            using (DalTrackActivity context = new DalTrackActivity())
            {
                context.SetButtonClicked(id, button);
            }
        }
        public void OpenMail(int id)
        {
            using (DalTrackActivity context = new DalTrackActivity())
            {
                context.OpenMail(id);
            }
        }
        
    }
}
