using System;
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

        [HttpGet]
        public ActionResult<List<BankAccount>> GetAll()
        {
            return _context.BankAccounts.ToList();
        }

        [HttpGet("{id}", Name = "Getbankaccount")]
        public ActionResult<BankAccount> GetById(long id)
        {
            var item = _context.BankAccounts.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost, Route("getuseraccounts")]
        public ActionResult<List<BankAccount>> GetUserAccounts(UserAccount item)
        {
            var user = _context.UserAccounts.SingleOrDefault(u => u.SocialNumber == item.SocialNumber);

            if (user == null)
                return BadRequest();

            item.ClientId = user.ClientId;
            return _context.BankAccounts.Where(b => b.ClientId == item.ClientId).ToList();
        }

        [HttpPost]
        public IActionResult Create(BankAccount item)
        {
            bool accNum = false;
            bool cliNum = false;

            var client = _context.Clients.FirstOrDefault(c => c.SocialNumber == item.ClientSocialNumber);
            item.ClientId = client.Id;

            if (!FoundClient(item.ClientId))
                return BadRequest(item.ClientId + " does not exist");

            while (!accNum || !cliNum)
            {
                if (!accNum)
                {
                    if (!FoundAccountNumb(item.AccountNumber = CreateAccountNumb()))
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

            _context.BankAccounts.Add(item);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, BankAccount item)
        {
            var bankAccount = _context.BankAccounts.Find(id);
            if (bankAccount == null)
                return BadRequest();

            bankAccount.Balance = item.Balance;
            bankAccount.AccountStatus = item.AccountStatus;

            _context.BankAccounts.Update(bankAccount);
            _context.SaveChanges();
            return Ok(bankAccount);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.BankAccounts.Find(id);
            if (item == null)
                return BadRequest();
            _context.BankAccounts.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        private bool FoundClient(long Id)
        {
            var item = _context.Clients.Find(Id);

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

        private string CreateAccountNumb()
        {
            Random random = new Random();
            string account = "";
            for (int i = 0; i < 12; i++)
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
