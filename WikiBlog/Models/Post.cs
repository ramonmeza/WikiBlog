/*********************/
/*                   */
/*      Post.cs      */
/*  Schema for Post  */
/*                   */
/*********************/

namespace WikiBlog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
