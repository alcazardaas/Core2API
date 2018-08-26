using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD_Server.Models;

namespace CRUD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ClientsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}", Name = "Getclient")]
        public ActionResult<Client> GetById(long id)
        {
            var item = _context.Clients.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Client item)
        {
            _context.Clients.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Getclient", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Client item)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            client.ClientId = item.ClientId;
            client.FirstName = item.FirstName;
            client.LastName = item.LastName;
            client.Gender = item.Gender;
            client.DateOfBirth = item.DateOfBirth;
            client.DateOfRegistration = item.DateOfRegistration;
            client.PhoneNumber = item.PhoneNumber;
            client.Email = item.Email;
            client.Address1 = item.Address1;
            client.City = item.City;
            client.State = item.State;
            client.Zip = item.Zip;

            _context.Clients.Update(client);
            _context.SaveChanges();
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Ok();
        }

    }
}