namespace Catalog.API.Exceptions
{
    public class CategoryException
    {
        public class CategoryNotFoundException : NotFoundException
        {
            public CategoryNotFoundException(Guid id) : base("Category", id)
            {
            }
        }
    }
}
