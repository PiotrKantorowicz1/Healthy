using System;
using System.Threading.Tasks;

namespace Healthy.Services.Services.Users.Abstract
{
    public interface IPasswordService : IService
    {
        Task ChangeAsync(Guid userId, string currentPassword, string newPassword);
        Task ResetAsync(Guid operationId, string email);
        Task SetNewAsync(string email, string token, string password);
    }
}