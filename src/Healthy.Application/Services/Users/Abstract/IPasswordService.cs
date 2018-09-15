using System;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IPasswordService : IService
    {
        Task ChangeAsync(string userId, string currentPassword, string newPassword);
        Task ResetAsync(Guid operationId, string email);
        Task SetNewAsync(string email, string token, string password);
    }
}