using byin_netcore_business.Interfaces;

namespace byin_netcore_business.UseCases.Base
{
    public class BaseBusiness
    {
        protected readonly IAuthorizationBusiness _authorizationService;
        public BaseBusiness(IAuthorizationBusiness authorizationService)
        {
            _authorizationService = authorizationService;
        }
    }
}
