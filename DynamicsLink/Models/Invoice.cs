using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsLink.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
        public int UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Net { get; set; }
        public int InoviceContainerNo { get; set; }
        [AllowNull]
        public int? InvoicesContainerID { get; set; }
        [ForeignKey("InvoicesContainerID")]
        public InvoiceContainer InvoiceContainer { get; set; }
    }
}
