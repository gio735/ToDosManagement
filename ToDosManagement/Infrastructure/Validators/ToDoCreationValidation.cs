using FluentValidation;
using ToDosManagement.Application.ToDos.Requests;

namespace ToDosManagement.API.Infrastructure.Validators
{
    public class ToDoCreationValidation : AbstractValidator<ToDoCreateRequest>
    {
        public ToDoCreationValidation()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100);
        }
    }
}
