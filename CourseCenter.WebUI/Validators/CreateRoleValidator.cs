using CourseCenter.WebUI.DTOs.RoleDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
