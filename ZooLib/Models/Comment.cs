﻿using System.ComponentModel.DataAnnotations;

namespace ZooLib.Models
{
    public class Comment
    {
        
        public int CommentId { get; set; }

        [Display(Name = "Enter a new Comment:")]
        [Required(ErrorMessage = "Please enter a valid comment (max 255 letters)")]
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string? CommentText { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal? Animal { get; set; }
    }
}
