using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDTO(this Comment model)
        {
            return new CommentDto
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                CreatedOn = model.CreatedOn,
                StockID = model.StockID
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentRequestDTO model, int stockId)
        {
            return new Comment
            {
                Title = model.Title,
                Content = model.Content,
                StockID = stockId
            };
        }
    }
}