using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class InterviewQuestion
    {
        public string QuestionId { get; set; } = null!;
        public string? Position { get; set; }
        public string? Title { get; set; }
        public string? Answer { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
