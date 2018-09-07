using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Server.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ClientId { get; set; }
        public Client Client { get; set; }

        public long ProviderId { get; set; }
        public Provider Provider { get; set; }
        
        [MaxLength(20)]
        public string PaymentType { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        [MaxLength(15)]
        public string Currency { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsPaid { get; set; }
    }
}
