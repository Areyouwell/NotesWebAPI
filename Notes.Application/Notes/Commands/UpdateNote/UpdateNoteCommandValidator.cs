using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(CreateNoteCommand => CreateNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(CreateNoteCommand => CreateNoteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
