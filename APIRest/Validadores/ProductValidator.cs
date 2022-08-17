using APIRest.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Validadores
{
    public class ProductValidator: AbstractValidator<Product>
    {

        public ProductValidator()
        {
            RuleFor(p => p).Must(p => p.Cost < p.Price);
            RuleFor(p => p.ProductId).NotEmpty().NotNull();
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Price).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.Cost).NotEmpty().NotNull().GreaterThan(0);
        }

    }
}
