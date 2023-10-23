using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Contracts.Persistence;

public interface IOrderRepository: IAsyncRepository<Order>
{
    
}