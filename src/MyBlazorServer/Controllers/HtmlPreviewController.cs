using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyBlazorServer.Controllers
{
    [Route("preview")]
    [ApiController]
    public class HtmlPreviewController : ControllerBase
    {
        private const string HtmlContents =
            @"<html>
        <head>
        </head>
            <body>
                <h1>This is a full HTML page rendered from an API call</h1>
                <div>
                 Yes sir.  This is rendered directly to screen from an API controller call.
                </div>
            </body>
       </html>";

        [HttpGet("raw")]
        public IActionResult GetRaw()
        {
            return Ok(HtmlContents);
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return new ContentResult
                {
                    ContentType = "text/html",
                    Content = HtmlContents
                };
        }
    }
}
