using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Footstep.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TesteController : ControllerBase
{
    [HttpPost]
    public ActionResult Post()
    {
        return Ok();
    }
}
