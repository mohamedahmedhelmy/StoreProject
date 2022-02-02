using Domains;

namespace helmyStor.ViewModel
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoices> Invoices { get; set; }
        public Customer Customer { get; set; }
        public Invoices InvoiceOrder { get; set; }
        public decimal TotalPriceInvoce { get; set; }   
    }
}
