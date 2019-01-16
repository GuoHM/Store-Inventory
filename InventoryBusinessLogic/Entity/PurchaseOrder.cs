namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseOrder")]
    public partial class PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            PurchaseItem = new HashSet<PurchaseItem>();
        }

        [StringLength(20)]
        public string PurchaseOrderID { get; set; }

        [StringLength(20)]
        public string SupplierID { get; set; }

        public double? TotalPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseDate { get; set; }

        [StringLength(200)]
        public string DeliverAddress { get; set; }

        [StringLength(128)]
        public string OrderBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedDate { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<PurchaseItem> PurchaseItem { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Supplier Supplier { get; set; }
    }
}
