using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(CreateNoteCommand => CreateNoteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
