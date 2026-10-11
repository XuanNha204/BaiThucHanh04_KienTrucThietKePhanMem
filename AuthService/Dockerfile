FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AuthService.Api/AuthService.Api.csproj AuthService.Api/
COPY AuthService/AuthService.csproj AuthService/
RUN dotnet restore AuthService/AuthService.csproj
COPY AuthService.Api/ AuthService.Api/
COPY AuthService/ AuthService/
RUN dotnet publish AuthService/AuthService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:6060
EXPOSE 6060
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthService.dll"]
