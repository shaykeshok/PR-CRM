//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public Nullable<int> SendingType { get; set; }
        public Nullable<int> Company { get; set; }
        public Nullable<bool> PersonalRequest { get; set; }
        public string PersonalRequestText { get; set; }
        public Nullable<int> MessagesPerPerson { get; set; }
        public Nullable<int> PeriodBetweenSendingType { get; set; }
        public Nullable<int> PeriodBetweenSending { get; set; }
        public Nullable<int> ButtonToStopSend { get; set; }
        public Nullable<bool> TrackMail { get; set; }
        public byte[] Upsize_ts { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public string Button3 { get; set; }
    }
}
