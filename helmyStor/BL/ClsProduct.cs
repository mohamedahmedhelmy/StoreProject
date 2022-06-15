using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAll(string search);
        Product GetById(int id);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
        List<Product> GetRelatedProducts(decimal Price);
   
    }
    public class ClsProduct : IProductService
    {
        private readonly HelmiStoreContext ctx;

        public ClsProduct(HelmiStoreContext context)
        {
            this.ctx = context;
        }
        public bool Add(Product product)
        {
            try
            {
                 ctx.Add<Product>(product);
                 ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(int id)
        {

            try
            {
                var product =  ctx.Products.FirstOrDefault(b => b.ProductId == id);
                ctx.Products.Remove(product);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }


        }
        public List<Product> GetAll()
        {
            return ctx.Products.Include(b => b.Category).ToList();
        }

        public List<Product> GetAll(string search)
        {
            return  ctx.Products.Include(b => b.Category).Where(b => b.ProductName.Contains(search)).ToList();
        }

        public Product GetById(int id)
        {
            return  ctx.Products.Include(n=>n.Category).FirstOrDefault(b => b.ProductId == id);
        }

        public List<Product> GetRelatedProducts(decimal Price)
        {
        
            decimal first = Price - 100;
            decimal end = Price + 200;

            return ctx.Products.Include(b => b.Category).Where(b => b.SalesPrice >= first && b.SalesPrice <= end).ToList();
        }


        public bool Update(Product product)
        {
            try
            {
                ctx.Entry(product).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
