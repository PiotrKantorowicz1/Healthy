using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Handlers.Users
{
    public sealed class DeleteAccountHandler : ICommandHandler<DeleteAccount>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public DeleteAccountHandler(IHandler handler,
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(DeleteAccount command)
            => await _handler
                .Run(async () => await _userService.DeleteAsync(command.UserId, command.Soft))
                .OnError((ex, logger) => logger.Error(
                    $"Error occured while deleting account for user: '{command.UserId}', " +
                    $"soft: {command.Soft}."))
                .ExecuteAsync();
    }
}