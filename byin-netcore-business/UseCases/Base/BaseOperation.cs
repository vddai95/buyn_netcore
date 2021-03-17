using byin_netcore_business.Constants;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace byin_netcore_business.UseCases.Base
{
    public class BaseOperation
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = OperationNames.Create };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = OperationNames.Read };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = OperationNames.Update };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = OperationNames.Delete };
        public static OperationAuthorizationRequirement ReadAll =
          new OperationAuthorizationRequirement { Name = OperationNames.ReadAll };
    }
}
