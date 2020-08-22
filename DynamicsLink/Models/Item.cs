using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsLink.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
        public ICollection<Unit> Units { get; set; }

    }
}
