﻿using System;
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

        [HttpPost]
        public IActionResult Create(UserAccount item)
        {
            var client = _context.Clients.Where(b => b.SocialNumber == item.SocialNumber).FirstOrDefault();

            if (client == null)
                return BadRequest("Client  do not exist");

            if (FoundUserAccount(item.SocialNumber))
                return BadRequest("User already exist");

            item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            _context.UserAccounts.Add(item);
            _context.SaveChanges();

            return BuildToken(item);
        }
        
        [HttpGet("{clientId}/{password}")]
        public IActionResult Login(string socialNumber, string password)
        {
            var account = CheckAccount(socialNumber, password);
            if(account == null)
            {
                return BadRequest("Account or password invalid.");
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
                return BadRequest("Account do not exist.");

            account.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            account.isAdmin = item.isAdmin;

            _context.UserAccounts.Update(account);
            _context.SaveChanges();
            return Ok(account);
        }


        private UserAccount CheckAccount(string socialNumber, string password)
        {
            var account = _context.UserAccounts.SingleOrDefault(a => a.SocialNumber.Equals(socialNumber));
            if(account != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, account.Password))
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
                //new Claim(JwtRegisteredClaimNames.UniqueName, userAccount.ClientId),
                //new Claim("userRole", userAccount.isAdmin),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["The_Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(10);

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
                expiration = expiration
            });
        }
    }
}