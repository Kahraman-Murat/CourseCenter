using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateBlogCategoryValidator : AbstractValidator<UpdateBlogCategoryDto>
    {
        public UpdateBlogCategoryValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x=>x.Name).MaximumLength(30).WithMessage("Bu alandaki veri en fazla 30 karekter olmalidir.");
        }
    }
}
