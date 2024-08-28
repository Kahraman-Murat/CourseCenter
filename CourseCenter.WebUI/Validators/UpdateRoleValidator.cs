using CourseCenter.WebUI.DTOs.RoleDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
