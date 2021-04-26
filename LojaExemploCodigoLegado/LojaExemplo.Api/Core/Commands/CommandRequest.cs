using FluentValidation.Results;
using MediatR;
using System;

namespace LojaExemplo.Api.Core.Commands
{
    public abstract class CommandRequest<TResponse> : ICommand, IRequest<TResponse>
    {
        protected CommandRequest()
        {
            MessageTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public long MessageTimestamp { get; private set; }
        public abstract ValidationResult ValidationResult { get; }
        public bool IsValid => ValidationResult.IsValid;
    }
}
