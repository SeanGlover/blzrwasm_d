using Microsoft.AspNetCore.Mvc;
using blzrwasm_d.Server.Services;
using blzrwasm_d.Shared;
using System.Net.Mime;

namespace blzrwasm_d.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService service) { productService = service; }

        [HttpGet]
        public ActionResult<List<Product>> Get() { return productService.Get(); }

        [HttpGet("{id}")]
        public ActionResult<Product?> Get(string id)
        {
            Product? Product = productService.Get(id);
            return Product == null ? NotFound($"Product with Id = {id} not found") : Product;
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product Product)
        {
            productService.Create(Product);
            return CreatedAtAction(nameof(Get), new { id = Product.Id }, Product);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(string id, [FromBody] Product Product)
        {
            if (id != Product.Id)
                return BadRequest("Employee ID mismatch");

            Product? ProductToUpdate = productService.Get(id);
            if (ProductToUpdate == null) { return NotFound($"Employee with Id = {id} not found"); }
            productService.Update(id, Product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(string id)
        {
            var existingProduct = productService.Get(id);
            if (existingProduct == null) { return NotFound($"Product with Id = {id} not found"); }
            else { productService.Remove(id); return Ok($"Product with Id = {id} deleted"); }
        }
    }
}
