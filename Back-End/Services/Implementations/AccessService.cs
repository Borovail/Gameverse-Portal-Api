using Back_End.Services.Interfaces;
using Back_End.Utils;

namespace Back_End.Services.Implementations
{
    public class AccessService : IAccessService
    {
        public Task<ApiResponseBuilder> GrantRole(string userId, params string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseBuilder> RevokeRole(string userId, params string[] roleNames)
        {
            throw new NotImplementedException();
        }
    }
}
