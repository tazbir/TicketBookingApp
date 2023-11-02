using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using TicketManagement.Application.Contracts;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Persistence.IntegrationTests;

public class TicketManagementDbContextTests
{
    private readonly TicketManagementDbContext _ticketManagementDbContext;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;
    
    public TicketManagementDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<TicketManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = Guid.Empty.ToString();
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(st => st.UserId).Returns(_loggedInUserId);
        _ticketManagementDbContext = new TicketManagementDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    [Fact]
    public async Task Saving_Sets_CreatedBy_Property()
    {
        var eventObj = new Event() { EventId = Guid.NewGuid(), Name = "Saving Test Event" };

        await _ticketManagementDbContext.Events.AddAsync(eventObj);

        await _ticketManagementDbContext.SaveChangesAsync();
        
        eventObj.CreatedBy.ShouldBe(_loggedInUserId);
        eventObj.Name.ShouldBe("Saving Test Event");
    }
}