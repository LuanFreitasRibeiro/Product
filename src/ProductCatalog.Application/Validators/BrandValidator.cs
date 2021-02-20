using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductCatalog.Application.Validators
{
    public class BrandValidator
    {
        private readonly StoreDataContext _context;

        public BrandValidator(Brand brand)
        {
        }

        public BrandValidator(StoreDataContext context)
        {
            _context = context;
        }

        public void ValidateIfNameIsNullOrEmpty(Brand brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentNullException(brand.Name, "Value cannot be null");
            }
        }

        public void ValidateIfNameExists(Brand brand)
        {
            var result = _context.Brands.Where(x => x.Name == brand.Name);

            if(result != null)
            {
                throw new ArgumentNullException(brand.Name, "You already have brand with this name.");
            }
        }
    }
}
