namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adjustment")]
    public partial class Adjustment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adjustment()
        {
            AdjustmentItem = new HashSet<AdjustmentItem>();
        }

        public int AdjustmentID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(128)]
        public string Supervisor { get; set; }

        public int? TotalPrice { get; set; }

        [StringLength(20)]
        public string AdjustmentStatus { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual AspNetUsers AspNetUsers1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdjustmentItem> AdjustmentItem { get; set; }
    }
}
