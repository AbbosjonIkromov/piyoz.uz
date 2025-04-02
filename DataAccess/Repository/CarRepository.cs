using Microsoft.EntityFrameworkCore;
using piyoz.uz.DataAccess.Entities;

namespace piyoz.uz.DataAccess.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetById(int id);
        Task Add(Car car);
        void Update(Car car);
        void Delete(Car car);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task Add(Car car)
        {
            await _context.Cars.AddAsync(car);
        }

        public void Update(Car car)
        {
            _context.Update(car);
        }

        public void Delete(Car car)
        {
            _context.Cars.Remove(car);
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
