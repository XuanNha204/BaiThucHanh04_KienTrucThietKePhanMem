FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY PolicyService.Api/PolicyService.Api.csproj PolicyService.Api/
COPY PolicyService/PolicyService.csproj PolicyService/
RUN dotnet restore PolicyService/PolicyService.csproj
COPY PolicyService.Api/ PolicyService.Api/
COPY PolicyService/ PolicyService/
RUN dotnet publish PolicyService/PolicyService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5050
EXPOSE 5050
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PolicyService.dll"]
