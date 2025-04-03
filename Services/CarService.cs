using piyoz.uz.DataAccess.Entities;
using piyoz.uz.DataAccess.Repository;
using piyoz.uz.Dtos.Car;
using piyoz.uz.Maps;

namespace piyoz.uz.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAll();
        Task<CarDto> GetById(int id);
        Task Add(CreateCarDto createCarDto);
        Task Update(int id, UpdateCarDto createCarDto);
        Task Delete(int id);
    }
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            var carMapper = new CarMapper();
            var cars = await _repository.GetAll();
            return carMapper.CarsToCarDtos(cars);
        }

        public async Task<CarDto> GetById(int id)
        {
            var carMapper = new CarMapper();
            var car = await _repository.GetById(id);
            if (car is null)
            {
                throw new Exception("Car not found");
            }
            return  carMapper.CarToCarDto(car);
        }

        public async Task Add(CreateCarDto createCarDto)
        {
            var carMapper = new CarMapper();
            var car = carMapper.CreateCarDtoToCar(createCarDto);

            await _repository.Add(car);
            await _repository.SaveChangesAsync();
        }

        public  async Task Update(int id, UpdateCarDto updateCarDto)
        {
            var car = await _repository.GetById(id);
            if (car is null)
            {
                throw new KeyNotFoundException("Car not found");
            }
            var carMapper = new CarMapper();
            carMapper.UpdateCarDtoToCar(updateCarDto, car);
            _repository.Update(car);
            await _repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var car = await _repository.GetById(id);
            if (car is null)
            {
                throw new KeyNotFoundException("Car not found");
            }

            _repository.Delete(car);
            await _repository.SaveChangesAsync();
        }
    }
}
