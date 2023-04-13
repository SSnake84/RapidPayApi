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
                return BadRequest(new { message = Messages.AUTH_BLANK_CREDENTIALS });

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = Messages.AUTH_WRONG_CREDENTIALS });

            return Ok(user);
        }

        [HttpGet("{cardNumber:regex([[0-9]]{{15}})}")]
        public IActionResult GetBalance(string cardNumber)
        {
            try
            {
                decimal balance = _creditCardService.GetCreditCardBalance(cardNumber);
                return Ok(balance);
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Messages.SYSTEM_EXCEPTION);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreditCard card)
        {
            try 
            {
                if(!_creditCardService.AddCreditCard(card))
                    return BadRequest(Messages.CARD_NUMBER_ALREADY_EXISTS);

                return StatusCode((int)HttpStatusCode.Created, Messages.CARD_CREATED);
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Messages.SYSTEM_EXCEPTION);
            }
        }

        [HttpPut("{cardNumber:regex([[0-9]]{{15}})}")]
        public IActionResult Pay([FromRoute] string cardNumber, [FromBody] decimal amount)
        {
            try
            {
                PaymentResponse res = _creditCardService.Pay(cardNumber, amount);
                return Ok(@$"Old Balance - Amount - Fee = New Balance{Environment.NewLine}{res.OldBalance.ToString("F2")} - {res.Amount.ToString("F2")} - {res.FeeApplied.ToString("F2")} = {res.NewBalance.ToString("F2")}");
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Messages.SYSTEM_EXCEPTION);
            }
        }
    }
}