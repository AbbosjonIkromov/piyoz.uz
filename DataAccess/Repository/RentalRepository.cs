using Microsoft.EntityFrameworkCore;
using piyoz.uz.DataAccess.Entities;

namespace piyoz.uz.DataAccess.Repository
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAll();
        Task<Rental> GetById(int id);
        Task Add(Rental rental);
        void Update(Rental rental);
        void Delete(Rental rental);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;

        public RentalRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rental>> GetAll()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental> GetById(int id)
        {
            return await _context.Rentals.FindAsync(id);
        }

        public async Task Add(Rental rental)
        { 
            await _context.Rentals.AddAsync(rental);
        }

        public void Update(Rental rental)
        {
            _context.Rentals.Update(rental);
        }

        public void Delete(Rental rental)
        {
            _context.Rentals.Remove(rental);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
