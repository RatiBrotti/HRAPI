﻿using FluentValidation;
using HRAPI.Models;

namespace HRAPI.Validations
{
    public class AdministratorValidation : AbstractValidator<Administrator>
    {
        public AdministratorValidation()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .WithMessage("სახელის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("გვარის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("ელ ფოსტის ფორმატი არასწორია");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("პაროლის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.IdNumber)
                .NotEmpty()
                .Length(11)
                .WithMessage("პირადი ნომერის ფორმატი არასწორია. შეიყვანეთ 11 ციფრიანი პირადი ნომერი");

        }
    }
}
