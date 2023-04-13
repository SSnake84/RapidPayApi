using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Handlers;
using RapidPayApi.Models;
using RapidPayApi.Services;
using System.Net;

namespace RapidPayApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(IUserService userService, ICreditCardService creditCardRepo)
        {
            _creditCardService = creditCardRepo;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            if(model.Username == null || model.Password == null)
                return BadRequest(new { message = Messages.BLANK_CREDENTIALS });

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = Messages.WRONG_CREDENTIALS });

            return Ok(user);
        }

        [HttpGet("{cardNumber:regex([[0-9]]{{15}})}")]
        public IActionResult GetBalance(string cardNumber)
        {
            decimal balance = _creditCardService.GetCreditCardBalance(cardNumber);
            return Ok(balance);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreditCard card)
        {
            _creditCardService.AddCreditCard(card);
            return StatusCode((int)HttpStatusCode.Created, "Card has been created");
        }

        [HttpPut("{cardNumber:regex([[0-9]]{{15}})}")]
        public IActionResult Pay([FromRoute] string cardNumber, [FromBody] decimal amount)
        {
            decimal newBalance = _creditCardService.Pay(cardNumber, amount);
            return Ok(newBalance);
        }
    }
}