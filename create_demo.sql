-- Tìm UserId của tài khoản vupn240491@tvu-onschool.edu.vn
-- Thêm 10 chi tiêu demo với ngày hiện tại

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Cơm trưa văn phòng',
    '45000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Ăn uống' LIMIT 1),
    '2025-09-07 12:30:00',
    'Cơm gà xối mỡ',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-07 12:30:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Xăng xe máy',
    '120000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Di chuyển' LIMIT 1),
    '2025-09-06 08:15:00',
    'Đổ đầy bình',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-06 08:15:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Mua áo thun',
    '250000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Mua sắm' LIMIT 1),
    '2025-09-05 19:20:00',
    'Áo thun cotton',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-05 19:20:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Xem phim rạp',
    '85000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Giải trí' LIMIT 1),
    '2025-09-04 20:00:00',
    'Phim hành động',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-04 20:00:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Khám răng',
    '300000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Sức khỏe' LIMIT 1),
    '2025-09-03 14:30:00',
    'Lấy cao răng',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-03 14:30:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Cà phê sáng',
    '25000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Ăn uống' LIMIT 1),
    '2025-09-07 07:45:00',
    'Cà phê đen đá',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-07 07:45:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Grab về nhà',
    '35000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Di chuyển' LIMIT 1),
    '2025-09-06 22:30:00',
    'Từ quận 1 về quận 7',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-06 22:30:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Mua sách lập trình',
    '180000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Giáo dục' LIMIT 1),
    '2025-09-02 16:00:00',
    'Sách ASP.NET Core',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-02 16:00:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Ăn tối nhà hàng',
    '320000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Ăn uống' LIMIT 1),
    '2025-09-01 19:30:00',
    'Nhà hàng Hàn Quốc',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-09-01 19:30:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');

INSERT INTO Expenses (Id, Title, Amount, CategoryId, Date, Notes, UserId, CreatedAt) 
SELECT 
    lower(hex(randomblob(16))),
    'Game mobile',
    '50000',
    (SELECT Id FROM Categories WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn') AND Name = 'Giải trí' LIMIT 1),
    '2025-08-31 21:00:00',
    'Nạp thẻ game',
    (SELECT Id FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn'),
    '2025-08-31 21:00:00'
WHERE EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'vupn240491@tvu-onschool.edu.vn');