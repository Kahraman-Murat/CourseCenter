using CourseCenter.WebUI.DTOs.BlogDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogDto>
    {
        public CreateBlogValidator()
        {

            RuleFor(x=>x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x=>x.Title).MaximumLength(20).WithMessage("Bu alandaki veri en fazla 30 karekter olmalidir.");

            RuleFor(x => x.Content).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Content).MaximumLength(2500).WithMessage("Bu alandaki veri en fazla 2500 karekter olmalidir.");

            
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            

            RuleFor(x => x.BlogCategoryId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
