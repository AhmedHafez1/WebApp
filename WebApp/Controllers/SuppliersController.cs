using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private DataContext context;
        public SuppliersController(DataContext ctx)
        {
            context = ctx;
        }
        [HttpGet("{id}")]
        public async Task<Supplier> GetSupplier(long id)
        {
            var supplier = await context.Suppliers.Include(s => s.Products).FirstAsync(s => s.SupplierId == id);

            foreach (var p in supplier.Products)
            {
                p.Supplier = null;
            }

            return supplier;
        }
    }
}
