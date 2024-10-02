using Jobcandidate.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobcandidate.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateService _service;

    public CandidateController(ICandidateService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Send([FromBody] CandiateCreateOrModifyDto dto)
    {
        var response = new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this._service.CreateOrModify(dto)
        };

        return Ok(response);
    }
}
