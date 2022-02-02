using Domains;

namespace helmyStor.Models
{
    public class HomePage
    {
        public IEnumerable<Slider> LstSlider { get; set; }
        public IEnumerable<Slider> banner { get; set; }
        public IEnumerable<Product> LstAllProduct { get; set; }
        public IEnumerable<Product> LstBestSeller { get; set; }
        public IEnumerable<Product> LstNewProduct { get; set; }
        public IEnumerable<Product> LstInstgram { get; set; }
        public IEnumerable<Product> LstCategories { get; set; }
    }
}
