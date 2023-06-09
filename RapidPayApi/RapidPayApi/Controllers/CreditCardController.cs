using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Handlers;
using RapidPayApi.Data.Models;
using RapidPayApi.Services;
using System.Net;
using RapidPayApi.Models;

namespace RapidPayApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(IUserService userService, ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            if(model.Username == null || model.Password == null)
                return BadRequest(new { message = Constants.MESSAGE_AUTH_BLANK_CREDENTIALS });

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = Constants.MESSAGE_AUTH_WRONG_CREDENTIALS });

            return Ok(user);
        }

        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            try
            {
                decimal balance = await _creditCardService.GetCreditCardBalanceAsync(cardNumber);
                return Ok(balance.ToString("F2"));
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCardRequest cardRequest)
        {
            try 
            {
                if(!await _creditCardService.AddCreditCardAsync(cardRequest.ToCreditCard()))
                    return BadRequest(Constants.MESSAGE_CARD_NUMBER_ALREADY_EXISTS);

                return StatusCode((int)HttpStatusCode.Created, Constants.MESSAGE_CARD_CREATED);
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }

        [HttpPost("Pay")]
        public async Task<IActionResult> Pay(PayRequest payRequest)
        {
            try
            {
                PaymentResponse res = await _creditCardService.PayAsync(payRequest.CardNumber, payRequest.Amount);
                return Ok(@$"Old Balance - Amount - Fee = New Balance{Environment.NewLine}{res.OldBalance.ToString("F2")} - {res.Amount.ToString("F2")} - {res.FeeApplied.ToString("F2")} = {res.NewBalance.ToString("F2")}");
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }
    }
}