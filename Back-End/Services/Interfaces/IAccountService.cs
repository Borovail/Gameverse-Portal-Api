using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponseBuilder> DeleteAccount();

        Task<ApiResponseBuilder> ChangeNickname(string newNickname);
        Task<ApiResponseBuilder> ChangeEmail(string newEmail);
        Task<ApiResponseBuilder> ChangePassword(string newPassword);
    }
}
