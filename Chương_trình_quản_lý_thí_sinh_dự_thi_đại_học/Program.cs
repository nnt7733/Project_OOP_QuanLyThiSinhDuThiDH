using System;
using System.Collections.Generic;
using System.Globalization;
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
                        ql.InDanhSach();
                        break;
                    case "5":
                        ql.ThongKeTheoKhoi();
                        break;
                    case "6":
                        ql.TimThuKhoa();
                        break;
                    case "7":
                        TimKiemTheoHoTen(ql);
                        break;
                    case "8":
                        ql.TaiTuTxt(filePath);
                        break;
                    case "9":
                        ql.LuuVaoTxt(filePath);
                        break;
                    case "10":
                        CapNhatThongTinThiSinh(ql, filePath);
                        break;
                    case "11":
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
            Console.WriteLine("4. Hiển thị danh sách thí sinh");
            Console.WriteLine("5. Thống kê số lượng theo khối");
            Console.WriteLine("6. Tìm thủ khoa từng khối");
            Console.WriteLine("7. Tìm kiếm thí sinh theo họ tên");
            Console.WriteLine("8. Tải dữ liệu từ tệp");
            Console.WriteLine("9. Lưu dữ liệu ra tệp");
            Console.WriteLine("10. Cập nhật thông tin thí sinh");
            Console.WriteLine("11. Xóa thí sinh");
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

            bool daCapNhat = false;
            bool tiepTuc = true;

            while (tiepTuc)
            {
                HienThiMenuCapNhat(thiSinhHienTai);
                Console.Write("Chọn mục cần cập nhật (0 để hoàn tất): ");
                string luaChon = Console.ReadLine();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "0":
                        tiepTuc = false;
                        break;
                    case "1":
                        CapNhatSoBaoDanh(ql, ref soBD, thiSinhHienTai, ref daCapNhat);
                        break;
                    case "2":
                        {
                            string hoTenMoi = NhapChuoiKhongRong("Nhập họ và tên mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "họ và tên", ts => ts.HoTen = hoTenMoi);
                            break;
                        }
                    case "3":
                        {
                            NgayThangNam ngaySinhMoi = NhapNgaySinhMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "ngày sinh", ts => ts.NgaySinh = ngaySinhMoi);
                            break;
                        }
                    case "4":
                        {
                            string danTocMoi = NhapChuoiKhongRong("Nhập dân tộc mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "dân tộc", ts => ts.DanToc = danTocMoi);
                            break;
                        }
                    case "5":
                        {
                            string tonGiaoMoi = NhapChuoiKhongRong("Nhập tôn giáo mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "tôn giáo", ts => ts.TonGiao = tonGiaoMoi);
                            break;
                        }
                    case "6":
                        {
                            string gioiTinhMoi = NhapGioiTinhMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "giới tính", ts => ts.GioiTinh = gioiTinhMoi);
                            break;
                        }
                    case "7":
                        {
                            string noiSinhMoi = NhapChuoiKhongRong("Nhập nơi sinh mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "nơi sinh", ts => ts.NoiSinh = noiSinhMoi);
                            break;
                        }
                    case "8":
                        {
                            string diaChiMoi = NhapChuoiKhongRong("Nhập địa chỉ mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "địa chỉ", ts => ts.DiaChi = diaChiMoi);
                            break;
                        }
                    case "9":
                        {
                            string soCanCuocMoi = NhapChuoiKhongRong("Nhập số căn cước mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "số căn cước", ts => ts.SoCanCuoc = soCanCuocMoi);
                            break;
                        }
                    case "10":
                        {
                            string soDienThoaiMoi = NhapSoDienThoaiMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "số điện thoại", ts => ts.SoDienThoai = soDienThoaiMoi);
                            break;
                        }
                    case "11":
                        {
                            string emailMoi = NhapEmailMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "email", ts => ts.Email = emailMoi);
                            break;
                        }
                    case "12":
                        {
                            string khuVucMoi = NhapKhuVucMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "khu vực", ts => ts.KhuVuc = khuVucMoi);
                            break;
                        }
                    case "13":
                        {
                            int doiTuongMoi = NhapDoiTuongUuTienMoi();
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "đối tượng ưu tiên", ts => ts.DoiTuongUuTien = doiTuongMoi);
                            break;
                        }
                    case "14":
                        {
                            string hoiDongMoi = NhapChuoiKhongRong("Nhập hội đồng thi mới: ");
                            CapNhatGiaTri(ql, soBD, ref daCapNhat, "hội đồng thi", ts => ts.HoiDongThi = hoiDongMoi);
                            break;
                        }
                    default:
                        if (!CapNhatDiemThi(ql, ref soBD, thiSinhHienTai, luaChon, ref daCapNhat))
                        {
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        }
                        break;
                }

                soBD = thiSinhHienTai.SoBD;
                Console.WriteLine();
            }

            if (daCapNhat)
            {
                ql.LuuVaoTxt(filePath);
                Console.WriteLine("Cập nhật thí sinh thành công.");
                Console.WriteLine("Dữ liệu đã được lưu vào tệp.");
            }
            else
            {
                Console.WriteLine("Không có thay đổi nào được thực hiện.");
            }
        }

        private static void CapNhatSoBaoDanh(QuanLyThiSinh ql, ref string soBD, ThongTinThiSinh thiSinhHienTai, ref bool daCapNhat)
        {
            string soBDMoi = NhapChuoiKhongRong("Nhập số báo danh mới: ");

            if (!string.Equals(soBDMoi, thiSinhHienTai.SoBD, StringComparison.Ordinal))
            {
                var thiSinhTrung = ql.TimTheoSoBD(soBDMoi);
                if (thiSinhTrung != null && !ReferenceEquals(thiSinhTrung, thiSinhHienTai))
                {
                    Console.WriteLine("Số báo danh đã tồn tại. Vui lòng chọn số khác.");
                    return;
                }
            }

            if (ql.CapNhatThiSinh(soBD, ts => ts.SoBD = soBDMoi))
            {
                daCapNhat = true;
                soBD = thiSinhHienTai.SoBD;
                Console.WriteLine("Đã cập nhật số báo danh.");
            }
            else
            {
                Console.WriteLine("Cập nhật số báo danh thất bại.");
            }
        }

        private static void CapNhatGiaTri(QuanLyThiSinh ql, string soBD, ref bool daCapNhat, string moTaThongTin, Action<ThongTinThiSinh> capNhat)
        {
            if (capNhat == null)
            {
                return;
            }

            if (ql.CapNhatThiSinh(soBD, capNhat))
            {
                daCapNhat = true;
                Console.WriteLine($"Đã cập nhật {moTaThongTin}.");
            }
            else
            {
                Console.WriteLine($"Cập nhật {moTaThongTin} không thành công.");
            }
        }

        private static bool CapNhatDiemThi(QuanLyThiSinh ql, ref string soBD, ThongTinThiSinh thiSinhHienTai, string luaChon, ref bool daCapNhat)
        {
            switch (thiSinhHienTai)
            {
                case ThiSinhKhoiA thiSinhKhoiA:
                    return CapNhatDiemKhoiA(ql, soBD, thiSinhKhoiA, luaChon, ref daCapNhat);
                case ThiSinhKhoiB thiSinhKhoiB:
                    return CapNhatDiemKhoiB(ql, soBD, thiSinhKhoiB, luaChon, ref daCapNhat);
                case ThiSinhKhoiC thiSinhKhoiC:
                    return CapNhatDiemKhoiC(ql, soBD, thiSinhKhoiC, luaChon, ref daCapNhat);
            }

            return false;
        }

        private static bool CapNhatDiemKhoiA(QuanLyThiSinh ql, string soBD, ThiSinhKhoiA thiSinh, string luaChon, ref bool daCapNhat)
        {
            switch (luaChon)
            {
                case "15":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Toán", diem => thiSinh.Diem.Toan = diem);
                case "16":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Lý", diem => thiSinh.Diem.Ly = diem);
                case "17":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Hóa", diem => thiSinh.Diem.Hoa = diem);
            }

            return false;
        }

        private static bool CapNhatDiemKhoiB(QuanLyThiSinh ql, string soBD, ThiSinhKhoiB thiSinh, string luaChon, ref bool daCapNhat)
        {
            switch (luaChon)
            {
                case "15":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Toán", diem => thiSinh.Diem.Toan = diem);
                case "16":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Hóa", diem => thiSinh.Diem.Hoa = diem);
                case "17":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Sinh", diem => thiSinh.Diem.Sinh = diem);
            }

            return false;
        }

        private static bool CapNhatDiemKhoiC(QuanLyThiSinh ql, string soBD, ThiSinhKhoiC thiSinh, string luaChon, ref bool daCapNhat)
        {
            switch (luaChon)
            {
                case "15":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Văn", diem => thiSinh.Diem.Van = diem);
                case "16":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Sử", diem => thiSinh.Diem.Su = diem);
                case "17":
                    return CapNhatDiemMon(ql, soBD, ref daCapNhat, "Địa", diem => thiSinh.Diem.Dia = diem);
            }

            return false;
        }

        private static bool CapNhatDiemMon(QuanLyThiSinh ql, string soBD, ref bool daCapNhat, string tenMon, Action<double> ganDiem)
        {
            double diemMoi = NhapDiemMon(tenMon);

            if (ql.CapNhatThiSinh(soBD, _ => ganDiem(diemMoi)))
            {
                daCapNhat = true;
                Console.WriteLine($"Đã cập nhật điểm {tenMon}.");
                return true;
            }

            Console.WriteLine($"Cập nhật điểm {tenMon} thất bại.");
            return false;
        }

        private static void HienThiMenuCapNhat(ThongTinThiSinh thiSinh)
        {
            Console.WriteLine("===== THÔNG TIN HIỆN TẠI =====");
            Console.WriteLine($"1. Số báo danh     : {thiSinh.SoBD}");
            Console.WriteLine($"2. Họ và tên       : {thiSinh.HoTen}");
            Console.WriteLine($"3. Ngày sinh       : {thiSinh.NgaySinh}");
            Console.WriteLine($"4. Dân tộc         : {thiSinh.DanToc}");
            Console.WriteLine($"5. Tôn giáo        : {thiSinh.TonGiao}");
            Console.WriteLine($"6. Giới tính       : {thiSinh.GioiTinh}");
            Console.WriteLine($"7. Nơi sinh        : {thiSinh.NoiSinh}");
            Console.WriteLine($"8. Địa chỉ         : {thiSinh.DiaChi}");
            Console.WriteLine($"9. Số căn cước     : {thiSinh.SoCanCuoc}");
            Console.WriteLine($"10. Số điện thoại  : {thiSinh.SoDienThoai}");
            Console.WriteLine($"11. Email          : {thiSinh.Email}");
            Console.WriteLine($"12. Khu vực        : {thiSinh.KhuVuc}");
            Console.WriteLine($"13. Đối tượng UT   : {thiSinh.DoiTuongUuTien}");
            Console.WriteLine($"14. Hội đồng thi   : {thiSinh.HoiDongThi}");

            if (thiSinh is ThiSinhKhoiA khoiA)
            {
                Console.WriteLine($"15. Điểm Toán      : {khoiA.Diem.Toan}");
                Console.WriteLine($"16. Điểm Lý        : {khoiA.Diem.Ly}");
                Console.WriteLine($"17. Điểm Hóa       : {khoiA.Diem.Hoa}");
            }
            else if (thiSinh is ThiSinhKhoiB khoiB)
            {
                Console.WriteLine($"15. Điểm Toán      : {khoiB.Diem.Toan}");
                Console.WriteLine($"16. Điểm Hóa       : {khoiB.Diem.Hoa}");
                Console.WriteLine($"17. Điểm Sinh      : {khoiB.Diem.Sinh}");
            }
            else if (thiSinh is ThiSinhKhoiC khoiC)
            {
                Console.WriteLine($"15. Điểm Văn       : {khoiC.Diem.Van}");
                Console.WriteLine($"16. Điểm Sử        : {khoiC.Diem.Su}");
                Console.WriteLine($"17. Điểm Địa       : {khoiC.Diem.Dia}");
            }

            Console.WriteLine("0. Hoàn tất cập nhật");
            Console.WriteLine();
        }

        private static NgayThangNam NhapNgaySinhMoi()
        {
            while (true)
            {
                Console.Write("Nhập ngày sinh mới (dd/mm/yyyy): ");
                string giaTri = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(giaTri))
                {
                    Console.WriteLine("Ngày sinh không được để trống.");
                    continue;
                }

                var parts = giaTri
                    .Split(new[] { '/', '-', '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 3)
                {
                    Console.WriteLine("Định dạng ngày sinh không hợp lệ. Vui lòng nhập lại.");
                    continue;
                }

                if (!int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var ngay) ||
                    !int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var thang) ||
                    !int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var nam))
                {
                    Console.WriteLine("Ngày sinh chỉ được phép chứa số.");
                    continue;
                }

                try
                {
                    return new NgayThangNam(ngay, thang, nam);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Ngày sinh không hợp lệ. Vui lòng nhập lại.");
                }
            }
        }

        private static string NhapChuoiKhongRong(string thongDiep)
        {
            while (true)
            {
                Console.Write(thongDiep);
                string giaTri = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(giaTri))
                {
                    return giaTri.Trim();
                }

                Console.WriteLine("Giá trị không được để trống.");
            }
        }

        private static string NhapGioiTinhMoi()
        {
            while (true)
            {
                Console.Write("Nhập giới tính mới (Nam/Nữ): ");
                string gioiTinh = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(gioiTinh))
                {
                    Console.WriteLine("Giới tính không được để trống.");
                    continue;
                }

                gioiTinh = gioiTinh.Trim();
                if (string.Equals(gioiTinh, "Nam", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(gioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
                {
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    return textInfo.ToTitleCase(gioiTinh.ToLowerInvariant());
                }

                Console.WriteLine("Giới tính chỉ được nhập Nam hoặc Nữ.");
            }
        }

        private static string NhapSoDienThoaiMoi()
        {
            while (true)
            {
                Console.Write("Nhập số điện thoại mới (10-11 số): ");
                string soDienThoai = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(soDienThoai) &&
                    soDienThoai.Length >= 10 &&
                    soDienThoai.Length <= 11 &&
                    long.TryParse(soDienThoai, out _))
                {
                    return soDienThoai;
                }

                Console.WriteLine("Số điện thoại không hợp lệ. Vui lòng nhập lại.");
            }
        }

        private static string NhapEmailMoi()
        {
            while (true)
            {
                Console.Write("Nhập email mới: ");
                string email = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains("."))
                {
                    return email.Trim();
                }

                Console.WriteLine("Email không hợp lệ. Vui lòng nhập lại.");
            }
        }

        private static string NhapKhuVucMoi()
        {
            HashSet<string> khuVucHopLe = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "KV1",
                "KV2",
                "KV2-NT",
                "KV3"
            };

            while (true)
            {
                Console.Write("Nhập khu vực mới (KV1/KV2/KV2-NT/KV3): ");
                string khuVuc = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(khuVuc))
                {
                    khuVuc = khuVuc.Trim().ToUpperInvariant();
                    if (khuVucHopLe.Contains(khuVuc))
                    {
                        return khuVuc;
                    }
                }

                Console.WriteLine("Khu vực không hợp lệ. Vui lòng nhập lại.");
            }
        }

        private static int NhapDoiTuongUuTienMoi()
        {
            while (true)
            {
                Console.Write("Nhập đối tượng ưu tiên mới (0/1/2): ");
                string giaTri = Console.ReadLine();

                if (int.TryParse(giaTri, NumberStyles.Integer, CultureInfo.InvariantCulture, out var doiTuong) &&
                    doiTuong >= 0 && doiTuong <= 2)
                {
                    return doiTuong;
                }

                Console.WriteLine("Đối tượng ưu tiên không hợp lệ. Vui lòng nhập lại.");
            }
        }

        private static double NhapDiemMon(string tenMon)
        {
            while (true)
            {
                Console.Write($"Nhập điểm {tenMon} (0-10): ");
                string giaTri = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(giaTri))
                {
                    Console.WriteLine("Điểm không được để trống.");
                    continue;
                }

                giaTri = giaTri.Replace(',', '.');

                if (double.TryParse(giaTri, NumberStyles.Number, CultureInfo.InvariantCulture, out var diem) &&
                    diem >= 0 && diem <= 10)
                {
                    return diem;
                }

                Console.WriteLine("Điểm không hợp lệ. Vui lòng nhập lại.");
            }
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
