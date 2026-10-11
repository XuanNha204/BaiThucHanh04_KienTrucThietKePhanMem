FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY PolicySearchService.Api/PolicySearchService.Api.csproj PolicySearchService.Api/
COPY PolicySearchService/PolicySearchService.csproj PolicySearchService/
RUN dotnet restore PolicySearchService/PolicySearchService.csproj
COPY PolicySearchService.Api/ PolicySearchService.Api/
COPY PolicySearchService/ PolicySearchService/
RUN dotnet publish PolicySearchService/PolicySearchService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5065
EXPOSE 5065
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PolicySearchService.dll"]
