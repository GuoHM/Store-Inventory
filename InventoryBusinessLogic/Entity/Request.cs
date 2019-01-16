namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Request")]
    public partial class Request
    {
        [StringLength(20)]
        public string RequestID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(20)]
        public string OrderID { get; set; }

        [StringLength(20)]
        public string ItemID { get; set; }

        public int? Needed { get; set; }

        public int? Actual { get; set; }

        [StringLength(20)]
        public string RequestStatus { get; set; }

        public DateTime? RequestDate { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Catalogue Catalogue { get; set; }

        public virtual Order Order { get; set; }
    }
}
