using FluentValidation;
using HRAPI.Models;

namespace HRAPI.Validations
{
    public class AdministratorValidation : AbstractValidator<AdministratorModel>
    {
        public AdministratorValidation()
        {
            RuleFor(x => x.Name)
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

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("დაბადების თარიღის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("ელ ფოსტის ველი არ უნდა იყოს ცარიელი")
               .EmailAddress()
               .WithMessage("ელ ფოსტის ფორმატი არასწორია");

            RuleFor(x => x.JobTitle)
               .NotEmpty()
               .WithMessage("თანამდებობის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Status)
              .NotEmpty()
              .WithMessage("სტატუსის ველი არ უნდა იყოს ცარიელი");

            RuleFor(x => x.Mobile)
              .NotEmpty()
              .WithMessage("მობილურის ველი არ უნდა იყოს ცარიელი");

        }
    }
}
