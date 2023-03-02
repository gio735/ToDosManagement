using FluentValidation;
using ToDosManagement.Application.Subtasks.Requests;
namespace ToDosManagement.API.Infrastructure.Validators
{
    public class SubtaskCreationValidation : AbstractValidator<SubtaskCreateRequest>
    {
        public SubtaskCreationValidation()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100);
            RuleFor(x => x.Description) 
                .MaximumLength(500);
        }
    }
}
