using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public  class Invoices
    {
        [Key]
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int CustomerId { get; set; }
      
        [ForeignKey("CustomerId")]
        public Customer Customer{ get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
