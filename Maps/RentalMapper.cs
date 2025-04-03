using System.Runtime.CompilerServices;
using piyoz.uz.DataAccess.Entities;
using piyoz.uz.Dtos.Rental;
using Riok.Mapperly.Abstractions;

namespace piyoz.uz.Maps
{
    [Mapper]
    public partial class RentalMapper
    {

        //[MapProperty(nameof(Rental.Car), nameof(RentalDto.CarInfo))]
        //private string MapCarToCarInfo(Car car) => $"{car.Make} {car.Model} {car.Year}";
        public partial IEnumerable<RentalDto> RentalsToRentalDtos(IEnumerable<Rental> rentals);
        public partial RentalDto RentalToRentalDto(Rental rental);
        public partial Rental  CreateRentalDtoToRental(CreateRentalDto rentalDto);
        public partial void UpdateRentalDtoToRental(UpdateRentalDto rentalDto, Rental rental);

    }
}
