using FluentValidation;
using HRAPI.Models;


namespace HRAPI.Validations
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {

        public EmployeeValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("სახელის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("გვარის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("სქესის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("დაბადების თარიღის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("თანამდებობის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Mobile)
                .NotEmpty()
                .WithMessage("მობილურის ნომრის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.IdNumber)
                .NotEmpty()
                .Length(11)
                .WithMessage("პირადი ნომერის ფორმატი არასწორია. შეიყვანეთ 11 ციფრიანი პირადი ნომერი");
            
        }

    }
}