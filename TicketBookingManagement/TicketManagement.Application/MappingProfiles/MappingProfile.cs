using AutoMapper;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using TicketManagement.Application.Features.Events;
using TicketManagement.Application.Features.Events.Commands.CreateEvent;
using TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.MappingProfiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventListVm>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryEventListVm>();
    }
}