namespace PolicySearchService.Api.Queries
{
    using PolicySearchService.Api.Queries.Dtos;

    public sealed class FindPolicyResult
    {
        public List<PolicyDto> Policies { get; set; } = new();
    }
}

namespace PolicySearchService.Api.Queries.Dtos
{
    public sealed class PolicyDto
    {
        public string PolicyNumber { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public string PolicyHolder { get; set; } = string.Empty;
        public decimal PremiumAmount { get; set; }
    }
}
