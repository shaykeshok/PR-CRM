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
    
    public partial class EmailsQueue
    {
        public int LineId { get; set; }
        public int ActivityId { get; set; }
        public System.DateTime SendDt { get; set; }
        public int ActivityJBI { get; set; }
        public bool Done { get; set; }
    }
}
