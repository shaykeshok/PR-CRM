using ENTITIES;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemUtils
{
    public class Utils : IDisposable
    {
        // clear all resource memory leak
        public void Dispose()
        {
        }

        public static void adderr(Exception exc, string func = "", string from = "")
        {

            try
            {
                Error err = new Error();
                err.from = exc.TargetSite.DeclaringType.FullName;
                err.func = exc.TargetSite.Name;
                err.desc = exc.Message;
                // Get stack trace for the exception with source file information
                var st = new StackTrace(exc, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                err.line = frame.GetFileLineNumber();
            }
            catch (Exception e) { }
        }
    }
}
