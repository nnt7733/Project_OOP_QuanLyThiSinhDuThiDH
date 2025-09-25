using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    internal class Program
    {
        private static readonly string[] MonThi =
        {
            "Toan", "Van", "Anh", "Ly", "Hoa", "Sinh", "Su", "Dia", "GDCD"
        };

        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var quanLy = new QuanLyThiSinh();
            var dataService = new CandidateDataService();

            while (true)
            {
                HienThiMenu();
                Console.Write("Chọn chức năng: ");
                var luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1":
                        ThemThiSinh(quanLy, new ThiSinhKhoiA());
                        break;
                    case "2":
                        ThemThiSinh(quanLy, new ThiSinhKhoiB());
                        break;
                    case "3":
                        ThemThiSinh(quanLy, new ThiSinhKhoiC());
                        break;
                    case "4":
                        quanLy.InDanhSach();
                        break;
                    case "5":
                        ThucHienTimKiem(quanLy);
                        break;
                    case "6":
                        ThucHienSapXep(quanLy);
                        break;
                    case "7":
                        CapNhatThiSinh(quanLy);
                        break;
                    case "8":
                        XoaThiSinh(quanLy);
                        break;
                    case "9":
                        quanLy.ThongKeTheoKhoi();
                        break;
                    case "10":
                        HienThiThongKeNangCao(quanLy);
                        break;
                    case "11":
                        XuatDuLieu(dataService, quanLy, true);
                        break;
                    case "12":
                        NhapDuLieu(dataService, quanLy, true);
                        break;
                    case "13":
                        XuatDuLieu(dataService, quanLy, false);
                        break;
                    case "14":
                        NhapDuLieu(dataService, quanLy, false);
                        break;
                    case "15":
                        XemNhatKy(quanLy);
                        break;
                    case "16":
                        if (!quanLy.Undo())
                        {
                            Console.WriteLine("Không còn thao tác để hoàn tác.");
                        }
                        break;
                    case "17":
                        if (!quanLy.Redo())
                        {
                            Console.WriteLine("Không còn thao tác để làm lại.");
                        }
                        break;
                    case "18":
                        quanLy.TimThuKhoa();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void HienThiMenu()
        {
            Console.WriteLine("================ MENU QUẢN LÝ THÍ SINH ================");
            Console.WriteLine("1. Thêm thí sinh khối A");
            Console.WriteLine("2. Thêm thí sinh khối B");
            Console.WriteLine("3. Thêm thí sinh khối C");
            Console.WriteLine("4. In danh sách thí sinh");
            Console.WriteLine("5. Tìm kiếm nâng cao");
            Console.WriteLine("6. Sắp xếp danh sách");
            Console.WriteLine("7. Cập nhật thông tin thí sinh");
            Console.WriteLine("8. Xóa thí sinh");
            Console.WriteLine("9. Thống kê cơ bản theo khối");
            Console.WriteLine("10. Thống kê nâng cao");
            Console.WriteLine("11. Xuất dữ liệu ra CSV");
            Console.WriteLine("12. Nhập dữ liệu từ CSV");
            Console.WriteLine("13. Xuất dữ liệu ra JSON");
            Console.WriteLine("14. Nhập dữ liệu từ JSON");
            Console.WriteLine("15. Xem nhật ký thao tác");
            Console.WriteLine("16. Hoàn tác thao tác gần nhất");
            Console.WriteLine("17. Làm lại thao tác vừa hoàn tác");
            Console.WriteLine("18. Hiển thị thủ khoa từng khối");
            Console.WriteLine("0. Thoát");
            Console.WriteLine("=======================================================");
        }

        private static void ThemThiSinh(QuanLyThiSinh quanLy, ThongTinThiSinh thiSinh)
        {
            try
            {
                switch (thiSinh)
                {
                    case ThiSinhKhoiA khoiA:
                        khoiA.Nhap();
                        quanLy.ThemThiSinh(khoiA);
                        break;
                    case ThiSinhKhoiB khoiB:
                        khoiB.Nhap();
                        quanLy.ThemThiSinh(khoiB);
                        break;
                    case ThiSinhKhoiC khoiC:
                        khoiC.Nhap();
                        quanLy.ThemThiSinh(khoiC);
                        break;
                }
                Console.WriteLine("Thêm thí sinh thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private static void ThucHienTimKiem(QuanLyThiSinh quanLy)
        {
            Console.WriteLine("--- TÌM KIẾM NÂNG CAO ---");
            Console.Write("Nhập từ khóa họ tên (bỏ trống nếu không): ");
            var hoTen = DocThongTinTuTuyChon();

            Console.Write("Nhập khu vực lọc (KV1/KV2/KV2-NT/KV3, bỏ trống nếu không): ");
            var khuVuc = DocThongTinTuTuyChon();

            Console.Write("Nhập giới tính lọc (Nam/Nữ, bỏ trống nếu không): ");
            var gioiTinh = DocThongTinTuTuyChon();

            var diemToiThieu = new Dictionary<string, double>();
            foreach (var mon in MonThi)
            {
                Console.Write($"Điểm tối thiểu môn {mon} (bỏ trống nếu không): ");
                var diem = DocDoubleTuTuyChon();
                if (diem.HasValue)
                {
                    diemToiThieu[mon] = diem.Value;
                }
            }

            Console.Write("Tổng điểm tối thiểu (bỏ trống nếu không): ");
            var tongDiem = DocDoubleTuTuyChon();

            var ketQua = quanLy.TimKiemNangCao(hoTen, khuVuc, gioiTinh, diemToiThieu, tongDiem);
            HienThiDanhSach(ketQua);
        }

        private static void ThucHienSapXep(QuanLyThiSinh quanLy)
        {
            Console.WriteLine("--- SẮP XẾP DANH SÁCH ---");
            Console.WriteLine("1. Theo tổng điểm");
            Console.WriteLine("2. Theo họ tên");
            Console.WriteLine("3. Theo ngày sinh");
            Console.Write("Chọn tiêu chí: ");
            var luaChon = Console.ReadLine();

            Console.Write("Sắp xếp tăng dần? (y/n): ");
            var tangDan = Console.ReadLine();
            var laTangDan = string.Equals(tangDan, "y", StringComparison.OrdinalIgnoreCase);

            var tieuChi = luaChon switch
            {
                "1" => TieuChiSapXep.TongDiem,
                "2" => TieuChiSapXep.HoTen,
                "3" => TieuChiSapXep.NgaySinh,
                _ => TieuChiSapXep.HoTen
            };

            var ketQua = quanLy.SapXepTheo(tieuChi, laTangDan);
            HienThiDanhSach(ketQua);
        }

        private static void CapNhatThiSinh(QuanLyThiSinh quanLy)
        {
            Console.Write("Nhập số báo danh cần cập nhật: ");
            var soBD = Console.ReadLine();
            var hienTai = quanLy.TimTheoSoBD(soBD);
            if (hienTai == null)
            {
                Console.WriteLine("Không tìm thấy thí sinh.");
                return;
            }

            Console.WriteLine("Nhập lại toàn bộ thông tin cho thí sinh.");
            ThongTinThiSinh moi = hienTai switch
            {
                ThiSinhKhoiA => new ThiSinhKhoiA(),
                ThiSinhKhoiB => new ThiSinhKhoiB(),
                ThiSinhKhoiC => new ThiSinhKhoiC(),
                _ => null
            };

            if (moi == null)
            {
                Console.WriteLine("Không hỗ trợ cập nhật cho loại thí sinh này.");
                return;
            }

            switch (moi)
            {
                case ThiSinhKhoiA khoiA:
                    khoiA.Nhap();
                    break;
                case ThiSinhKhoiB khoiB:
                    khoiB.Nhap();
                    break;
                case ThiSinhKhoiC khoiC:
                    khoiC.Nhap();
                    break;
            }

            try
            {
                quanLy.CapNhatThiSinh(soBD, moi);
                Console.WriteLine("Cập nhật thí sinh thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private static void XoaThiSinh(QuanLyThiSinh quanLy)
        {
            Console.Write("Nhập số báo danh cần xóa: ");
            var soBD = Console.ReadLine();
            if (!quanLy.XoaThiSinh(soBD))
            {
                Console.WriteLine("Không tìm thấy thí sinh để xóa.");
            }
            else
            {
                Console.WriteLine("Đã xóa thí sinh.");
            }
        }

        private static void HienThiThongKeNangCao(QuanLyThiSinh quanLy)
        {
            var thongKe = quanLy.ThongKeNangCao();
            Console.WriteLine($"Tổng số thí sinh: {thongKe.TongSoThiSinh}");
            Console.WriteLine("-- Điểm trung bình theo khối --");
            foreach (var kv in thongKe.DiemTrungBinhTheoKhoi)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value:0.00}");
            }

            Console.WriteLine("-- Thống kê theo môn --");
            foreach (var kv in thongKe.ThongKeTheoMon.OrderBy(x => x.Key))
            {
                var mon = kv.Value;
                Console.WriteLine(
                    $"{mon.Mon}: TB {mon.DiemTrungBinh:0.00} | Cao nhất {mon.DiemCaoNhat:0.00} | Thấp nhất {mon.DiemThapNhat:0.00} | Số bài {mon.SoLuong}");
            }
        }

        private static void XuatDuLieu(CandidateDataService dataService, QuanLyThiSinh quanLy, bool laCsv)
        {
            Console.Write(laCsv ? "Nhập đường dẫn file CSV cần xuất: " : "Nhập đường dẫn file JSON cần xuất: ");
            var path = Console.ReadLine();
            try
            {
                if (laCsv)
                {
                    dataService.XuatCsv(quanLy.DanhSach, path);
                }
                else
                {
                    dataService.XuatJson(quanLy.DanhSach, path);
                }
                Console.WriteLine("Xuất dữ liệu thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xuất dữ liệu: {ex.Message}");
            }
        }

        private static void NhapDuLieu(CandidateDataService dataService, QuanLyThiSinh quanLy, bool laCsv)
        {
            Console.Write(laCsv ? "Nhập đường dẫn file CSV cần nhập: " : "Nhập đường dẫn file JSON cần nhập: ");
            var path = Console.ReadLine();
            try
            {
                var duLieu = laCsv ? dataService.NhapCsv(path) : dataService.NhapJson(path);
                foreach (var ts in duLieu)
                {
                    quanLy.ThemHoacCapNhat(ts);
                }
                Console.WriteLine($"Đã nhập {duLieu.Count} thí sinh từ file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi nhập dữ liệu: {ex.Message}");
            }
        }

        private static void XemNhatKy(QuanLyThiSinh quanLy)
        {
            Console.WriteLine("--- NHẬT KÝ THAO TÁC ---");
            if (!quanLy.NhatKy.Any())
            {
                Console.WriteLine("Chưa có thao tác nào.");
                return;
            }

            foreach (var dong in quanLy.NhatKy)
            {
                Console.WriteLine(dong);
            }
        }

        private static string DocThongTinTuTuyChon()
        {
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }

        private static double? DocDoubleTuTuyChon()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (double.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                {
                    return value;
                }

                Console.Write("Giá trị không hợp lệ. Nhập lại hoặc bỏ trống để bỏ qua: ");
            }
        }

        private static void HienThiDanhSach(IEnumerable<ThongTinThiSinh> danhSach)
        {
            var list = danhSach.ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("Không có thí sinh phù hợp.");
                return;
            }

            foreach (var ts in list)
            {
                switch (ts)
                {
                    case ThiSinhKhoiA khoiA:
                        khoiA.In();
                        break;
                    case ThiSinhKhoiB khoiB:
                        khoiB.In();
                        break;
                    case ThiSinhKhoiC khoiC:
                        khoiC.In();
                        break;
                    default:
                        ts.InThongTin();
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
