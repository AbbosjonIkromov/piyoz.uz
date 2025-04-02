using piyoz.uz.DataAccess.Repository;
using piyoz.uz.Dtos.Driver;
using piyoz.uz.Maps;

namespace piyoz.uz.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAll();
        Task<DriverDto> GetById(int id);
        Task Add(CreateDriverDto createDriverDto);
        Task Update(int id, UpdateDriverDto updateDriverDto);
        Task Delete(int id);
    }
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repository;

        public DriverService(IDriverRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<DriverDto>> GetAll()
        {
            var driverMapper = new DriverMapper();
            return driverMapper.DriversToDriverDtos(await _repository.GetAll());
        }

        public async Task<DriverDto> GetById(int id)
        {
            var driver = await _repository.GetById(id);
            if (driver is null)
            {
                throw new KeyNotFoundException("Driver not found");
            }
            var driverMapper = new DriverMapper();
            return driverMapper.DriverToDriverDto(driver);
        }

        public async Task Add(CreateDriverDto createDriverDto)
        {
            var driverMapper = new DriverMapper();
            var driver = driverMapper.CreateDriverDtoToDriver(createDriverDto);
            await _repository.Add(driver);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateDriverDto updateDriverDto)
        {
            var driver = await _repository.GetById(id);
            if (driver is null)
            {
                throw new KeyNotFoundException("Driver not found");
            }
            var driverMapper = new DriverMapper();
            driver = driverMapper.UpdateDriverDtoToDriver(updateDriverDto);
            _repository.Update(driver);
            await _repository.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var driver = await _repository.GetById(id);
            if (driver is null)
            {
                throw new KeyNotFoundException("Driver not found");
            }
            _repository.Delete(driver);
            await _repository.SaveChangesAsync();
        }
    }
}
