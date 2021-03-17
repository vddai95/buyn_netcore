using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_business.UseCases.Base;
using System;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.OrderBusiness
{
    public class OrderBusiness : BaseBusiness, IOrderBusiness
    {
        IRepository<Order> OrderRepository;
        public OrderBusiness(IRepository<Order> orderRepos, IAuthorizationBusiness authorizationService) : base(authorizationService)
        {
            OrderRepository = orderRepos;
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            return await OrderRepository.InsertAsync(order).ConfigureAwait(false);
        }

        public Order ValidateOrder(Order order)
        {
            throw new NotImplementedException();
        }

    }
}
