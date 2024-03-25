using Back_End.Services.Interfaces;
using Back_End.Utils;

namespace Back_End.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public async Task<ApiResponseBuilder> ChangeEmail(string newEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> ChangeNickname(string newNickname)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> ChangePassword(string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> DeleteAccount()
        {
            throw new NotImplementedException();
        }
    }
}
