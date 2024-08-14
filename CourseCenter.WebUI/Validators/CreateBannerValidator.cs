using CourseCenter.WebUI.DTOs.BannerDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateBannerValidator : AbstractValidator<CreateBannerDto>
    {
        public CreateBannerValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            //RuleFor(x=>x.Title).MaximumLength(30).WithMessage("Bu alandaki veri en fazla 30 karekter olmalidir.");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}