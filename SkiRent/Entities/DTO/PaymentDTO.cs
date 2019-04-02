using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiRent.Entities.DTO
{
    public class PaymentDTO
    {
        public int ID { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}