namespace PricingService.Api;

public sealed record PriceCalculationRequest(string ProductCode, DateTime PolicyFrom, DateTime PolicyTo);
public sealed record PriceCalculationResult(decimal TotalPrice, string Currency = "EUR");
