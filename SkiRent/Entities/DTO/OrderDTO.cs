using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiRent.Entities.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }

        public int PaymentID { get; set; }

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        public DateTime? Date_Rented { get; set; }

        public DateTime? Date_Return { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public virtual CustomerDTO Customer { get; set; }

        public virtual EmployeeDTO Employee { get; set; }

        public virtual PaymentDTO Payment { get; set; }

        public virtual ICollection<RentedItemDTO> Rented_Items { get; set; }
    }
}