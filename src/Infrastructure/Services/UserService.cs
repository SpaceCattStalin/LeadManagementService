using Application.Common.Interfaces.Service;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task<bool> UserExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
