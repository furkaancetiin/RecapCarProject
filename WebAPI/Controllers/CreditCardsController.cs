using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {

        ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }       

        [HttpPost("get")]
        public IActionResult Get(string cardNumber, string expireMonthAndYear, string cvc, string cardHolderFullName)
        {
            var result = _creditCardService.Get(cardNumber, expireMonthAndYear, cvc, cardHolderFullName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }             
    }
}
