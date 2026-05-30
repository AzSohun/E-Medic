using E_Medic.DTOs;
using FluentValidation;

namespace E_Medic.Validators
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {

        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Please enter your full name.")
                .MaximumLength(100).WithMessage("Name must not be exceeded more the 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please enter your email address")
                .EmailAddress().WithMessage("Enter a valid email address");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(?:\+88|88)?(01[3-9]\d{8})$").WithMessage("Enter a Bangladeshi Phone Number");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Enter your password")
                .MinimumLength(8).WithMessage("Password at least has 8 characters");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Re-enter your password")
                .Equal(x => x.Password).WithMessage("Password doesn't match");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Enter your password");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Enter your date of birth")
                .Must(dob => dob <= DateTime.Today.AddYears(-1)).WithMessage("Enter your valid birth date");
        }
    }
}
