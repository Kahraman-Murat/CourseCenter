using CourseCenter.DTO.DTOs.RoleDtos;
using FluentValidation;

namespace CourseCenter.API.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol Adi alanı boş bırakılamaz.");

        }
    }
}
