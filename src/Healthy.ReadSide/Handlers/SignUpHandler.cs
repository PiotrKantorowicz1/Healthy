using System;
using System.Threading.Tasks;
using Healthy.Contracts.Commands;
using Healthy.Contracts.Events.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.ReadSide.Handlers
{
    public class SignUpHandler : IEventHandler<SignedUp>
    {
        private readonly IHandler _handler;

        public SignUpHandler(IHandler handler)
        {
            _handler = handler;
        }

        public async Task HandleAsync(SignedUp @event)
            => await _handler
                .Run(async () => throw new NotImplementedException())
                .OnError((ex, logger) => logger.Error(ex, ""))
                .ExecuteAsync();
    }
}