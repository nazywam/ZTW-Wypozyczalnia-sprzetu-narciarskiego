using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiRent.Entities.DTO
{
    public class CustomerDTO
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentyficationNumber { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}