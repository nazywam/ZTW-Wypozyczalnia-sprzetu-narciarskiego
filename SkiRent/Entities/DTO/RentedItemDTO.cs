namespace SkiRent.Entities.DTO
{
    public class RentedItemDTO
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ItemID { get; set; }

        public virtual ItemDTO Item { get; set; }

        public virtual OrderDTO Order { get; set; }
    }
}