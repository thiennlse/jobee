# Sử dụng hình ảnh .NET 6.0 SDK làm hình ảnh cơ sở
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép csproj và khôi phục các gói
COPY *.csproj ./
RUN dotnet restore

# Sao chép tất cả mã nguồn
COPY . ./

# Biên dịch ứng dụng
RUN dotnet publish -c Release -o out

# Tạo hình ảnh runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app/out .

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "JobeeWebAPI.dll"] # Thay YourProjectName bằng tên dự án của bạn