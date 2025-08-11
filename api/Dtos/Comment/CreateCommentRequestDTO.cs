using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Must have a longer title")]
        [MaxLength(200, ErrorMessage = "Title cannot be over 200 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(20, ErrorMessage = "Content must have over 20 characters")]
        [MaxLength(400, ErrorMessage = "Content must not be longer than 400 characters.")]
        public string Content { get; set; } = string.Empty;

    }
}