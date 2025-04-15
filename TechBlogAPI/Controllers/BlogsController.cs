using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechBlog.Application.DTOs;
using TechBlog.Application.Interfaces;

namespace TechBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAll(CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetAllAsync(cancellationToken);
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var blog = await _blogService.GetByIdAsync(id, cancellationToken);
            return Ok(blog);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<BlogDto>> Create([FromBody] CreateBlogDto createDto, CancellationToken cancellationToken)
        {
            var blog = await _blogService.CreateAsync(createDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlogDto updateDto, CancellationToken cancellationToken)
        {
            await _blogService.UpdateAsync(id, updateDto, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _blogService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new KeyNotFoundException("This is a test not found error.");
        }
    }
}
