using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment>? GetByIdAsync(int id);

        Task<Comment>? CreateComment(Comment commentModel);

        Task<Comment>? UpdateComment(int id, UpdateCommentRequestDTO commentModel); // we need the id to identify and the model to update the fields.
        Task<Comment>? DeleteComment(int id);
    }
}