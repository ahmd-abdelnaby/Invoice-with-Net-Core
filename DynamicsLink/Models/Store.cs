using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsLink.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items  { get; set; }
    }
}
