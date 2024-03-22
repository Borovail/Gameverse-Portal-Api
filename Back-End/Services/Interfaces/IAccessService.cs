using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAccessService
    {
        Task<ServiceResult> GrantRoles(IReadOnlyList<string> userIds,params string[] roleNames);

        Task<ServiceResult> RevokeRoles(IReadOnlyList<string> userIds, params string[] roleNames);
    }
}
