using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Server.Models
{
    public class Transfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public BankAccount BankAccount { get; set; }

        public BankAccount DestBankAccount { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
