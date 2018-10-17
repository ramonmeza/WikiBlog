/****************************/
/*                          */
/*    PostsController.cs    */
/*  Routes for Post         */
/*                          */
/****************************/

using Microsoft.AspNetCore.Mvc;

namespace WikiBlog.Controllers
{
    public class PostsController : Controller
    {
        [HttpGet("/posts")]
        public IActionResult GetAll()
        {
            return View("Index");
        }

        [HttpGet("/posts/{id?}")]
        public IActionResult Get(int? id)
        {
            ViewData["post_id"] = id;
            return View("Index");
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