//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoginLog
    {
        public int LineId { get; set; }
        public string ClientCompId { get; set; }
        public string ClientCompName { get; set; }
        public string ServerUserName { get; set; }
        public string ServerUserCode { get; set; }
        public Nullable<int> ActTypeCode { get; set; }
        public string ActTypeDesc { get; set; }
        public Nullable<System.DateTime> ActTime { get; set; }
        public byte[] upsize_ts { get; set; }
        public string ComputerName { get; set; }
        public string OutsideIPv4 { get; set; }
        public string InsideIPv4 { get; set; }
    }
}