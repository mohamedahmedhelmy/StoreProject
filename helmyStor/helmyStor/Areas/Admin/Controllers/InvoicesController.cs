using BL;
using helmyStor.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace helmyStor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoicesController : Controller
    {
        private readonly HelmiStoreContext _ctx;
        public InvoicesController(HelmiStoreContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult List()
        {
            var Customeres =_ctx.Customers.ToList();
            return View(Customeres);
        }
        public IActionResult Details(int Id)
        {
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel()
            {
                Invoices = _ctx.Invoices.Where(b => b.CustomerId == Id).Include(p => p.Product).ToList(),
                TotalPriceInvoce=_ctx.Invoices.Where(b=>b.CustomerId==Id).Sum(b=>b.TotalPrice),
                Customer=_ctx.Customers.FirstOrDefault(b=>b.CustomerId== Id),
                InvoiceOrder=_ctx.Invoices.Include(b=>b.Order).FirstOrDefault(b=>b.CustomerId== Id)
            };
          
            return View(invoiceViewModel);
        }
    }
}
