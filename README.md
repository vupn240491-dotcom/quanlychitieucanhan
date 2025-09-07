# Ứng dụng Quản lý Chi tiêu Cá nhân

Ứng dụng web ASP.NET Core MVC để quản lý chi tiêu cá nhân với SQLite database.

## Tính năng chính

### 🔐 Xác thực người dùng
- Đăng ký tài khoản mới
- Đăng nhập/Đăng xuất
- Quản lý thông tin cá nhân
- Đổi mật khẩu

### 💰 Quản lý chi tiêu
- Thêm, sửa, xóa giao dịch chi tiêu
- Ghi chú cho từng giao dịch
- Lọc và tìm kiếm chi tiêu
- Hiển thị danh sách chi tiêu theo thời gian

### 🎯 Quản lý danh mục
- Tạo danh mục chi tiêu tùy chỉnh
- Chọn icon và màu sắc cho danh mục
- Thiết lập ngân sách cho từng danh mục
- Xóa/chỉnh sửa danh mục

### 📊 Dashboard & Thống kê
- Tổng quan chi tiêu tháng hiện tại
- Biểu đồ xu hướng chi tiêu
- Phân bố chi tiêu theo danh mục
- So sánh với tháng trước
- Thống kê số giao dịch

### 📈 Báo cáo chi tiết
- Báo cáo chi tiêu theo tháng/năm
- Phân tích chi tiêu theo danh mục
- Top chi tiêu lớn nhất
- Theo dõi ngân sách và mức sử dụng
- Biểu đồ trực quan

### 👤 Thông tin cá nhân
- Xem thống kê tổng quan
- Cập nhật thông tin tài khoản
- Đổi mật khẩu
- Lịch sử sử dụng ứng dụng

## Công nghệ sử dụng

- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite với Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: HTML, CSS (Tailwind CSS), JavaScript
- **Charts**: Chart.js
- **UI Framework**: Tailwind CSS

## Cài đặt và chạy

### Yêu cầu hệ thống
- .NET 9.0 SDK
- Visual Studio 2022 hoặc VS Code

### Các bước cài đặt

1. **Clone hoặc tải project**
   ```bash
   cd ExpenseTracker
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Tạo database**
   ```bash
   dotnet ef database update
   ```

4. **Chạy ứng dụng**
   ```bash
   dotnet run
   ```

5. **Truy cập ứng dụng**
   - Mở trình duyệt và truy cập: `http://localhost:5276`
   - Hoặc `https://localhost:7276` (HTTPS)

## Cấu trúc project

```
ExpenseTracker/
├── Controllers/           # Các controller MVC
│   ├── AccountController.cs
│   ├── DashboardController.cs
│   ├── ExpensesController.cs
│   ├── CategoriesController.cs
│   ├── ReportsController.cs
│   └── ProfileController.cs
├── Models/               # Các model và ViewModel
│   ├── ApplicationUser.cs
│   ├── Category.cs
│   ├── Expense.cs
│   ├── MonthlyBudget.cs
│   └── ViewModels/
├── Views/                # Các view Razor
│   ├── Account/
│   ├── Dashboard/
│   ├── Expenses/
│   ├── Categories/
│   ├── Reports/
│   ├── Profile/
│   └── Shared/
├── Data/                 # DbContext và Seed data
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
└── Migrations/           # Entity Framework migrations
```

## Tính năng nổi bật

### 🎨 Giao diện thân thiện
- Thiết kế responsive, tương thích mobile
- Sử dụng Tailwind CSS cho UI hiện đại
- Biểu đồ trực quan với Chart.js

### 🔒 Bảo mật
- Xác thực người dùng với ASP.NET Core Identity
- Mã hóa mật khẩu
- Phân quyền truy cập dữ liệu theo user

### 📱 Responsive Design
- Tương thích với desktop, tablet, mobile
- Giao diện tối ưu cho mọi kích thước màn hình

### 🚀 Hiệu suất cao
- Sử dụng Entity Framework Core với SQLite
- Tối ưu truy vấn database
- Caching và lazy loading

## Hướng dẫn sử dụng

### Bước 1: Đăng ký tài khoản
1. Truy cập trang đăng ký
2. Nhập họ tên, email và mật khẩu
3. Hệ thống sẽ tự động tạo các danh mục mặc định

### Bước 2: Thêm chi tiêu
1. Vào trang "Chi tiêu"
2. Nhấn "Thêm chi tiêu"
3. Điền thông tin: tiêu đề, số tiền, danh mục, ngày, ghi chú
4. Lưu giao dịch

### Bước 3: Quản lý danh mục
1. Vào trang "Danh mục"
2. Tạo danh mục mới hoặc chỉnh sửa danh mục có sẵn
3. Thiết lập ngân sách cho danh mục (tùy chọn)

### Bước 4: Xem báo cáo
1. Vào trang "Báo cáo"
2. Xem các biểu đồ và thống kê chi tiết
3. Theo dõi việc sử dụng ngân sách

## Danh mục mặc định

Khi đăng ký tài khoản mới, hệ thống sẽ tự động tạo các danh mục:
- 🍜 Ăn uống
- 🚗 Di chuyển  
- 🎮 Giải trí
- 🛍️ Mua sắm
- 🏥 Sức khỏe
- 📚 Giáo dục

## Lưu ý

- Database SQLite sẽ được tạo tự động tại `expense_tracker.db`
- Dữ liệu được phân tách theo từng user
- Mật khẩu tối thiểu 6 ký tự
- Hỗ trợ tiếng Việt đầy đủ

## Hỗ trợ

Nếu gặp vấn đề trong quá trình sử dụng, vui lòng:
1. Kiểm tra log trong console
2. Đảm bảo .NET 9.0 SDK đã được cài đặt
3. Kiểm tra kết nối database

---

**Phiên bản**: 1.0.0  
**Ngày cập nhật**: 07/09/2025