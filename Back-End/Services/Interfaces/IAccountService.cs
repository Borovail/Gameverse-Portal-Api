using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult> DeleteAccount();

        Task<ServiceResult> ChangeNickname(string newNickname);
        Task<ServiceResult> ChangeEmail(string newEmail);
        Task<ServiceResult> ChangePassword(string newPassword);
    }
}
