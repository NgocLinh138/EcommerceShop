namespace Catalog.API.Exceptions
{
    public class ProductException 
    {
        public class ProductNotFoundException : NotFoundException
        {
            public ProductNotFoundException(Guid id) : base("Product", id)
            {
            }
        }
    }
}
