using FluentValidation;
using ToDosManagement.Application.Users.Requests;

namespace ToDosManagement.API.Infrastructure.Validators
{
    public class UserUpdateValidation : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateValidation()
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(50);
        }
    }
}
