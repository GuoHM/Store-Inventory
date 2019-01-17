namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Request = new HashSet<Request>();
        }

        [StringLength(20)]
        public string OrderID { get; set; }

        [StringLength(20)]
        public string DepartmentID { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public double? TotalPrice { get; set; }

        [Column(TypeName = "image")]
        public byte[] Signature { get; set; }

        [JsonIgnore]
        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Request> Request { get; set; }
    }
}
