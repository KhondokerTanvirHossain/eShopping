using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;
using Catalog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _service;

        public CatalogController(ICatalogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _service.GetProducts());
        }
        
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _service.GetProduct(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            return Ok(await _service.GetProductsByName(name));
        }
        
        
        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            return Ok(await _service.GetProductsByCategory(category));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.Created)]
        public  async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            Product p = await _service.Create(product);
            return CreatedAtRoute("GetProductById", new {id = p.Id}, p);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<Product>> Update([FromBody] Product product)
        {
            if(await _service.Update(product))
                return CreatedAtRoute("GetProductById", new {id = product.Id}, product);
            return NotFound();
        }
        
        [HttpDelete]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public  async Task<ActionResult<Product>> Delete(string id)
        {
            return Ok(await _service.Delete(id)) ;
        }
        
    }
}