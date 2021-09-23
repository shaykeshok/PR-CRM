using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalTrackActivity : IDisposable
    {
        private DbPrCrmEntities _context;
        public DalTrackActivity()
        {
            _context = new DbPrCrmEntities();
        }




        public void SetButtonClicked(int id, int button)
        {
            try
            {
                EmailsSend editRow = _context.EmailsSends.Where(x => x.LineId == id).FirstOrDefault();
                if (editRow != null)
                {
                    editRow.IsOpen = true;
                    editRow.ClickedAnswer = button;
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void OpenMail(int id)
        {
            try
            {
                EmailsSend editRow = _context.EmailsSends.Where(x => x.LineId == id).FirstOrDefault();
                if (editRow != null)
                {
                    editRow.IsOpen = true;
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
        }
    }
}
