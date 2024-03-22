using Back_End.Services.Interfaces;
using Back_End.Utils;

namespace Back_End.Services.Implementations
{
    public class AccessService : IAccessService
    {
        public async Task<ServiceResult> GrantRoles(IReadOnlyList<string> userIds, params string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> RevokeRoles(IReadOnlyList<string> userIds, params string[] roleNames)
        {
            throw new NotImplementedException();
        }
    }
}
