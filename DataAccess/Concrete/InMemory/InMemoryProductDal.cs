using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product> {
            new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=8},
            new Product{ProductId=2,CategoryId=2,ProductName="Televizyon",UnitPrice=15,UnitsInStock=8},
            new Product{ProductId=3,CategoryId=3,ProductName="Ütü",UnitPrice=15,UnitsInStock=8},
            new Product{ProductId=4,CategoryId=4,ProductName="Tost Makinası",UnitPrice=15,UnitsInStock=8},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product producToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            _products.Remove(producToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDTO> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product producToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            producToUpdate.ProductName = product.ProductName;
            producToUpdate.UnitPrice = product.UnitPrice;
            producToUpdate.UnitsInStock = product.UnitsInStock;
            product.CategoryId = product.CategoryId;
        }
    }
}
