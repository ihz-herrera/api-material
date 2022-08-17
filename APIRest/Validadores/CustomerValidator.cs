using APIRest.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Validadores
{
    public class CustomerValidator:AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().NotNull();
            RuleFor(c => c.Description).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(c => c.Email).EmailAddress().MaximumLength(50)
                .When(c=> c.Status);
            //RuleFor(c => c.Email)
            //    .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            //    .WithMessage("El formato del correo es invalido");
        }

    }
}
