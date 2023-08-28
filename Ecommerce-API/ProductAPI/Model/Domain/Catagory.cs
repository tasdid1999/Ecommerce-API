namespace ProductAPI.Model.Domain
{
    public class Catagory
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual List<Product>? products { get; set; }
    }
}
