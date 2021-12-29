using System.Threading.Tasks;
using TravelTalkApi.Auth.Policies.Utilities;

namespace TravelTalkApi.Auth.Policies
{
    public interface IAccessPolicy<in TArgs, TExtraData>
    {
        public Task<AccessPolicyResult<TExtraData>> CanAccess(TArgs args);
    }
}