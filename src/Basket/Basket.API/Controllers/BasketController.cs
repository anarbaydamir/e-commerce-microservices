using AutoMapper;
using Basket.API.Entity;
using Basket.API.Repository.Inter;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly EventBusRabbitMQProducer eventBus;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository repository, EventBusRabbitMQProducer eventBus, IMapper mapper)
        {
            this.repository = repository;
            this.eventBus = eventBus;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> get(string userName)
        {
            var basket = await repository.get(userName);
            return Ok(basket ?? new BasketCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basket)
        {
            return Ok(await repository.update(basket));
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> delete(string userName)
        {
            return Ok(await repository.delete(userName));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get total price of the basket
            // remove the basket 
            // send checkout event to rabbitMq 

            var basket = await repository.get(basketCheckout.userName);
            if (basket == null)
                return BadRequest();

            var basketRemoved = await repository.delete(basketCheckout.userName);
            if (!basketRemoved)
                return BadRequest();

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process

            var eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.requestId = Guid.NewGuid();
            eventMessage.totalPrice = basket.totalPrice;

            try
            {
                eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {
                throw;
            }

            return Accepted();
        }
    }
}
