namespace CmsCapaMedikal.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public string BottomBrand { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public byte [] Photo { get; set; }

    }
}
