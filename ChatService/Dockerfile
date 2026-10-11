FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ChatService.Api/ChatService.Api.csproj ChatService.Api/
COPY ChatService/ChatService.csproj ChatService/
RUN dotnet restore ChatService/ChatService.csproj
COPY ChatService.Api/ ChatService.Api/
COPY ChatService/ ChatService/
RUN dotnet publish ChatService/ChatService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:4099
EXPOSE 4099
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ChatService.dll"]
