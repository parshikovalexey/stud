//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace stud.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class WEIGHTING
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> WEIGHTTIME { get; set; }
        public Nullable<double> WEIGHT { get; set; }
        public Nullable<int> NOTENUMBER { get; set; }
        public string CONTAINERTYPE { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        public string Sync { get; set; }
    
        public virtual NOTE NOTE { get; set; }
    }
}
