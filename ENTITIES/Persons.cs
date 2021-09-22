using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{

    public class UsrWebEntity
    {
        public int moneln;
        public string email;
        public string username;
        public string userkod;
        public List<KeyValuePair<int, string>> userCompanies;
    }

    public class JBI
    {
        public int Moneln { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Phone { get; set; }
    }

    public class Activity
    {
        public string ActivityTitle { get; set; }
        public int SendingType { get; set; }
        public int Company { get; set; }
        public string[] Buttons { get; set; }
        public bool PersonalRequest { get; set; }
        public string PersonalRequestText { get; set; }
        public int MessagesPerPerson { get; set; }
        public int PeriodBetweenSendingType { get; set; }
        public int PeriodBetweenSending { get; set; }
        public int ButtonToStopSend { get; set; }
        public JBI[] JBIlist { get; set; }
        public TextBox[] TextBoxList { get; set; }
        public bool TrackMail { get; set; }
        public DateTime InsertDate { get; set; }
    }

    public class TextBox
    {
        public int BoxId { get; set; }
        public string Text { get; set; }
        public bool Bold { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
    }

    public class Template
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public int TextBoxNum { get; set; }
    }

}
