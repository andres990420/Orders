
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync() 
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCuntriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Juegos" });
                _context.Categories.Add(new Category { Name = "Accesorios" });
                _context.Categories.Add(new Category { Name = "Zapatos" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCuntriesAsync()
        {
            if (!_context.Countries.Any()) 
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Argentina" });
                _context.Countries.Add(new Country { Name = "Estados Unidos" });
                _context.Countries.Add(new Country { Name = "Chile" });
                _context.Countries.Add(new Country { Name = "Venezuela" });
                _context.Countries.Add(new Country { Name = "Republica Dominicana" });
                _context.Countries.Add(new Country { Name = "Paraguay" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
