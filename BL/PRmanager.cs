using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PRmanager : IDisposable
    {
        public PRmanager() { }

        // clear all resource memory leak 
        public void Dispose()
        {
        }
    }
}
