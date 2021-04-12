# VaniCRM
Một dự án CRM được xây dựng cho đồ án tốt nghiệp Khóa 15 - Học viện Kỹ thuật quân sự.

### Công nghệ thực hiện:

- VueJS
- .NET
- Microsoft SQL Server

### Sinh viên thực hiện:

1. Trịnh Việt Anh
2. Hà Thị Kim Biên

### Cấu trúc thư mục:

- App
    - Backend
    - Client
- Documentation - Tất cả tài liệu liên quan

### Cách cài đặt:

#### Frontend:
``` cd App ```

``` cd client ```

``` npm install ``` hoặc ``` yarn install ``` nếu đã cài yarn.

Có thể cài yarn bằng: ```npm install -g yarn```

##### Build và thiết lập server Hot Reloads (dùng khi phát triển)
```npm start``` hoặc ``` npm run serve ```

##### Build và minify code (dùng khi đưa vào hoạt động)
```npm run build```

#### SQL
```cd Documentation```
Chạy script ```DB_Create.sql``` và ```DB_Data.sql``` trong SQL Express

#### Backend
```cd App/Backend```
Chạy ```Backend.sln```
Restore Nuget Package
Trong Package Manager run ```Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r```
Build và Run

#### Swagger
Navigate tới ```/swagger``` để xem API documentation

