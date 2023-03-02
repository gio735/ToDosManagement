using FluentValidation;
using ToDosManagement.Application.Users.Requests;

namespace ToDosManagement.API.Infrastructure.Validators
{
    public class UserCreationValidation : AbstractValidator<UserCreateRequest>
    {
        public UserCreationValidation()
        {
            RuleFor(x => x.Username)
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(50);
        }
    }
}
