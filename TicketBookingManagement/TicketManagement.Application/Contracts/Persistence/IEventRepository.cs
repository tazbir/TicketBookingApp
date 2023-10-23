using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Contracts.Persistence;

public interface IEventRepository: IAsyncRepository<Event>
{
    
}