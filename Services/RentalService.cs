using piyoz.uz.DataAccess.Repository;
using piyoz.uz.Dtos.Rental;
using piyoz.uz.Maps;

namespace piyoz.uz.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalDto>> GetAll();
        Task<RentalDto> GetById(int id);
        Task Add(CreateRentalDto rentalDto);
        Task Update(int id, UpdateRentalDto rentalDto);
        Task Delete(int id);

    }
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _repository;

        public RentalService(IRentalRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<RentalDto>> GetAll()
        {
            var rentalMapper = new RentalMapper();
            var rentals = await _repository.GetAll();
            return rentalMapper.RentalsToRentalDtos(rentals);
        }

        public async Task<RentalDto> GetById(int id)
        {
            var rental = await _repository.GetById(id);
            if (rental is null)
            {
                throw new KeyNotFoundException("Rental not found");
            }
            var rentalMapper = new RentalMapper();
            return rentalMapper.RentalToRentalDto(rental);
        }

        public async Task Add(CreateRentalDto rentalDto)
        {
            var rentalMapper = new RentalMapper();
            var rental = rentalMapper.CreateRentalDtoToRental(rentalDto);
            await _repository.Add(rental);
        }

        public async Task Update(int id, UpdateRentalDto rentalDto)
        {
            var rental = await _repository.GetById(id);
            if (rental is null)
            {
                throw new KeyNotFoundException("Rental not found");
            }
            var rentalMapper = new RentalMapper();
            rentalMapper.UpdateRentalDtoToRental(rentalDto, rental);
            _repository.Update(rental);
        }

        public async Task Delete(int id)
        {
            var rental = await _repository.GetById(id);
            if (rental is null)
            {
                throw new KeyNotFoundException("Rental not found");
            }
            _repository.Delete(rental);
        }
    }
}
