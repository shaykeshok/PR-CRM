using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PRService
{
    public partial class PRservice : ServiceBase
    {
        Timer timer = new Timer();
        private DbPrCrmEntities _context;

        public PRservice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            WriteToFile("start service");
            _context = new DbPrCrmEntities();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 30000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("stop service");
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            List<EmailsQueue> lst = _context.EmailsQueues.Where((x) => x.SendDt <= now && !x.Done).ToList();

            if (!(lst is null))
            {
                foreach (EmailsQueue item in lst)
                {
                    Activity activity = _context.Activities.Where(x => x.ActivityId == item.ActivityId).FirstOrDefault();
                    if (!(activity is null))
                    {
                        List<TextBox> textBoxes = _context.TextBoxes.Where(x => x.ActivityId == item.ActivityId).ToList();

                        string title = activity.ActivityTitle;
                        JBI jbi = _context.JBIs.Where(x => x.Moneln == item.ActivityJBI).FirstOrDefault();
                        if (jbi.Email != "")
                        {
                            int guid = InsertNewEmail(activity.ActivityId, jbi.Moneln);
                            string body = buildHTML(textBoxes, activity.Button1, activity.Button2, activity.Button3, guid);
                            Emails emails = new Emails(jbi.Email, title, body);
                            bool isSend = emails.sendMail();
                            if (isSend)
                            {
                                UpdateEmailSend(guid, item.LineId);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateEmailSend(int EmailsSendLineId, int EmailsQueueLineId)
        {
            try
            {
                EmailsSend row = _context.EmailsSends.Where(x => x.LineId == EmailsSendLineId).FirstOrDefault();
                row.SendDt = DateTime.Now;
                EmailsQueue item = _context.EmailsQueues.Where(x => x.LineId == EmailsQueueLineId).FirstOrDefault();
                item.Done = true;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteToFile("EX-UpdateEmailSend: " + ex.Message);
            }

        }

        private int InsertNewEmail(int ActivityId, int ActivityJBI)
        {
            EmailsSend newObj;
            try
            {
                EmailsSend row = new EmailsSend();
                row.ActivityId = ActivityId;
                row.ActivityJBI = ActivityJBI;
                row.ClickedAnswer = 0;
                row.IsOpen = false;
                row.SendDt = new DateTime(2000, 1, 1, 0, 0, 0);
                newObj = _context.EmailsSends.Add(row);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                WriteToFile("EX-InsertNewEmail: " + ex.Message + ex.InnerException.Message);
                throw;
            }

            return newObj.LineId;
        }

        //deal with design: https://email.uplers.com/blog/step-step-guide-create-html-email/
        //deal with see images: https://stackoverflow.com/questions/51471607/email-thumbnail-url-changed-to-googleusercontent-com-in-gmail
        private string buildHTML(List<TextBox> textBoxes, string button1, string button2, string button3, int guid)
        {
            string HTML = "<!DOCTYPE html><html><head>";
            HTML += "<style>.myButton {box-shadow:inset 0px 1px 0px 0px #ffffff;background:linear-gradient(to bottom, #ffffff 5%, #f6f6f6 100%);background-color:#ffffff;border-radius:6px;border:1px solid #dcdcdc;display:inline-block;cursor:pointer;color:#666666;font-family:Arial;font-size:15px;font-weight:bold;padding:6px 24px;text-decoration:none;text-shadow:0px 1px 0px #ffffff;}.myButton:hover {background:linear-gradient(to bottom, #f6f6f6 5%, #ffffff 100%);background-color:#f6f6f6;}.myButton:active {position:relative;top:1px;}</style>";
            HTML += "</head><body>";
            HTML += "<div><img src='http://localhost:61442/api/TrackActivity/openMail/" + guid + "' width='1' height='1' /></div>";
            //HTML += "<div><img src='https://images.unsplash.com/photo-1453728013993-6d66e9c9123a?ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dmlld3xlbnwwfHwwfHw%3D&ixlib=rb-1.2.1&w=1000&q=80' width='100' height='100' /></div>";
            HTML += "<div>";
            foreach (TextBox item in textBoxes)
            {
                HTML += "<p style='";
                if (item.Bold ?? false)
                {
                    HTML += "font-weight:bold;";
                }
                HTML += "font-size:" + item.FontSize + ";";
                HTML += "color:" + item.Color + ";";
                HTML += "'>"; ;
                HTML += item.Text;
                HTML += "</p>";
                HTML += "</b>";
            }

            HTML += "</div>";

            if (button1 != "" || button2 != "" || button3 != "")
            {
                HTML += "<div style='display:flex;justify-content:space-evenly'>";
                if (button1 != "")
                    HTML += "<div><button type='button' class='myButton'><a href='http://localhost:61442/api/TrackActivity/ButtonClicked/" + guid + "/1'>" + button1 + "</a></button></div>";
                if (button2 != "")
                    HTML += "<div><button type='button' class='myButton'><a href='http://localhost:61442/api/TrackActivity/ButtonClicked/" + guid + "/2'>" + button2 + "</a></button></div>";
                if (button3 != "")
                    HTML += "<div><button type='button' class='myButton'><a href='http://localhost:61442/api/TrackActivity/ButtonClicked/" + guid + "/3'>" + button3 + "</a></button></div>";
                HTML += "</div></body></html>";
            }
            return HTML;
        }
    }
}
