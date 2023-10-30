using MediatR;

namespace TicketManagement.Application.Features.Events;

public class GetEventsListQuery: IRequest<List<EventListVm>>
{
    
}