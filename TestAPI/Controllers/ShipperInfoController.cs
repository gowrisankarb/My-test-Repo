using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class ShipperInfoController : Controller
    {
        private readonly ShipperInfoContext _context;

        public ShipperInfoController(ShipperInfoContext context)
        {
            _context = context;

            if (_context.ShipperInfoItems.Count() == 0)
            {
                _context.ShipperInfoItems.Add(new ShipperInfo { shipmentNumber = 123456, itemTyp = "Hazardous", itemNam = "Sulphuric Acid", vendorName = "UPS", itemCnt = 10, status = "In-Transit" });
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
            var item = _context.ShipperInfoItems.FirstOrDefault(t => t.shipmentNumber == ShipmentNo);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //[Route("~/api/AddShipper")]
        [HttpPost]
        //[Route("api/[PostShipperInfo]")]
        public IActionResult Create([FromBody] ShipperInfo item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.ShipperInfoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetShipperInfo", new { id = item.shipmentNumber }, item);
            
        }

        //[Route("~/api/UpdateShipper")]
        [HttpPut("{shipmentNumber}")]
        public IActionResult Update(long ShipmentNo, [FromBody] ShipperInfo item)
        {
            if (item == null || item.shipmentNumber != ShipmentNo)
            {
                return BadRequest();
            }

            var todo = _context.ShipperInfoItems.FirstOrDefault(t => t.shipmentNumber == ShipmentNo);
            if (todo == null)
            {
                return NotFound();
            }

            todo.itemTyp = item.itemTyp;
            todo.itemNam = item.itemNam;
            todo.vendorName = item.vendorName;
            todo.itemCnt = item.itemCnt;
            todo.status = item.status;

            _context.ShipperInfoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        //[Route("~/api/DeleteShipper")]
        [HttpDelete("{ShipmentNo}")]
        public IActionResult Delete(long ShipmentNo)
        {
            var todo = _context.ShipperInfoItems.FirstOrDefault(t => t.shipmentNumber == ShipmentNo);
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
