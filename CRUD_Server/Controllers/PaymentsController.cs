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
    public class PaymentsController : ControllerBase
    {
        ApplicationContext _context;

        public PaymentsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            return _context.Payments.ToList();
        }

        [HttpGet("{id}", Name ="Getpayment")]
        public ActionResult<Payment> GetById(long id)
        {
            var item = _context.Payments.Find(id);
            if (item == null)
                return BadRequest();

            return Ok(item);
        }

        [HttpPost, Route("getuserpayments")]
        public ActionResult<List<Payment>> GetUserPayments(UserAccount item)
        {
            var user = _context.UserAccounts.SingleOrDefault(u => u.SocialNumber == item.SocialNumber);

            if (user == null)
                return BadRequest();

            item.ClientId = user.ClientId;
            return _context.Payments.Where(b => b.ClientId == item.ClientId).ToList();
        }

        [HttpPost]
        public IActionResult Create(Payment item)
        {
            if (!FoundBankAccount(item.Id))
                return BadRequest("This bank account does not exist");

            item.IsPaid = false;
            return CreatedAtRoute("Getpayment", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Payment item)
        {
            var payment = _context.Payments.Find(id);
            //var account = _context.BankAccounts.Where(a => a.Id.Equals(item.BankAccountId));

            //if (payment == null || account == null)
            //    return BadRequest();


            payment.IsPaid = item.IsPaid;

            _context.Payments.Update(payment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.Payments.Find(id);
            if (item == null)
                return NotFound();
            _context.Payments.Remove(item);
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