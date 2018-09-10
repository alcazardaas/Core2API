using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FavAccountsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public FavAccountsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<FavAccount>> GetAll()
        {
            return _context.FavAccounts.ToList();
        }

        [HttpGet("{id}", Name = "Getfavaccount")]
        public ActionResult<FavAccount> GetById(long id)
        {
            var item = _context.FavAccounts.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        //NO MUY SEGURO DE ESTA PENDEJADA
        [HttpPost]
        public IActionResult Create(FavAccount item)
        {
            _context.FavAccounts.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Getfavaccount", new { id = item.ClientId }, item);
        }

        //NO MUY SEGURO DE ESTA PENDEJADA
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.FavAccounts.Find(id);

            if (item == null)
                return BadRequest();

            _context.FavAccounts.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

    }
}