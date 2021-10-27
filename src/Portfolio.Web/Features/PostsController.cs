namespace Portfolio.Web.Features
{
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Domain.Posts.Models.Posts;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private static readonly List<Post> posts = new List<Post>()
            {
                new Post("testtitle", "testcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontenttestcontent")
            };

        [HttpGet]
        public IEnumerable<Post> Get() => posts;
    }
}
