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

        [ForeignKey("BankAccount")]
        public long BankAccount { get; set; }

        [Required]
        public long DestBankAccount { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
