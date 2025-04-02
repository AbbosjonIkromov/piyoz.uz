using Microsoft.EntityFrameworkCore;
using piyoz.uz.DataAccess.Entities;

namespace piyoz.uz.DataAccess.Repository
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAll();
        Task<Driver> GetById(int id);
        Task Add(Driver driver);
        void Update(Driver driver);

        void Delete(Driver driver);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;

        public DriverRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Driver>> GetAll()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetById(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task Add(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
        }

        public void Update(Driver driver)
        {
            _context.Drivers.Update(driver);
        }

        public void Delete(Driver driver)
        {
            _context.Drivers.Remove(driver);
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
