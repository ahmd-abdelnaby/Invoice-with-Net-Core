using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsLink.Models
{
    public class InvoiceContainer
    {
        public int ID { get; set; }
        [DefaultValue("0")]
        public int No { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public int Total { get; set; }
        public int Taxes { get; set; }
        public int Net { get; set; }
    }
}
