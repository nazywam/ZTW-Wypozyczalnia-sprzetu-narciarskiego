namespace SkiRent.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("videomixhd_narty.Rented_Items")]
    public partial class RentedItem
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public virtual Item Item { get; set; }

        public virtual Order Order { get; set; }
    }
}
