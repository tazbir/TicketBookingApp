using System.Globalization;
using CsvHelper;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Features.Events.Queries.GetEventsExport;

namespace TicketManagement.Infrastructure.FileExport;

public class CsvExporter: ICsvExporter
{
    public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
    {
        using var memoryStream= new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(eventExportDtos);

        return memoryStream.ToArray();
    }
}