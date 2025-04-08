# JobeeWebApp API
JobeeWebApp API là một ứng dụng web cung cấp các API cho hệ thống Jobee, được phát triển bằng ngôn ngữ C#.

## Mục lục

- [Giới thiệu](#giới-thiệu)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Cài đặt](#cài-đặt)
- [Sử dụng](#sử-dụng)
- [Đóng góp](#đóng-góp)
- [Giấy phép](#giấy-phép)

## Giới thiệu

JobeeWebApp API được thiết kế để cung cấp các chức năng backend cho hệ thống Jobee, bao gồm quản lý người dùng, công việc, và các dịch vụ liên quan.

## Cấu trúc dự án

```
JobeeWebApp/
├── .github/                 # Cấu hình GitHub
├── BusinessObject/          # Các đối tượng nghiệp vụ
├── Migrations/              # Quản lý cơ sở dữ liệu
├── Repository/              # Tầng truy cập dữ liệu
├── Services/                # Các dịch vụ xử lý logic
├── Validation/              # Các lớp kiểm tra dữ liệu
├── JobeeWepAppAPI/          # Ứng dụng API chính
├── .dockerignore            # Cấu hình Docker bỏ qua
├── .gitattributes           # Thuộc tính Git
├── .gitignore               # Các file và thư mục bỏ qua trong Git
├── Dockerfile               # Cấu hình Docker
└── JobeeAPI.sln             # Tệp giải pháp cho Visual Studio
```

## Yêu cầu hệ thống

- [.NET Core](https://dotnet.microsoft.com/download/dotnet-core)
- [Docker](https://www.docker.com/get-started) (nếu sử dụng Docker)

## Cài đặt

1. **Clone repository:**

   ```bash
   git clone https://github.com/thiennlse/JobeeWebApp.git
   ```

2. **Chuyển đến thư mục dự án:**

   ```bash
   cd JobeeWebApp
   ```

3. **Khởi tạo và cập nhật cơ sở dữ liệu:**

   ```bash
   dotnet ef database update
   ```

## Sử dụng

### Chạy ứng dụng bằng .NET Core

1. **Chuyển đến thư mục chứa API:**

   ```bash
   cd JobeeWepAppAPI
   ```

2. **Chạy ứng dụng:**

   ```bash
   dotnet run
   ```

### Chạy ứng dụng bằng Docker

1. **Xây dựng hình ảnh Docker:**

   ```bash
   docker build -t jobeewebapp .
   ```

2. **Chạy container:**

   ```bash
   docker run -d -p 5000:80 jobeewebapp
   ```

   Ứng dụng sẽ chạy tại `http://localhost:5000`.

## Đóng góp

Chúng tôi hoan nghênh mọi đóng góp từ cộng đồng. Vui lòng tạo một pull request hoặc mở issue để thảo luận về các thay đổi.

## Giấy phép

Dự án này được cấp phép theo giấy phép MIT. Xem tệp [LICENSE](LICENSE) để biết thêm chi tiết.
