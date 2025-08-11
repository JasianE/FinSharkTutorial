using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(item => item.ToCommentDTO());

            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")] // the :int is a route constraint
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment? comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDTO());

        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDTO commentDto, [FromRoute] int stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist.");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId); //since there is not this, we must provide it
            await _commentRepo.CreateComment(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDTO());

        }
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, UpdateCommentRequestDTO updateDto)
        {
            if (!ModelState.IsValid) // model state comes from the controller base class. (also controller base basically says that this is a controller!)
            {
                return BadRequest(ModelState);
            }

            var commentModel = await _commentRepo.UpdateComment(commentId, updateDto);
            if (commentModel == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.ToCommentDTO());

        }
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var commentModel = await _commentRepo.DeleteComment(commentId);
            if (commentModel == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(commentModel);
        }



    }
}