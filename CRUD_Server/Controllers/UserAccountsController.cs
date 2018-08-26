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
            item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            _context.UserAccounts.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }

        [HttpGet]
        public bool Login(int clientId, string password)
        {
            var account = checkAccount(clientId, password);
            if(account == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private UserAccount checkAccount(int clientId, string password)
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
    }
}