using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("{id}", Name = "Getpayment")]
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

        [HttpPost, Route("paypayment")]
        public IActionResult PayPayment(PayPayment paypayment)
        {
            var bankaccount = _context.BankAccounts.Find(paypayment.BankAccountId);
            if (bankaccount == null)
                BadRequest("Account does not exist");

            var client = _context.Clients.SingleOrDefault(c => c.SocialNumber == paypayment.ClientId);
            if (client == null)
                BadRequest("Client does not exist");

            var paythispayment = _context.Payments.SingleOrDefault(p => p.ClientId == client.Id && p.ProviderId == paypayment.ProviderId);

            if (paythispayment == null)
                return BadRequest(paythispayment);

            if (bankaccount.Currency == "Colones" && paythispayment.Currency == "Dolars")
            {
                double v = paythispayment.Amount * 587.699871;
                paythispayment.Amount = (float)v;
            }

            if (bankaccount.Currency == "Dolars" && paythispayment.Currency == "Colones")
            {
                double v = paythispayment.Amount * 0.001702;
                paythispayment.Amount = (float)v;
            }

            if (bankaccount.Balance <= paythispayment.Amount)
                return BadRequest();

            bankaccount.Balance -= paythispayment.Amount;
            paythispayment.Amount = 0;
            paythispayment.IsPaid = true;
            _context.BankAccounts.Update(bankaccount);
            _context.Payments.Update(paythispayment);

            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(Payment item)
        {
            var client = _context.Clients.Find(item.ClientId);
            if (client == null)
                return BadRequest("User doesn't exist");

            var provider = _context.Providers.Find(item.ProviderId);
            if (provider == null)
                return BadRequest("Provider doesn't exist");

            var payment = _context.Payments.SingleOrDefault(p => p.ClientId == client.Id && p.ProviderId == provider.Id);
            if (payment == null)
            {
                item.IsPaid = false;

                _context.Payments.Add(item);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                payment.Amount += item.Amount;
                payment.IsPaid = false;
                payment.DueDate = item.DueDate;
                _context.Payments.Update(payment);
                _context.SaveChanges();
                return Ok();
            }

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