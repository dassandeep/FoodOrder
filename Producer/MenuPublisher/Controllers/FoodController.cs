using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IFoodPublishMessage _foodPublishMessage;

        public FoodController(ILogger<FoodController> logger, IFoodPublishMessage foodPublishMessage)
        {
            _logger = logger;
            _foodPublishMessage = foodPublishMessage;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Food food)
        {
           await _foodPublishMessage.WriteMessage(food);
           return Created("TransactionId", "Your order is in progress");
        }
    }
}
