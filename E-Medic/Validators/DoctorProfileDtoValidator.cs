using E_Medic.DTOs;
using FluentValidation;

namespace E_Medic.Validators
{
    public class DoctorProfileDtoValidator: AbstractValidator<DoctorProfileDto>
    {
        public DoctorProfileDtoValidator()
        {
            RuleFor(x => x.Qualifications)
                .NotEmpty().WithMessage("Qualifications field is required.")
                .MaximumLength(200).WithMessage("Qualifications cannot exceed 200 characters.");

            RuleFor(x => x.Specialty)
                .NotEmpty().WithMessage("Specialty field is required.")
                .MaximumLength(100).WithMessage("Specialty cannot exceed 100 characters.");

            RuleFor(x => x.AvailableHours)
                .NotEmpty().WithMessage("Available hours/schedule is required.")
                .MaximumLength(300).WithMessage("Available hours cannot exceed 300 characters.");

            RuleFor(x => x.ConsultationFee)
                .GreaterThanOrEqualTo(100).WithMessage("Consultation fee must be at least 100 TK.")
                .LessThanOrEqualTo(10000).WithMessage("Consultation fee cannot exceed 10,000 TK.");

            RuleFor(x => x.ExperienceYears)
                .GreaterThanOrEqualTo(0).WithMessage("Experience years cannot be negative.")
                .LessThanOrEqualTo(50).WithMessage("Invalid experience years.");
        }
    }
}
