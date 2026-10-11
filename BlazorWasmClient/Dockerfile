FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY DashboardService.Api/DashboardService.Api.csproj DashboardService.Api/
COPY PolicySearchService.Api/PolicySearchService.Api.csproj PolicySearchService.Api/
COPY PolicyService.Api/PolicyService.Api.csproj PolicyService.Api/
COPY PricingService.Api/PricingService.Api.csproj PricingService.Api/
COPY ProductService.Api/ProductService.Api.csproj ProductService.Api/
COPY BlazorWasmClient/BlazorWasmClient.csproj BlazorWasmClient/
RUN dotnet restore BlazorWasmClient/BlazorWasmClient.csproj
COPY DashboardService.Api/ DashboardService.Api/
COPY PolicySearchService.Api/ PolicySearchService.Api/
COPY PolicyService.Api/ PolicyService.Api/
COPY PricingService.Api/ PricingService.Api/
COPY ProductService.Api/ ProductService.Api/
COPY BlazorWasmClient/ BlazorWasmClient/
RUN dotnet publish BlazorWasmClient/BlazorWasmClient.csproj -c Release -o /app/publish --no-restore
FROM nginx:1.27-alpine AS final
COPY BlazorWasmClient/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80
