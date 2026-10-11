FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY DashboardService.Api/DashboardService.Api.csproj DashboardService.Api/
COPY DashboardService/DashboardService.csproj DashboardService/
RUN dotnet restore DashboardService/DashboardService.csproj
COPY DashboardService.Api/ DashboardService.Api/
COPY DashboardService/ DashboardService/
RUN dotnet publish DashboardService/DashboardService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5035
EXPOSE 5035
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DashboardService.dll"]
