FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY DocumentService.Api/DocumentService.Api.csproj DocumentService.Api/
COPY DocumentService/DocumentService.csproj DocumentService/
RUN dotnet restore DocumentService/DocumentService.csproj
COPY DocumentService.Api/ DocumentService.Api/
COPY DocumentService/ DocumentService/
RUN dotnet publish DocumentService/DocumentService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5075
EXPOSE 5075
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DocumentService.dll"]
