using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsLink.Models
{
    public class Unit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
