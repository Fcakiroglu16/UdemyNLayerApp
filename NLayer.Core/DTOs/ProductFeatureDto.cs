namespace NLayer.Core.DTOs
{
    public class ProductFeatureDto
    {

        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public int ProductId { get; set; }
    }
}
