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
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(Payment item)
        {
            if (!FoundBankAccount(item.BankAccount))
                return NotFound("This bank account does not exist");

            return CreatedAtRoute("Getpayment", new { id = item.Id }, item);

        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Payment item)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
                return NotFound();

            payment.PaymentStatus = item.PaymentStatus;

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