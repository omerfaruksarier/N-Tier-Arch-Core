using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();

        IDataResult<List<Product>> GetAllByCategoryId(int id);  // Bu kategori id ye sahip olan ürünleri getir.

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);  // Fiyat aralığındaki ürünleri getir.

        IDataResult<List<ProductDetailDTO>> GetProductDetails();

        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);


    }
}
