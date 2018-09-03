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

        [HttpGet("{id}", Name ="Gettransfer")]
        public ActionResult<Transfer> GetById(long id)
        {
            var item = _context.Transfers.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(Transfer item)
        {
            //if (!FoundBankAccount(item.BankAccount))
            //    return NotFound();

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

        private bool FoundBankAccount(long bankAccount)
        {
            var item = _context.BankAccounts.Find(bankAccount);
            if (item == null)
                return false;

            return true;
        }
    }
}