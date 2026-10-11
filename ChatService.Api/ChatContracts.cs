namespace ChatService.Api;

public sealed record ChatMessageDto(string Sender, string Message, DateTime SentAt);
