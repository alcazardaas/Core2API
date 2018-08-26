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

        [ForeignKey("BankAccount")]
        public long BankAccount { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int MyProperty { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }
    }
}
