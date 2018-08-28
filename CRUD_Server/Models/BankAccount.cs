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

        public long ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        [MaxLength(15)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(25)]
        public string AccountClientNumber { get; set; }

        [Required]
        public float Balance { get; set; }

        [Required]
        [MaxLength(15)]
        public string Currency { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(15)]
        public string AccountType { get; set; }

        [Required]
        public bool AccountStatus { get; set; }
    }
}
