using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IProductApi
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int Id);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int Id);
        Task<Product> AddProduct(Product product);
    }
    public class ProductApi:IProductApi
    {
        private readonly HelmiStoreContext ctx;
        public ProductApi(HelmiStoreContext context)
        {
            ctx = context;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var result = await ctx.Products.AddAsync(product);
            await ctx.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> DeleteProduct(int Id)
        {
            var result = ctx.Products.Find(Id);
            if (result != null)
            {
                ctx.Products.Remove(result);
                await ctx.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int Id)
        {
            return await ctx.Products.FirstOrDefaultAsync(b => b.ProductId == Id);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result =  ctx.Products.FirstOrDefault(b => b.ProductId == product.ProductId);
            if (result != null)
            {
                ctx.Entry(product).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return product;
            }
            return null;
        }
    }
}
