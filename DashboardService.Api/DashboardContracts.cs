namespace DashboardService.Api.Queries
{
    using DashboardService.Api.Queries.Dtos;

    public abstract class SalesPeriodQuery
    {
        public DateTime SalesDateFrom { get; set; }
        public DateTime SalesDateTo { get; set; }
    }

    public sealed class GetTotalSalesQuery : SalesPeriodQuery
    {
        public string? ProductCode { get; set; }
    }

    public sealed class GetAgentsSalesQuery : SalesPeriodQuery { }

    public sealed class GetSalesTrendsQuery : SalesPeriodQuery
    {
        public TimeUnit Unit { get; set; } = TimeUnit.Month;
    }

    public sealed class GetTotalSalesResult
    {
        public SalesDto Total { get; set; } = new();
        public Dictionary<string, SalesDto> PerProductTotal { get; set; } = new();
    }

    public sealed class GetAgentsSalesResult
    {
        public Dictionary<string, SalesDto> PerAgentTotal { get; set; } = new();
    }

    public sealed class GetSalesTrendsResult
    {
        public List<PeriodSalesDto> PeriodsSales { get; set; } = new();
    }
}

namespace DashboardService.Api.Queries.Dtos
{
    public enum TimeUnit { Day, Week, Month, Year }

    public sealed class SalesDto
    {
        public decimal PremiumAmount { get; set; }
        public long PoliciesCount { get; set; }
    }

    public sealed class PeriodSalesDto
    {
        public string Period { get; set; } = string.Empty;
        public SalesDto Sales { get; set; } = new();
    }
}
