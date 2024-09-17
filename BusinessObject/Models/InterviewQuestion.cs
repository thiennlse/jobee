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
        public string? Position { get; set; }
        [Required]
        public string Experiences {  get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Answer { get; set; }
        [Required]
        public DateTime? CreateAt { get; set; }
    }
}
