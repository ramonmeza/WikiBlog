using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using WikiBlog.Models;

namespace WikiBlog.Data
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
