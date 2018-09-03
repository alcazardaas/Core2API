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
    public class ProvidersController : ControllerBase
    {
        ApplicationContext _context;

        public ProvidersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Provider>> GetAll()
        {
            return _context.Providers.ToList();
        }

        [HttpGet("{id}", Name = "Getprovider")]
        public ActionResult<Provider> GetById(long id)
        {
            var item = _context.Providers.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(Provider item)
        {
            _context.Providers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("Getprovider", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Provider item)
        {
            var provider = _context.Providers.Find(id);

            if (provider == null)
                return BadRequest();

            provider.Name = item.Name;
            provider.LegalNumber = item.LegalNumber;

            _context.Providers.Update(provider);
            _context.SaveChanges();
            return Ok(provider);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.Providers.Find(id);

            if (item == null)
                return BadRequest();

            _context.Providers.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}