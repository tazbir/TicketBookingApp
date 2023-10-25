using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Contracts.Persistence;

public interface ICategoryRepository: IAsyncRepository<Category>
{
    Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
    
}