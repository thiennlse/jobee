using FluentValidation;

using BusinessObject.RequestModel;

namespace Validaiton.User
{
    public class AccountValidation : AbstractValidator<RegisterAccountModel>
    {
        public AccountValidation() 
        {
            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Not a valid email");
        }
    }
}
