# # IT008.M11 - Lập trình trực quan
## 1. Danh sách thành viên
| No. | Student ID | Full name | Class | Role |
| --- | --- | --- | --- | --- |
| 1 | 19522487 | Võ Minh Tuấn | CNPM2019 | Leader |
| 2 | 19520289 | Hồ Quốc Thư | KHTN2019 | Member |
| 3 | 19522461 | Võ Xuân Tú | CNPM2019 | Member |
| 4 | 19521316 | Nguyễn Hải Đăng | KHTN2019 | Member |
| 5 | 19522489 | Đặng Quang Anh Tuấn | KHTN2019 | Member |
## 2. Giới thiệu đề tài
- Phần mềm quản lí cửa hàng tạp hóa đem đến một hình ảnh mới lạ về phong cách phục vụ, hệ thông thiết bị bán hàng hiện đại, nhanh chóng, chính xác với một cách thức làm việc chuyên nghiệp. Giúp của hàng tiết kiệm thời gian công sức, nhất là khâu thanh toán để tập trung vào tư vấn sản phẩm, đóng gói, sắp xếp trưng bày hàng hóa một cách thu hút. Với phần mềm quản lí của hàng tạp hóa, nhà bán lẻ có thể quản lí chặt chẽ hàng hóa, tránh thất thoát trong kinh doanh.
## 3.Danh sách các yêu cầu chức năng, phi chức năng
* Danh sách các yêu cầu nghiệp vụ:
	- Lập danh mục hàng hóa: Cung cấp thông tin về hàng hóa và đưa vào danh sách.
	- Lập hóa đơn: Cung cấp tin hàng hóa, số lượng, mã giảm giá( nếu có).
	- Lập danh sách nhân viên: Cung cấp thông tin nhân viên vào đưa vào danh sách.
	- Lập báo cáo tháng: Nhập thông tin tháng, năm cần báo cáo.
	- Lập phiếu nhập kho: Nhập thông tin hàng hóa, số lượng, ngày tháng nhập kho.
	- Tra cứu hàng hóa: Nhập thông tin hàng hóa cần tra cứu.
	- Thay đổi quy định: Cung cấp giá trị mới và thay đổ quy định.
* Danh sách các yêu cầu tiến hóa:
	- Thay đổi danh mục hàng hóa: Cho biết giá trị mới của tên hàng hóa, đơn giá, số lượng.
	- Thay đổi danh mục nhân viên: Cho biết giá trị mới về thông tin nhân viên, chức vụ.
	- Thay đổi danh mục mã giảm giá: Cho biết giá trị mới của mã giảm giá, thời gian áp dụng, đối tượng áp dụng.
* Yêu cầu phi chức năng: giao diện thân thiện người dùng, tốc độ xử lý nhanh,...
## 4.Kiến trúc hệ thống 
![image](https://user-images.githubusercontent.com/80675685/146349366-08bade46-b3da-479e-86e6-2585e702827e.png)

## 5.Cơ sở dữ liệu
https://drive.google.com/drive/u/0/folders/1ximBSLdWrYnvpIH1hI58SrC-j7JWGXAV?fbclid=IwAR3WjEPKIY44tC2--rkvDe5PgXo4trGLYVS7kelPs0PJAXbMhO0fYGW6QE8
## 6.Thiết kế giao diện
https://www.figma.com/file/2zDPmEUCydutNOwzeC3oP6/Untitled?node-id=0%3A1
## 7.Các thư viện bên ngoài
  	- BouncyCastle
	- EntityFramework
	- iTextSharp
	- LiveCharts
	- LiveCharts.Wpf
	- MateriaDesignColors
	- MaterialDesignThemes
	- System.Windows.Interactivity.WPF
	- ToastNotifications
	- ToastNotifications.Messages
