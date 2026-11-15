using PortifyAPI.Dtos;
using PortifyAPI.Entities;

namespace PortifyAPI.Features.Pies.GetPies;

internal sealed record PiePosition(
    IReadOnlyList<StockSummaryDto> Instruments,
    Settings Settings,
    DividendDetails DividendDetails);
