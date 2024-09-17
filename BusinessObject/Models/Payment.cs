using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class Payment
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public DateTime? PaymentDate { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
        [Required]
        public string? Status { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
