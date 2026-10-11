namespace PaymentService.Api;

public sealed record PolicyAccountDto(string PolicyNumber, string AccountNumber, decimal Balance);
public sealed record RegisterPaymentCommand(string PolicyNumber, decimal Amount, DateTime PaidAt);
