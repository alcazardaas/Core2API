﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        ApplicationContext _context;

        public BankAccountsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<BankAccount>> GetAll()
        {
            return _context.BankAccounts.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="Getbankaccount")]
        public ActionResult<BankAccount> GetById(long id)
        {
            var item = _context.BankAccounts.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create(BankAccount item)
        {
            bool accNum = false;
            bool cliNum = false;

            while(!accNum || !cliNum)
            {
                if (!accNum)
                {
                    if(!FoundAccountNumb(item.AccountNumber = CreateAccountNumb()))
                    {
                        accNum = true;
                    }
                }
                if (!cliNum)
                {
                    if (!FoundAccountClientNumb(item.AccountClientNumber = CreateAccountClientNum()))
                    {
                        cliNum = true;
                    }
                }
            }

            if(!FoundClient(item.ClientId))
                return NotFound(item.ClientId);

            _context.BankAccounts.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Getbankaccount", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, BankAccount item)
        {
            var bankAccount = _context.BankAccounts.Find(id);
            if (bankAccount == null)
                return NotFound();

            bankAccount.Balance = item.Balance;
            bankAccount.AccountStatus = item.AccountStatus;

            _context.BankAccounts.Update(bankAccount);
            _context.SaveChanges();
            return Ok(bankAccount);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.BankAccounts.Find(id);
            if (item == null)
                return NotFound();
            _context.BankAccounts.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        private bool FoundClient(string clientId)
        {
            var item = _context.Clients.Where(b => b.ClientId == clientId).FirstOrDefault();

            if (item == null)
                return false;

            return true;
        }

        private bool FoundAccountNumb(string accountNumber)
        {
            var item = _context.BankAccounts.Where(b => b.AccountNumber == accountNumber).FirstOrDefault();

            if (item == null)
                return false;

            return true;
        }

        private bool FoundAccountClientNumb(string clientAccNumb)
        {
            var item = _context.BankAccounts.Where(b => b.AccountClientNumber == clientAccNumb).FirstOrDefault();

            if (item == null)
                return false;

            return true;
        }

        private string CreateAccountNumb ()
        {
            Random random = new Random();
            string account = "";
            for(int i = 0; i < 12; i++)
            {
                account += random.Next(0, 10).ToString();
            }

            return account;
        }

        private string CreateAccountClientNum()
        {
            Random random = new Random();
            string account = "";
            for (int i = 0; i < 19; i++)
            {
                account += random.Next(0, 10).ToString();
            }

            return account;
        }
    }
}
