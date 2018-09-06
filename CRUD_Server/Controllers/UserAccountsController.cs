using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CRUD_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public UserAccountsController(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<UserAccount>> GetAll()
        {
            return _context.UserAccounts.ToList();
        }

        [HttpPost]
        public IActionResult Create(UserAccount item)
        {
            var client = _context.Clients.Where(b => b.SocialNumber == item.SocialNumber).FirstOrDefault();

            if (client == null)
                return BadRequest("Client  do not exist");

            if (FoundUserAccount(item.SocialNumber))
                return BadRequest("User already exist");

            item.ClientId = client.Id;
            item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);

            _context.UserAccounts.Add(item);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost, Route("login")]
        public IActionResult Login(UserAccount item)
        {
            var account = CheckAccount(item.SocialNumber, item.Password);
            if (account == null)
            {
                return BadRequest("Account or password invalid.");
            }
            else
            {
                item.IsAdmin = account.IsAdmin;
                return BuildToken(item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UserAccount item)
        {
            var account = _context.UserAccounts.Find(id);
            if (account == null)
                return BadRequest("Account do not exist.");

            account.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            account.IsAdmin = item.IsAdmin;

            _context.UserAccounts.Update(account);
            _context.SaveChanges();
            return Ok(account);
        }


        private UserAccount CheckAccount(string socialNumber, string password)
        {
            var account = _context.UserAccounts.SingleOrDefault(a => a.SocialNumber.Equals(socialNumber));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }

        private bool FoundUserAccount(string SocialNumber)
        {
            var item = _context.UserAccounts.SingleOrDefault(c => c.SocialNumber.Equals(SocialNumber));

            if (item == null)
                return false;

            return true;
        }

        private IActionResult BuildToken(UserAccount userAccount)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userAccount.ClientId.ToString()),
                new Claim("userRole", userAccount.IsAdmin.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["The_Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(50);
            var isAdmin = userAccount.IsAdmin;

            //create JWT after create claims key and credentials

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration,
                isAdmin = isAdmin
            });
        }
    }
}