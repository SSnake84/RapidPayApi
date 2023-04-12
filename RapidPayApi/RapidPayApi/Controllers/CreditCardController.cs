using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Models;
using System.Net;

namespace RapidPayApi.Controllers
{
    [Authorize]
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

        [HttpPost]
        public IActionResult Create([FromBody] CreditCard card)
        {
            // TODO: Create the Card
            return StatusCode((int)HttpStatusCode.Created, "Card has been created");
        }

        [HttpPut("{cardNumber:regex([[0-9]]{{15}})}")]
        public IActionResult Pay([FromRoute] string cardNumber, [FromBody] decimal amount)
        {
            // TODO: Subtract th amout from that card and return new balance
            decimal balance = 0;
            return Ok(balance);
        }
    }
}