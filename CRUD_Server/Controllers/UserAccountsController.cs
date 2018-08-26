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
    public class UserAccountsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UserAccountsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(UserAccount item)
        {
            var client = _context.Clients.Where(b => b.ClientId == item.ClientId).FirstOrDefault();

            if (client == null)
                return NotFound("Client do not exist");

            if (FoundUserAccount(item.ClientId))
                return BadRequest("User already exist");

            item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            _context.UserAccounts.Add(item);
            _context.SaveChanges();

            return Ok();
        }
        
        [HttpGet("{clientId}/{password}")]
        public IActionResult Login(string clientId, string password)
        {
            var account = CheckAccount(clientId, password);
            if(account == null)
            {
                return NotFound("Account do not exist.");
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UserAccount item)
        {
            var account = _context.UserAccounts.Find(id);
            if (account == null)
            {
                return NotFound("Account do not exist. You can only modify the password");
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);

            _context.UserAccounts.Update(account);
            _context.SaveChanges();
            return Ok(account);
        }


        private UserAccount CheckAccount(string clientId, string password)
        {
            var account = _context.UserAccounts.SingleOrDefault(a => a.ClientId.Equals(clientId));
            if(account != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }

        private bool FoundUserAccount(string clientId)
        {
            var item = _context.UserAccounts.SingleOrDefault(c => c.ClientId.Equals(clientId));

            if (item == null)
                return false;

            return true;
        }
    }
}