namespace InventoryBusinessLogic.Entity
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Department")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            AspNetUsers2 = new HashSet<AspNetUsers>();
            Order = new HashSet<Order>();
        }

        [StringLength(20)]
        public string DepartmentID { get; set; }

        [StringLength(128)]
        public string DepartmentRep { get; set; }

        [StringLength(128)]
        public string DepartmentHead { get; set; }

        [StringLength(50)]
        public string DepartmentName { get; set; }

        [StringLength(200)]
        public string CollectionPoint { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DepartmentHeadStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DepartmentHeadEndDate { get; set; }
        [JsonIgnore]
        public virtual AspNetUsers AspNetUsers { get; set; }
        [JsonIgnore]
        public virtual AspNetUsers AspNetUsers1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
       [JsonIgnore]
        public virtual ICollection<Order> Order { get; set; }
      //  public string UserID { get; internal set; }
    }
}
