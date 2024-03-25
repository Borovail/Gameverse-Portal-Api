using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAccessService
    {
        Task<ApiResponseBuilder> GrantRole(string userId,params string[] roleNames);

        Task<ApiResponseBuilder> RevokeRole(string userId, params string[] roleNames);
    }
}
