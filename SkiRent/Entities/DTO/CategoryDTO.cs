using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiRent.Entities.DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public decimal? PricePerDay { get; set; }

        public virtual ICollection<ItemDTO> Items { get; set; }
    }
}