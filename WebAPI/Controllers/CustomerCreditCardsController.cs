using Business.Abstract;
using Entities.Concrete;
using Entities.Models;
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
    public class CustomerCreditCardsController : ControllerBase
    {
        ICustomerCreditCardService _customerCreditCardService;

        public CustomerCreditCardsController(ICustomerCreditCardService customerCreditCardService)
        {
            _customerCreditCardService = customerCreditCardService;
        }

       
        [HttpGet("getcustomercreditcardbyid")]
        public IActionResult GetCustomerCreditCardsById(int id)
        {
            var result = _customerCreditCardService.GetCustomerCreditCardsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("savecustomercreditcard")]
        public IActionResult SaveCustomerCreditCard(CustomerCreditCardModel customerCreditCardModel)
        {
            var result = _customerCreditCardService.SaveCustomerCreditCard(customerCreditCardModel);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletedustomercreditcard")]
        public IActionResult DeleteCustomerCreditCard(CustomerCreditCard customerCreditCard)
        {
            var result = _customerCreditCardService.DeleteCustomerCreditCard(customerCreditCard);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
