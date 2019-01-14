namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdjustmentItem")]
    public partial class AdjustmentItem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string AdjustmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ItemID { get; set; }

        [StringLength(20)]
        public string Quantity { get; set; }

        [StringLength(100)]
        public string Reason { get; set; }

        public virtual Adjustment Adjustment { get; set; }

        public virtual Catalogue Catalogue { get; set; }
    }
}