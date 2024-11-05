using BankSystem.App.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
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
            RuleFor(c => c.Contract)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Depatment)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.JobTitle)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Salary)
                .NotNull()
                .NotEmpty();

        }
    }
}
