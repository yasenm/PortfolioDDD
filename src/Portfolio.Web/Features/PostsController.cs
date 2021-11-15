namespace Portfolio.Web.Features
{
    using Microsoft.AspNetCore.Mvc;
    using Portfolio.Application.Contracts;
    using Portfolio.Domain.Posts.Models.Posts;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IDomainRepository<Post> repository;

        public PostsController(IDomainRepository<Post> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Post> Get() => repository.All();
    }
}
