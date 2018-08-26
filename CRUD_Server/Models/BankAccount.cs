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

        [Required]
        public Client Client { get; set; }

        public string AccountNumber { get; set; }
        public string AccountClientNumber { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AccountType { get; set; }
        public bool AccountStatus { get; set; }
    }
}
