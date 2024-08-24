using CourseCenter.WebUI.DTOs.UserDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bu alan bos birakilamaz.")
                .EmailAddress().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Bu alan bos birakilamaz.")
                .Matches(x => x.Password).WithMessage("Password verileri eslesmiyor.");
        }
    }
}
