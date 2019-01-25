namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("AdjustmentItem")]
    public partial class AdjustmentItem
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdjustmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ItemID { get; set; }

        [StringLength(20)]
        public string Quantity { get; set; }

        [StringLength(100)]
        public string Reason { get; set; }
        
        [JsonIgnore]
        public virtual Adjustment Adjustment { get; set; }

        public virtual Catalogue Catalogue { get; set; }
    }
}
