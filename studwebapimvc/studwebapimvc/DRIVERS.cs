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
    
    public partial class DRIVERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DRIVERS()
        {
            this.AUTODRIVERCUSTOMER = new HashSet<AUTODRIVERCUSTOMER>();
            this.NOTES = new HashSet<NOTES>();
        }
    
        public long ID { get; set; }
        public string FULLNAME { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
        public string Sync { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTODRIVERCUSTOMER> AUTODRIVERCUSTOMER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTES> NOTES { get; set; }
    }
}
