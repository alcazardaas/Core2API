using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        ApplicationContext _context;

        public TransfersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Transfer>> GetAll()
        {
            return _context.Transfers.ToList();
        }

        [HttpGet("{id}", Name = "Gettransfer")]
        public ActionResult<Transfer> GetById(long id)
        {
            var item = _context.Transfers.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost, Route("getusertransfers")]
        public ActionResult<List<Transfer>> GetUserTransfers(UserAccount item)
        {
            var user = _context.UserAccounts.SingleOrDefault(u => u.SocialNumber == item.SocialNumber);

            if (user == null)
                return BadRequest();

            item.ClientId = user.ClientId;
            return _context.Transfers.Where(b => b.ClientId == item.ClientId).ToList();
        }

        [HttpPost]
        public IActionResult Create(Transfer item)
        {
            var discAccount = _context.BankAccounts.Find(item.DiscAccount);
            var destAccount = _context.BankAccounts.Find(item.DestBankAccount);

            if (discAccount == null || destAccount == null)
                return BadRequest();

            discAccount.Balance -= item.Amount;
            destAccount.Balance += item.Amount;

            _context.BankAccounts.Update(discAccount);
            _context.BankAccounts.Update(destAccount);
            _context.Transfers.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("Gettransfer", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Transfer item)
        {
            var transfer = _context.Transfers.Find(id);
            if (transfer == null)
                return NotFound();

            transfer.Amount = item.Amount;

            _context.Transfers.Add(transfer);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var transfer = _context.Transfers.Find(id);
            if (transfer == null)
                return NotFound();

            _context.Transfers.Remove(transfer);
            _context.SaveChanges();
            return Ok();
        }
    }
}