using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Server.Models
{
    public class PayPayment
    {
        public long BankAccountId { get; set; }
        public long ProviderId { get; set; }
        public long ClientId { get; set; }
    }
}
