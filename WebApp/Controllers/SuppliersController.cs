using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [HttpsOnly]
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await context.Suppliers.FindAsync(id);

            if (s == null)
            {
                return BadRequest();
            }

            patchDoc.ApplyTo(s);

            await context.SaveChangesAsync();

            return Ok(s);
        }
    }
}
