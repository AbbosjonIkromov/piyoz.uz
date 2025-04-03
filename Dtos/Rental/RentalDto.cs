namespace piyoz.uz.Dtos.Rental
{
    public class RentalDto
    {
        public int Id { get; set; }
        public string CarInfo { get; set; }
        public string CustomerName { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
