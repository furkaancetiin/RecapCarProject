using Business.Abstract;
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
    public class FindexPointsController : ControllerBase
    {
        IFindexPointService _findexPointService;

        public FindexPointsController(IFindexPointService findexPointService)
        {
            _findexPointService = findexPointService;
        }

        [HttpGet("getfindexpointbycustomerid")]
        public IActionResult GetFindexPointByCustomerId(int id)
        {
            var result = _findexPointService.GetFindexPointByCustomerId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
