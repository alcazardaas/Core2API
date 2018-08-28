using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Server.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string SocialNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        public char Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime DateOfRegistration { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
        public ICollection<FavAccount> FavAccounts { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
