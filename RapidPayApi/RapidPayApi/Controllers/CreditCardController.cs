using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Models;
using System.Net;

namespace RapidPayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        public CreditCardController()
        {
        }

        [HttpGet]
        public IActionResult GetBalance()
        {
            //TODO: Find the card and return its balance.
            decimal balance = 0;
            return Ok(balance);
        }

        [HttpGet]
        public IActionResult Create([FromBody] CreditCard card)
        {
            // TODO: Create the Card
            return StatusCode((int)HttpStatusCode.Created, "Card has been created");
        }

        [HttpGet]
        public IActionResult Pay([FromBody] CreditCard card, decimal amount)
        {
            // TODO: Subtract th amout from that card and return new balance
            decimal balance = 0;
            return Ok(balance);
        }
    }
}