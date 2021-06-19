using AutoMapper;
using EventBusRabbitMQ.Events;

namespace Basket.API.Database.Entities
{
    public class BasketMapping: Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}