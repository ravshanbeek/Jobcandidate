using Jobcandidate.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobcandidate.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ManualController : ControllerBase
{
    private readonly IManualService _service;

    public ManualController(IManualService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetPreferWay()
    {
        var response = new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = this._service.GetPrefer()
        };

        return Ok(response);
    }
}
