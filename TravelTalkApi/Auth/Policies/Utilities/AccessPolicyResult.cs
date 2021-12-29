namespace TravelTalkApi.Auth.Policies.Utilities
{
    public class AccessPolicyResult<TExtraData>
    {
        public AccessPolicyResult(bool canAccess, TExtraData extraData)
        {
            CanAccess = canAccess;
            ExtraData = extraData;
        }

        public bool CanAccess { get; set; }

        /**
         * Extra data returned from the AccessPolicy check method
         * This can be used for example to return an object from the database
         * that was retrieved for authorization checks to avoid re-fetching it in a controller / service
         */
        public TExtraData ExtraData { get; set; }

        public void Deconstruct(out bool canAccess, out TExtraData extraData)
        {
            canAccess = CanAccess;
            extraData = ExtraData;
        }
    }
}