using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ISliderService
    {
        List<Slider> GetAll();
        Slider GetById(int id);
        bool Add(Slider slider);
        bool Edit(Slider slider);
        bool Delete(int Id);
    }
    public class ClsSlider : ISliderService
    {
        private readonly HelmiStoreContext ctx;

        public ClsSlider(HelmiStoreContext context)
        {
            this.ctx = context;
        }
        public List<Slider> GetAll()
        {
            return ctx.Sliders.ToList();
        }
        public Slider GetById(int id)
        {
            return ctx.Sliders.FirstOrDefault(b => b.Id == id);
        }
        public bool Add(Slider slider)
        {
            try
            {
                ctx.Add<Slider>(slider);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Edit(Slider slider)
        {
            try
            {
                ctx.Entry(slider).State = EntityState.Modified;
                ctx.SaveChanges();
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
                Slider slider = ctx.Sliders.FirstOrDefault(b => b.Id == Id);
                ctx.Sliders.Remove(slider);
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