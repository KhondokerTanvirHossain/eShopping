using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Database.Entities;
using Basket.API.Database.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producers;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;

        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
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
        
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
                return BadRequest();
            var basketRemoved = _repository.DeleteBasket(basketCheckout.UserName);
            if (!basketRemoved.Result)
                return BadRequest();
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;
            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Accepted();
        }
    }
}