﻿using ProductCatalog.Data;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Brand;
using System;
using System.Linq;

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

        public void Validate(CreateBrandRequest brand)
        {
            ValidateIfNameIsNullOrEmpty(brand);
            ValidateIfNameExists(brand);
        }

        public void ValidateIfNameIsNullOrEmpty(CreateBrandRequest brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentNullException(brand.Name, "Value cannot be null.");
            }
        }

        public void ValidateIfNameExists(CreateBrandRequest brand)
        {
            var result = _context.Brands.Any(x => x.Name == brand.Name);

            if (result == true)
            {
                throw new ArgumentNullException(brand.Name, "You already have brand with this name.");
            }
        }
    }
}
