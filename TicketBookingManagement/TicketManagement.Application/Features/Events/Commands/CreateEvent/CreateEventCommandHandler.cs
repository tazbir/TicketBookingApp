using AutoMapper;
using MediatR;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Models.Mail;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler: IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _emailService = emailService;
    }
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEventCommandValidator(_eventRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);
        var @event = _mapper.Map<Event>(request);
        @event = await _eventRepository.AddAsync(@event);

        SendNotificationEmail(request);
        
        return @event.EventId;
    }

    private void SendNotificationEmail(CreateEventCommand request)
    {
        var email = new Email()
        {
            To = "tazbirs@gmail.com",
            Body = "A new event is being created " + request,
            Subject = "Event Creation Alert"
        };

        try
        {
            _emailService.SendEmail(email);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}