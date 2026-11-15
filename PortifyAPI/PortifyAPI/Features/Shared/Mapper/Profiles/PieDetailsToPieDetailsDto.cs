using PortifyAPI.Dtos;
using PortifyAPI.Entities;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

public class PieDetailsToPieDetailsDto : IMapper<PieDetails, PieDetailsDto>
{
    public PieDetailsDto Map(PieDetails source) => new(
        source.Instruments.Select(instrument => new InstrumentDetailsDto(
            instrument.CurrentShare,
            instrument.ExpectedShare,
            instrument.Issues.Select(issue => new InstrumentIssueDto(
                issue.Name, issue.Severity)).ToArray(),
            instrument.OwnedQuantity,
            new PieResultDto(
                instrument.Result.PriceAvgInvestedValue,
                instrument.Result.PriceAvgResult,
                instrument.Result.PriceAvgResultCoef,
                instrument.Result.PriceAvgValue),
            instrument.Ticker)).ToArray(),
        new PieSettingsDto(
            source.Settings.Name,
            source.Settings.CreationDate,
            source.Settings.DividendCashAction,
            source.Settings.EndDate,
            source.Settings.Goal,
            source.Settings.Icon,
            source.Settings.Id,
            source.Settings.InitialInvestment,
            new Dictionary<string, decimal>(source.Settings.InstrumentShares),
            source.Settings.PublicUrl));
}
