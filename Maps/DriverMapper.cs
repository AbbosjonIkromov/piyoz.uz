using piyoz.uz.DataAccess.Entities;
using piyoz.uz.Dtos.Driver;
using Riok.Mapperly.Abstractions;

namespace piyoz.uz.Maps
{
    [Mapper]
    public partial class DriverMapper
    {
        public partial IEnumerable<DriverDto> DriversToDriverDtos(IEnumerable<Driver> drivers);
        public partial DriverDto DriverToDriverDto(Driver driver);
        public partial Driver CreateDriverDtoToDriver(CreateDriverDto createDriverDto);
        public partial Driver UpdateDriverDtoToDriver(UpdateDriverDto updateDriverDto);
    }
}
