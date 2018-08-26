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

        [Required]
        public BankAccount BankAccount { get; set; }

        public string PaymentType { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public int MyProperty { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
