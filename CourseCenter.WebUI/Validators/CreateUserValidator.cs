﻿using CourseCenter.WebUI.DTOs.UserDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad alanı boş bırakılamaz.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı Adı alanı boş bırakılamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Adresi alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre Tekrarı alanı boş bırakılamaz.")
                .Matches(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
