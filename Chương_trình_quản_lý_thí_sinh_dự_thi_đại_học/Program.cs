using System;
using System.IO;
using System.Linq;
using System.Text;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            const string dataDirectory = "Data";
            const string dataFileName = "ThiSinh.txt";
            string filePath = Path.Combine(dataDirectory, dataFileName);

            Directory.CreateDirectory(dataDirectory);

            QuanLyThiSinh ql = new QuanLyThiSinh();
            ql.TaiTuTxt(filePath);

            bool thoat = false;
            while (!thoat)
            {
                HienThiMenu();
                Console.Write("Chọn chức năng: ");
                string luaChon = Console.ReadLine();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "1":
                        ThemThiSinhKhoiA(ql, filePath);
                        break;
                    case "2":
                        ThemThiSinhKhoiB(ql, filePath);
                        break;
                    case "3":
                        ThemThiSinhKhoiC(ql, filePath);
                        break;
                    case "4":
                        ThemThiSinhKhoiD(ql, filePath);
                        break;
                    case "5":
                        ql.InDanhSach();
                        break;
                    case "6":
                        ql.ThongKeTheoKhoi();
                        break;
                    case "7":
                        ql.TimThuKhoa();
                        break;
                    case "8":
                        TimKiemTheoHoTen(ql);
                        break;
                    case "9":
                        ql.TaiTuTxt(filePath);
                        break;
                    case "10":
                        ql.LuuVaoTxt(filePath);
                        break;
                    case "11":
                        CapNhatThongTinThiSinh(ql, filePath);
                        break;
                    case "12":
                        XoaThiSinh(ql, filePath);
                        break;
                    case "0":
                        thoat = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void HienThiMenu()
        {
            Console.WriteLine("===== CHƯƠNG TRÌNH QUẢN LÝ THÍ SINH =====");
            Console.WriteLine("1. Thêm thí sinh khối A");
            Console.WriteLine("2. Thêm thí sinh khối B");
            Console.WriteLine("3. Thêm thí sinh khối C");
            Console.WriteLine("4. Thêm thí sinh khối D");
            Console.WriteLine("5. Hiển thị danh sách thí sinh");
            Console.WriteLine("6. Thống kê số lượng theo khối");
            Console.WriteLine("7. Tìm thủ khoa từng khối");
            Console.WriteLine("8. Tìm kiếm thí sinh theo họ tên");
            Console.WriteLine("9. Tải dữ liệu từ tệp");
            Console.WriteLine("10. Lưu dữ liệu ra tệp");
            Console.WriteLine("11. Cập nhật thông tin thí sinh");
            Console.WriteLine("12. Xóa thí sinh");
            Console.WriteLine("0. Thoát");
        }

        private static void ThemThiSinhKhoiA(QuanLyThiSinh ql, string filePath)
        {
            ThiSinhKhoiA thiSinh = NhapThiSinhKhoiA();
            ThemThiSinh(ql, filePath, thiSinh);
        }

        private static void ThemThiSinhKhoiB(QuanLyThiSinh ql, string filePath)
        {
            ThiSinhKhoiB thiSinh = NhapThiSinhKhoiB();
            ThemThiSinh(ql, filePath, thiSinh);
        }

        private static void ThemThiSinhKhoiC(QuanLyThiSinh ql, string filePath)
        {
            ThiSinhKhoiC thiSinh = NhapThiSinhKhoiC();
            ThemThiSinh(ql, filePath, thiSinh);
        }

        private static void ThemThiSinhKhoiD(QuanLyThiSinh ql, string filePath)
        {
            ThiSinhKhoiD thiSinh = NhapThiSinhKhoiD();
            ThemThiSinh(ql, filePath, thiSinh);
        }

        private static ThiSinhKhoiA NhapThiSinhKhoiA()
        {
            ThiSinhKhoiA thiSinh = new ThiSinhKhoiA();
            thiSinh.Nhap();
            return thiSinh;
        }

        private static ThiSinhKhoiB NhapThiSinhKhoiB()
        {
            ThiSinhKhoiB thiSinh = new ThiSinhKhoiB();
            thiSinh.Nhap();
            return thiSinh;
        }

        private static ThiSinhKhoiC NhapThiSinhKhoiC()
        {
            ThiSinhKhoiC thiSinh = new ThiSinhKhoiC();
            thiSinh.Nhap();
            return thiSinh;
        }

        private static ThiSinhKhoiD NhapThiSinhKhoiD()
        {
            ThiSinhKhoiD thiSinh = new ThiSinhKhoiD();
            thiSinh.Nhap();
            return thiSinh;
        }

        private static void ThemThiSinh(QuanLyThiSinh ql, string filePath, ThongTinThiSinh thiSinh)
        {
            if (thiSinh == null)
            {
                Console.WriteLine("Không có thông tin thí sinh để thêm.");
                return;
            }

            string khoi = thiSinh switch
            {
                ThiSinhKhoiA _ => "A",
                ThiSinhKhoiB _ => "B",
                ThiSinhKhoiC _ => "C",
                ThiSinhKhoiD _ => "D",
                _ => string.Empty
            };

            if (!ql.ThemThiSinh(thiSinh))
            {
                Console.WriteLine($"Số báo danh đã tồn tại, không thể thêm thí sinh khối {khoi}.");
                return;
            }

            Console.WriteLine($"Đã thêm thí sinh khối {khoi} thành công.");
            ql.LuuVaoTxt(filePath);
            Console.WriteLine("Dữ liệu đã được lưu vào tệp.");
        }

        private static void TimKiemTheoHoTen(QuanLyThiSinh ql)
        {
            Console.Write("Nhập tên cần tìm kiếm: ");
            string tuKhoa = Console.ReadLine();
            var ketQua = ql.TimTheoHoTen(tuKhoa).ToList();

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy thí sinh phù hợp.");
                return;
            }

            Console.WriteLine("===== KẾT QUẢ TÌM KIẾM =====");
            foreach (var ts in ketQua)
            {
                ts.InThongTin();
                Console.WriteLine();
            }
        }

        private static void CapNhatThongTinThiSinh(QuanLyThiSinh ql, string filePath)
        {
            Console.Write("Nhập số báo danh cần cập nhật: ");
            string soBD = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(soBD))
            {
                Console.WriteLine("Số báo danh không hợp lệ.");
                return;
            }

            var thiSinhHienTai = ql.TimTheoSoBD(soBD);
            if (thiSinhHienTai == null)
            {
                Console.WriteLine("Không tìm thấy thí sinh với số báo danh đã nhập.");
                return;
            }

            Console.WriteLine("Nhập thông tin mới cho thí sinh (các thông tin cũ sẽ được thay thế).");

            ThongTinThiSinh thongTinCapNhat;
            if (thiSinhHienTai is ThiSinhKhoiA)
            {
                thongTinCapNhat = NhapThiSinhKhoiA();
            }
            else if (thiSinhHienTai is ThiSinhKhoiB)
            {
                thongTinCapNhat = NhapThiSinhKhoiB();
            }
            else if (thiSinhHienTai is ThiSinhKhoiC)
            {
                thongTinCapNhat = NhapThiSinhKhoiC();
            }
            else if (thiSinhHienTai is ThiSinhKhoiD)
            {
                thongTinCapNhat = NhapThiSinhKhoiD();
            }
            else
            {
                Console.WriteLine("Loại thí sinh không xác định, không thể cập nhật.");
                return;
            }

            if (!ql.CapNhatThiSinh(soBD, thongTinCapNhat))
            {
                Console.WriteLine("Cập nhật thất bại. Có thể số báo danh mới đã tồn tại hoặc loại khối không khớp.");
                return;
            }

            Console.WriteLine("Cập nhật thí sinh thành công.");
            ql.LuuVaoTxt(filePath);
            Console.WriteLine("Dữ liệu đã được lưu vào tệp.");
        }

        private static void XoaThiSinh(QuanLyThiSinh ql, string filePath)
        {
            Console.Write("Nhập số báo danh cần xóa: ");
            string soBD = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(soBD))
            {
                Console.WriteLine("Số báo danh không hợp lệ.");
                return;
            }

            if (!ql.XoaThiSinh(soBD))
            {
                Console.WriteLine("Không tìm thấy thí sinh với số báo danh đã nhập.");
                return;
            }

            Console.WriteLine($"Đã xóa thí sinh có số báo danh {soBD}.");
            ql.LuuVaoTxt(filePath);
            Console.WriteLine("Dữ liệu đã được lưu vào tệp.");
        }
    }
}
