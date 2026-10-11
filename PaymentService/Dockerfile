FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY PaymentService.Api/PaymentService.Api.csproj PaymentService.Api/
COPY PaymentService/PaymentService.csproj PaymentService/
RUN dotnet restore PaymentService/PaymentService.csproj
COPY PaymentService.Api/ PaymentService.Api/
COPY PaymentService/ PaymentService/
RUN dotnet publish PaymentService/PaymentService.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5070
EXPOSE 5070
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.dll"]
