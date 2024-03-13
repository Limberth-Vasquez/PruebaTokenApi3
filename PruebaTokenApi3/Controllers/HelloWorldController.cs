using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTokenApi3.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class HelloWorldController : Controller
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Hello World");
		}
	}
}
