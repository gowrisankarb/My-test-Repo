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
        public long shipmentNumber { get; set; }
        public string itemTyp { get; set; }
        public string itemNam { get; set; }
        public string vendorName { get; set; }
        public Int32 itemCnt { get; set; }
        public string status { get; set; }

    }
}
