using CourseCenter.WebUI.DTOs.AboutDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDto>
    {
        public CreateAboutValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x=>x.Title).MaximumLength(30).WithMessage("Bu alandaki veri en fazla 30 karekter olmalidir.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Bu alandaki veri en fazla 1000 karekter olmalidir.");

            RuleFor(x => x.ImageUrl1).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.ImageUrl2).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Item1).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Item1).MaximumLength(50).WithMessage("Bu alandaki veri en fazla 50 karekter olmalidir.");

            RuleFor(x => x.Item2).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Item2).MaximumLength(50).WithMessage("Bu alandaki veri en fazla 50 karekter olmalidir.");

            RuleFor(x => x.Item3).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Item3).MaximumLength(50).WithMessage("Bu alandaki veri en fazla 50 karekter olmalidir.");

            RuleFor(x => x.Item4).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Item4).MaximumLength(50).WithMessage("Bu alandaki veri en fazla 50 karekter olmalidir.");
        }
    }
}
