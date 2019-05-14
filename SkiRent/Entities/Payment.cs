namespace SkiRent.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("videomixhd_narty.Payments")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            
        }

        public int ID { get; set; }
        public int OrderID { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public decimal ExchangeRate { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Order Order { get; set; }
    }
}
