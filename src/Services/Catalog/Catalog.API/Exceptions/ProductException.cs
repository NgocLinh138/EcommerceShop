﻿namespace Catalog.API.Exceptions
{
    public class ProductException 
    {
        public class ProductNotFoundException : NotFoundException
        {
            public ProductNotFoundException() : base("Product not found.")
            {
            }
        }
    }
}