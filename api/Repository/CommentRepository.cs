using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context; // this is dependency injection
        }

        public async Task<Comment>? CreateComment(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment>? GetByIdAsync(int id)
        {
            Comment? commentModel = await _context.Comments.FirstOrDefaultAsync(item => item.Id == id);
            if (commentModel == null)
            {
                return null;
            }

            return commentModel;
        }

        public async Task<Comment>? UpdateComment(int id, UpdateCommentRequestDTO commentDto)
        {
            Comment? commentModel = await _context.Comments.FirstOrDefaultAsync(item => item.Id == id);
            if (commentModel == null)
            {
                return null;
            }

            commentModel.Title = commentDto.Title;
            commentModel.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return commentModel;
        }
        public async Task<Comment?> DeleteComment(int id)
        {
            Comment? commentModel = await _context.Comments.FirstOrDefaultAsync(item => item.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Remove(commentModel);
            _context.SaveChanges();

            return commentModel;
        }
    }
}