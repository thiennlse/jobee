# Sử dụng hình ảnh .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Sao chép tệp giải pháp vào container
COPY JobeeAPI.sln ./

# Sao chép các tệp csproj của từng dự án vào container
COPY JobeeWepAppAPI/JobeeWepAppAPI.csproj JobeeWepAppAPI/
COPY BusinessObject/BusinessObject.csproj BusinessObject/
COPY Repository/Repository.csproj Repository/
COPY Services/Services.csproj Services/

# Khôi phục các gói NuGet
RUN dotnet restore

# Sao chép tất cả mã nguồn và xây dựng ứng dụng
COPY . ./
RUN dotnet publish JobeeWepAppAPI/JobeeWepAppAPI.csproj -c Release -o out

# Sử dụng hình ảnh .NET Runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out ./

# Chỉ định cổng mà ứng dụng sẽ lắng nghe
EXPOSE 80

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "JobeeWepAppAPI.dll"]