namespace Catalog.API.Exceptions
{
    public class ProductException 
    {
        public class ProductNotFoundException : NotFoundException
        {
            public ProductNotFoundException(Guid Id) : base("Product", Id)
            {
            }
        }
    }
}
