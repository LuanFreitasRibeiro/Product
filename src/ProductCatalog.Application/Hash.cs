using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ProductCatalog.Application
{
    public class Hash
    {
        private HashAlgorithm _algorithm;
        private readonly StoreDataContext _context;

        public Hash(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public Hash(StoreDataContext context, HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
            _context = context;
        }

        public string EncryptPassword(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
