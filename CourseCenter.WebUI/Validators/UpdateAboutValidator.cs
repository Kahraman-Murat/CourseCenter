using CourseCenter.WebUI.DTOs.AboutDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.OurMission).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.OurVision).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x=>x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x=>x.Title).MaximumLength(100).WithMessage("Bu alandaki veri en fazla 100 karekter olmalidir.");

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
