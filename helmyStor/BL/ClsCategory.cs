using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        bool Add(Category category);
        Category GetById(int Id);
        bool Edit(Category category);
        bool Delete(int Id);
    }
    public class ClsCategory : ICategoryService
    {
        private readonly HelmiStoreContext Ctx;

        public ClsCategory(HelmiStoreContext context)
        {
            this.Ctx = context;
        }
        public List<Category> GetAll()
        {
            return Ctx.Categories.ToList();
        }
        public Category GetById(int Id)
        {
            return Ctx.Categories.FirstOrDefault(b => b.CategoryId == Id);
        }
        public bool Add(Category category)
        {
            try
            {
                Ctx.Add<Category>(category);
                Ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Edit(Category category)
        {
            try
            {
                Ctx.Entry(category).State = EntityState.Modified;
                Ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var category = Ctx.Categories.FirstOrDefault(b => b.CategoryId == Id);
                Ctx.Categories.Remove(category);
                Ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
