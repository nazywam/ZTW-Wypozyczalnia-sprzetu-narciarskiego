namespace SkiRent.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("videomixhd_narty.Items")]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Rented_Items = new HashSet<RentedItem>();
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(255)]
        public string ModelName { get; set; }

        [Required]
        [StringLength(10)]
        public string Size { get; set; }

        [StringLength(1)]
        public string Avaiable { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Purchase_date { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentedItem> Rented_Items { get; set; }
    }
}
