using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        // Eğer Nesne( Product -- liste şeklinde olabilir) döneceksen ErrorDataResult veya SuccessDataResult -- void ise SuccessResult veya ErrorResult ile dön.
        IProductDal _productDal;

        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;

            _categoryService = categoryService;
        }

       //[SecuredOperation("product.add,admin")]  // Hangi Methoda Hangi Rolün erişimi var.
        [ValidationAspect(typeof(ProductValidator))] // Eklenen Ürünün Validation kontrolü sağlanır.
        [CacheRemoveAspect("IProductService.Get")]  // Ürün eklenir veya güncellenir ise cacheden silinir.
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded); // Messages classımızı standart mesajlar üretmemizi sağlar.
        }


        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));   
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDTO>> GetProductDetails()
        {
            //if(DateTime.Now.Hour == 15)
            //{
            //    return new ErrorDataResult<List<ProductDetailDTO>>("HATA");
            //}

            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // Bir kategoride en fazla 10 ürün olabilir sorgulaması

            var result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            // Aynı isimde ürün adı eklenemez.

            var result = _productDal.GetAll(x => x.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [PerformanceAspect(5)] // Performansı test eder eğer method 5 saniyeden fazla sürer ise uyarı gönderir.
        public IResult AddTransactional(Product product) // Bir test işlemidir. Amaç iki veya daha fazla add() methodu çağırılacak ise ve
                                                         // birincisi çalışırda ikincisinde hata verirse birinci add() methoduda geri alınır.
                                                         // try catch yapısını Aspect ettik.
        {
            Add(product);

            if(product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
        }
    }
}
