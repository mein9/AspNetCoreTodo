using System;
using System.ComponentModel.DataAnnotations;
namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public string? UserId { get; set; }

        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public string? Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}