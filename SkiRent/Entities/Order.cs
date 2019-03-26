namespace SkiRent.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("videomixhd_narty.Orders")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Rented_Items = new HashSet<RentedItem>();
        }

        public int ID { get; set; }

        public int PaymentID { get; set; }

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? Date_Rented { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? Date_Return { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Payment Payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentedItem> Rented_Items { get; set; }
    }
}
