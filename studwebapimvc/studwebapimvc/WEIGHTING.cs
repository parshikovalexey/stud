//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace studwebapimvc
{
    using System;
    using System.Collections.Generic;
    
    public partial class WEIGHTING
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> WEIGHTTIME { get; set; }
        public Nullable<double> WEIGHT { get; set; }
        public string CONTAINERTYPE { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        public string Sync { get; set; }
        public Nullable<long> NOTENUMBER { get; set; }
    
        public virtual NOTES NOTES { get; set; }
    }
}
