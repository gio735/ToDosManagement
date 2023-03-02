using FluentValidation;
using ToDosManagement.Application.ToDos.Requests;

namespace ToDosManagement.API.Infrastructure.Validators
{
    public class ToDoUpdateValidation : AbstractValidator<ToDoUpdateRequest>
    {
        public ToDoUpdateValidation()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100);
        }
    }
}
