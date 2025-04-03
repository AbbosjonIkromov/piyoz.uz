using piyoz.uz.DataAccess.Entities;
using piyoz.uz.Dtos.Car;
using Riok.Mapperly.Abstractions;

namespace piyoz.uz.Maps
{
    [Mapper]
    public partial class CarMapper
    {
        public partial IEnumerable<CarDto> CarsToCarDtos(IEnumerable<Car> cars);
        public partial CarDto CarToCarDto(Car car);
        public partial Car CreateCarDtoToCar(CreateCarDto createCarDto);
        public partial void UpdateCarDtoToCar(UpdateCarDto updateCarDto, Car car);
    }
}
