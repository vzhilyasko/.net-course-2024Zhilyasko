using BankSystem.App.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Validators
{
    public class ClientDtoValidator: AbstractValidator<ClientDto>
    {
        public ClientDtoValidator()
        { 
        RuleFor(c => c.FullName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя пользователя обязательно");

        RuleFor(c => c.Birthday)
            .NotNull()
            .NotEmpty();
            
        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.PhoneNumber)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.PassportSeriya)
            .NotNull()
            .NotEmpty();
        RuleFor(c => c.PassportNumber)
            .NotNull()
            .NotEmpty();
        }
    }
}
