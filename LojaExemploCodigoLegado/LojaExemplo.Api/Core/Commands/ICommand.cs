using FluentValidation.Results;

namespace LojaExemplo.Api.Core.Commands
{
    public interface ICommand
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}
