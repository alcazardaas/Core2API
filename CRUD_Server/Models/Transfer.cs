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

        public long ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public long DiscAccount { get; set; }

        [Required]
        public long DestBankAccount { get; set; }

        [Required]
        public float Amount { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
