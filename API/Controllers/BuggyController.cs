using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("auth")]
    //Use IActionResult if the return type is not important
    public IActionResult GetAuth()
    {
        return Unauthorized();
    }

    [HttpGet("not-found")]
    //Use IActionResult if the return type is not important
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("server-error")]
    //Use IActionResult if the return type is not important
    public IActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }

    [HttpGet("bad-request")]
    //Use IActionResult if the return type is not important
    public IActionResult GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}
