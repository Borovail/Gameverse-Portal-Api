using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAccessService
    {
        Task<ServiceResult> GrantRole(string userId,params string[] roleNames);

        Task<ServiceResult> RevokeRole(string userId, params string[] roleNames);
    }
}
