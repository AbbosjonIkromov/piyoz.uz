namespace piyoz.uz.Dtos.Rental
{
    public class UpdateRentalDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
