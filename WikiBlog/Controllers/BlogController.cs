using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WikiBlog.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet("/")]
        public IActionResult Get()
        {
            return View("Index");
        }
    }
}