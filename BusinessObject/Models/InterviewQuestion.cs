using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public partial class InterviewQuestion
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [Required]
        public string Position { get; set; } = null!;
        [Required]
        public string Experiences { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Answer { get; set; } = null!;
        [Required]
        public DateTime CreateAt { get; set; }
    }
}
