using Domains;

namespace helmyStor.Models
{
    public class ProductDetails
    {
        public Product Product { get; set; }
        public IEnumerable<Product> LstRelatedProducts { get; set; }
    }
}
