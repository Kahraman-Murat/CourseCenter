using CourseCenter.WebUI.DTOs.CourseVideoDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateCourseVideoValidator : AbstractValidator<CreateCourseVideoDto>
    {
        public CreateCourseVideoValidator()
        {
            RuleFor(x => x.VideoNumber).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.VideoNumber).GreaterThan(0).WithMessage("Bu alan Sıfır dan büyük olmalıdır.");

            RuleFor(x => x.VideoTitle).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.VideoContent).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.VideoUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}