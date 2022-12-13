using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoriesManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoriesManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryNameExist(category.CategoryName));

            if (result != null)
            {
                return result;
            }

            _categoryDal.Add(category);

            return new SuccessResult(Messages.CategoryAdded); // Messages classımızı standart mesajlar üretmemizi sağlar.
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }

        private IResult CheckIfCategoryNameExist(string categoryName)
        {
            // Aynı isimde ürün adı eklenemez.

            var result = _categoryDal.GetAll(x => x.CategoryName == categoryName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
