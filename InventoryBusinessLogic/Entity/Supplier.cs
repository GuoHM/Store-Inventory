namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Catalogue = new HashSet<Catalogue>();
            Catalogue1 = new HashSet<Catalogue>();
            Catalogue2 = new HashSet<Catalogue>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        [StringLength(20)]
        public string SupplierID { get; set; }

        [StringLength(50)]
        public string SupplierName { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string FaxNumber { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(50)]
        public string GSTNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalogue> Catalogue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalogue> Catalogue1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalogue> Catalogue2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
