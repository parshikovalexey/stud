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
    
    public partial class ORGANIZATIONS
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public string INFO { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        public string LEGALADDRESS { get; set; }
        public string Sync { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> DefaultOrganizations { get; set; }
        public string SystemGuid { get; set; }
        public string WarehouseManager { get; set; }
    }
}
