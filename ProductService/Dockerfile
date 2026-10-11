FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ProductService.Api/ProductService.Api.csproj ProductService.Api/
COPY ProductService/ProductService.csproj ProductService/
RUN dotnet restore ProductService/ProductService.csproj
COPY ProductService.Api/ ProductService.Api/
COPY ProductService/ ProductService/
RUN dotnet publish ProductService/ProductService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5030
EXPOSE 5030
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
