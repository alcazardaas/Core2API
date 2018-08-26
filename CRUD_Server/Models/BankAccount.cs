using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Server.Models
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountClientNumber { get; set; }

        [Required]
        public float Balance { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public bool AccountStatus { get; set; }
    }
}
