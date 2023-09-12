using DsDelivery.Core.Shared.Dto.Erro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DsDelivery.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public ErrorResponse Error()
    {
        Response.StatusCode = 500;
        var id = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
        return new ErrorResponse(id);
    }
}
