# Insurance Agency Microservices - Bài thực hành 4

Solution .NET 8 cho case study đại lý bảo hiểm, được tổ chức theo ba project cho mỗi microservice:

- `<Service>.Api`: command, query, event và DTO công khai.
- `<Service>`: triển khai HTTP/SignalR và đăng ký MediatR.
- `<Service>.Test`: unit test và integration test.

## Build và kiểm thử

```powershell
.\scripts\build.ps1
```

## Chạy bằng Docker Compose

Sau khi Docker Desktop hoạt động:

```powershell
.\scripts\run-all.ps1
```

Các địa chỉ chính:

- Vue: http://localhost:8080
- Blazor: http://localhost:5081
- API Gateway: http://localhost:8099
- Auth: http://localhost:6060
- RabbitMQ Management: http://localhost:15672
- Elasticsearch: http://localhost:9200
- Eureka: http://localhost:8761

## Backup PostgreSQL

```powershell
.\scripts\backup-databases.ps1
```

Các file backup custom-format được tạo trong `output/database`.
