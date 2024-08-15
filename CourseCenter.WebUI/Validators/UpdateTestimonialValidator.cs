using CourseCenter.WebUI.DTOs.TestimonialDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateTestimonialValidator : AbstractValidator<UpdateTestimonialDto>
    {
        public UpdateTestimonialValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Comment).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            
            RuleFor(x => x.Star).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
