namespace AllwellInventory.Models
{
    public class ProductLite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public bool Damaged { get; set; }
    }
}