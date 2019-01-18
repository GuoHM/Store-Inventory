namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseItem")]
    public partial class PurchaseItem
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ItemID { get; set; }

        public int? Quantity { get; set; }

        public virtual Catalogue Catalogue { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
