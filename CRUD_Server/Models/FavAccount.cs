﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Server.Models
{
    public class FavAccount
    {
        public long ClientId { get; set; }
        public Client Client { get; set; }

        public string FavBankAccount { get; set; }
    }
}
