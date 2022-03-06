using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{id}")]
        public Product GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogInformation("GetProduct Action Invoked");
            Console.WriteLine("GetProduct Action Invoked");
            return _context.Products.Find(id);
        }

        [HttpPost]
        public void SaveProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
