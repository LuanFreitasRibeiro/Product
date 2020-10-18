namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        
    }
}