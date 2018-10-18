using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WikiBlog.Data;
using WikiBlog.Models;

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
            ViewData["post"] = await _context.Posts.FindAsync(id);

            return View("Index", await _context.Posts.ToListAsync());
        }

        [HttpPost("/posts")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([Bind("ID, Title, Author, Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }

            return View(post);
        }
        
        [HttpPut("/posts/{id}")]
        public string Put(int? id)
        {
            return "posts PUT";
        }

        [HttpDelete("/posts/{id}")]
        public string Delete(int? id)
        {
            return "posts DELETE";
        }
    }
}