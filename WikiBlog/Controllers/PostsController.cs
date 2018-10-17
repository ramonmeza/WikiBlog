using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WikiBlog.Data;

namespace WikiBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostsContext _context;

        public PostsController(PostsContext context)
        {
            _context = context;
        }

        [HttpGet("/posts")]
        public async Task<IActionResult> GetAll()
        {
            return View("Index", await _context.Posts.ToListAsync());
        }

        [HttpGet("/posts/{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            ViewData["post_id"] = id;
            return View("Index", await _context.Posts.ToListAsync());
        }

        [HttpPost("/posts/")]
        public string Post()
        {
            return "posts POST";
        }

        [HttpPut("/posts/{id}")]
        public string Put()
        {
            return "posts PUT";
        }

        [HttpDelete("/posts/{id}")]
        public string Delete()
        {
            return "posts DELETE";
        }
    }
}