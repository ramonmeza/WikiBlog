using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using WikiBlog.Data;
using WikiBlog.Models;

namespace WikiBlog.Controllers
{
    [Route("[controller]")]
    public class PostsController : Controller
    {
        private readonly PostsContext _context;

        public PostsController(PostsContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return View("Index", await _context.Posts.ToListAsync());
        }
        
        [HttpGet("{id?}"), ActionName("Get")]
        public async Task<IActionResult> Get(int? id)
        {
            ViewData["post_id"] = id;
            ViewData["post"] = await _context.Posts.FindAsync(id);

            return View("Index", await _context.Posts.ToListAsync());
        }

        [HttpPost, ActionName("Post")]
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

        [HttpPost("{id}"), ActionName("Put")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Put(int? id, [Bind("ID, Title, Author, Content")] Post post)
        {
            if (id != post.ID)
                return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAll));
            }

            return View(post);
        }

        // This is NOT RESTful - Todo: MAKE IT RESTFUL!
        [HttpPost("{id}/Delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var post = await _context.Posts.FindAsync(id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));
        }
    }
}