using System;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Database.Entities;
using Basket.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            return Ok(await _repository
                .GetBasket(userName)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
        {
            return Ok(await _repository
                .UpdateBasket(basket)
            );
        }
        
        [HttpDelete]
        [Route("{userName}")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            return Ok(await _repository
                .DeleteBasket(userName)
            );
        }
    }
}