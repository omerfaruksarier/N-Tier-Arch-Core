using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); // Şartlar Koayabiliyoruz.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adı A harfi ile başlamalı");   // Method ile Şartlar Yazabiliyoruz.

        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
