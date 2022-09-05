//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineVaccineSystem.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CenterSlot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterSlot()
        {
            this.AppoimentMasters = new HashSet<AppoimentMaster>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> VaccineId { get; set; }
        public Nullable<int> CenterId { get; set; }
        public Nullable<int> SlotCount { get; set; }
        public string SlotTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppoimentMaster> AppoimentMasters { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual VaccineCenter VaccineCenter { get; set; }
    }
}