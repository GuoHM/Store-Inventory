namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Request")]
    public partial class Request
    {
        public int RequestID { get; set; }

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

        [StringLength(500)]
        public string Remarks { get; set; }
        [JsonIgnore]
        public virtual AspNetUsers AspNetUsers { get; set; }
        [JsonIgnore]
        public virtual Catalogue Catalogue { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
