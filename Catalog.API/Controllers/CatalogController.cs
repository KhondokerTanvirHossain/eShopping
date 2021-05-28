using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;
using Catalog.API.Database.Repositories.Interfaces;
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
    }
}