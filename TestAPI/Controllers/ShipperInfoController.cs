using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    public class ShipperInfoController : Controller
    {
        private readonly ShipperInfoContext _context;

        public ShipperInfoController(ShipperInfoContext context)
        {
            _context = context;

            if (_context.ShipperInfoItems.Count() == 0)
            {
                _context.ShipperInfoItems.Add(new ShipperInfo { ShipmentNo = 123456, ItemType = "Hazardous", ItemName = "Sulphuric Acid", Vendor = "UPS", Count = 10, Status = "In-Transit" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<ShipperInfo> GetAll()
        {
            return _context.ShipperInfoItems.ToList();
        }

        [HttpGet("{ShipmentNo}", Name = "GetShipperInfo")]
        public IActionResult GetById(long ShipmentNo)
        {
            var item = _context.ShipperInfoItems.FirstOrDefault(t => t.ShipmentNo == ShipmentNo);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ShipperInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.ShipperInfoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetShipperInfo", new { id = item.ShipmentNo }, item);
        }

        [HttpPut("{ShipmentNo}")]
        public IActionResult Update(long ShipmentNo, [FromBody] ShipperInfo item)
        {
            if (item == null || item.ShipmentNo != ShipmentNo)
            {
                return BadRequest();
            }

            var todo = _context.ShipperInfoItems.FirstOrDefault(t => t.ShipmentNo == ShipmentNo);
            if (todo == null)
            {
                return NotFound();
            }

            todo.ItemType = item.ItemType;
            todo.ItemName = item.ItemName;
            todo.Vendor = item.Vendor;
            todo.Count = item.Count;
            todo.Status = item.Status;

            _context.ShipperInfoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{ShipmentNo}")]
        public IActionResult Delete(long ShipmentNo)
        {
            var todo = _context.ShipperInfoItems.FirstOrDefault(t => t.ShipmentNo == ShipmentNo);
            if (todo == null)
            {
                return NotFound();
            }

            _context.ShipperInfoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
