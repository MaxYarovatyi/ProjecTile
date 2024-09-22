using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository repository)
        {
            _commentRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Comment>> GetComment(string id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comment ?? new Comment(id));
        }
        [HttpPost]
        public async Task<ActionResult<Comment>> UpdateComment(string id, Comment comment)
        {
            return await _commentRepository.UpdateComment(id, comment);
        }
    }
}