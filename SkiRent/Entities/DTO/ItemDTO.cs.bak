using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiRent.Entities.DTO
{
    public class ItemDTO
    {
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

        public DateTime? Purchase_date { get; set; }

        public virtual CategoryDTO Category { get; set; }

        public virtual ICollection<RentedItemDTO> Rented_Items { get; set; }
    }
}