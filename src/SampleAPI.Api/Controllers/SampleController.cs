using Microsoft.AspNetCore.Mvc;

namespace CopenhagenSoftware.Api.Controllers
{
    public sealed class SampleController : ControllerBase
    {
        public ILogger<SampleController> Logger { get; }

        public SampleController(ILogger<SampleController> logger)
        {
            Logger = logger;
        }

        [HttpGet("/")]
        public IActionResult Get()
        {
            Logger.LogInformation(
                "What if we structured our logs? {@answer}",
                new { Response = "It would be good" });

            return Ok("It's ok");
        }
    }
}