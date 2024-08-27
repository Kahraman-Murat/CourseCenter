using CourseCenter.DTO.DTOs.RoleDtos;
using FluentValidation;

namespace CourseCenter.API.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol Adi alanı boş bırakılamaz.");

        }
    }
}
