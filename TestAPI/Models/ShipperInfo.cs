using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class ShipperInfo
    {
        [Key]
        public long ShipmentNo { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string Vendor { get; set; }
        public Int32 Count { get; set; }
        public string Status { get; set; }

    }
}
