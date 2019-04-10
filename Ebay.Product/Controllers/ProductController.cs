using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebay.Product.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Product.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;

        public ProductController(IProductRepository repository)
        {
            this.productRepository = repository;
        }

        // GET api/product
        [HttpGet]
        public ActionResult<IEnumerable<Entities.Product>> Get()
        {
            return Ok(this.productRepository.GetAll());
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public ActionResult<Entities.Product> Get(Guid id)
        {
            return Ok(this.productRepository.GetByID(id));
        }

        // POST api/product
        [HttpPost]
        public void Post([FromBody] Entities.Product value)
        {
            this.productRepository.Add(value);
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Entities.Product value)
        {
            this.productRepository.Update(value);
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this.productRepository.Delete(id);
        }
    }
}
