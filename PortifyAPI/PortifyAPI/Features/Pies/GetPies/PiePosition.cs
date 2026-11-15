using PortifyAPI.Dtos;
using PortifyAPI.Entities;

namespace PortifyAPI.Features.Pies.GetPies;

public sealed record PiePosition(
    IReadOnlyList<StockSummaryDto> Instruments,
    Settings Settings,
    DividendDetails DividendDetails);
