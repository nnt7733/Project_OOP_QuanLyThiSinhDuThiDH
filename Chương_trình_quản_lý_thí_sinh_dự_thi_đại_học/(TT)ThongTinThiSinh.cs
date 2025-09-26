using System;
using System.Collections.Generic;
using System.Globalization;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class ThongTinThiSinh
    {
        public string SoBD { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DanToc { get; set; }
        public string GioiTinh { get; set; }
        public string NoiSinh { get; set; }
        public string DiaChi { get; set; }
        public string SoCanCuoc { get; set; } 
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string KhuVuc { get; set; }
        public int DoiTuongUuTien { get; set; }
        public string HoiDongThi { get; set; }
        public ThongTinThiSinh()
        {
        }
        public ThongTinThiSinh(string soBD,string hoTen,DateTime ngaySinh,string danToc,string gioiTinh,string noiSinh,
            string diaChi, string soCanCuoc, string soDienThoai,string email,string khuVuc,int doiTuongUuTien,
            string hoiDongThi)
        {
            SoBD = soBD;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DanToc = danToc;
            GioiTinh = gioiTinh;
            NoiSinh = noiSinh;
            DiaChi = diaChi;
            SoCanCuoc = soCanCuoc;
            SoDienThoai = soDienThoai; 
            Email = email;
            KhuVuc = khuVuc;
            DoiTuongUuTien = doiTuongUuTien;
            HoiDongThi= hoiDongThi;
        }
        public virtual void NhapThongTin()
        {
            SoBD = NhapChuoiKhongRong("Nhập số báo danh: ");
            HoTen = NhapChuoiKhongRong("Nhập họ và tên: ");

            NgaySinh = NhapNgaySinh();

            DanToc = NhapChuoiKhongRong("Nhập dân tộc: ");
            GioiTinh = NhapGioiTinh();
            NoiSinh = NhapChuoiKhongRong("Nhập nơi sinh: ");
            DiaChi = NhapChuoiKhongRong("Nhập địa chỉ: ");
            SoCanCuoc = NhapChuoiKhongRong("Nhập số căn cước: ");
            SoDienThoai = NhapSoDienThoai();
            Email = NhapEmail();
            KhuVuc = NhapKhuVuc();
            DoiTuongUuTien = NhapDoiTuongUuTien();
            HoiDongThi = NhapChuoiKhongRong("Nhập hội đồng thi: ");
        }
        public virtual void InThongTin()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("       THÔNG TIN THÍ SINH DỰ THI    ");
            Console.WriteLine("====================================");
            Console.WriteLine($"Số báo danh     : {SoBD}");
            Console.WriteLine($"Họ và tên       : {HoTen}");
            Console.WriteLine($"Ngày sinh       : {NgaySinh:dd/MM/yyyy}");
            Console.WriteLine($"Giới tính       : {GioiTinh}");
            Console.WriteLine($"Dân tộc         : {DanToc}");
            Console.WriteLine($"Nơi sinh        : {NoiSinh}");
            Console.WriteLine($"Địa chỉ         : {DiaChi}");
            Console.WriteLine($"Số căn cước     : {SoCanCuoc}");
            Console.WriteLine($"Số điện thoại   : {SoDienThoai}");
            Console.WriteLine($"Email           : {Email}");
            Console.WriteLine($"Khu vực         : {KhuVuc}");
            Console.WriteLine($"Đối tượng UT    : {DoiTuongUuTien}");
            Console.WriteLine($"Hội đồng thi    : {HoiDongThi}");
            Console.WriteLine("====================================");
        }

        protected double TinhDiemCongKhuVuc()
        {
            return KhuVuc switch
            {
                "KV1" => 0.75,
                "KV2" => 0.5,
                "KV2-NT" => 0.25,
                _ => 0
            };
        }

        protected double TinhDiemCongUuTien()
        {
            return DoiTuongUuTien switch
            {
                1 => 2,
                2 => 1,
                _ => 0
            };
        }

        private static string NhapChuoiKhongRong(string thongDiep)
        {
            string? ketQua;
            do
            {
                Console.Write(thongDiep);
                ketQua = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(ketQua));

            return ketQua.Trim();
        }

        private static DateTime NhapNgaySinh()
        {
            Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
            DateTime ngaySinh;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out ngaySinh))
            {
                Console.Write("Sai định dạng! Nhập lại (dd/MM/yyyy): ");
            }

            return ngaySinh;
        }

        private static string NhapGioiTinh()
        {
            string? gioiTinh;
            do
            {
                Console.Write("Nhập giới tính (Nam/Nữ): ");
                gioiTinh = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gioiTinh))
                {
                    continue;
                }

                gioiTinh = gioiTinh.Trim();
            } while (!string.Equals(gioiTinh, "Nam", StringComparison.OrdinalIgnoreCase) &&
                     !string.Equals(gioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase));

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gioiTinh.ToLowerInvariant());
        }

        private static string NhapSoDienThoai()
        {
            string? soDienThoai;
            do
            {
                Console.Write("Nhập số điện thoại (10-11 số): ");
                soDienThoai = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(soDienThoai) || soDienThoai.Length < 10 || soDienThoai.Length > 11 ||
                     !long.TryParse(soDienThoai, out _));

            return soDienThoai;
        }

        private static string NhapEmail()
        {
            string? email;
            do
            {
                Console.Write("Nhập email: ");
                email = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."));

            return email.Trim();
        }

        private static string NhapKhuVuc()
        {
            var khuVucHopLe = new HashSet<string> { "KV1", "KV2", "KV2-NT", "KV3" };
            string? khuVuc;
            do
            {
                Console.Write("Nhập khu vực (KV1/KV2/KV2-NT/KV3): ");
                khuVuc = Console.ReadLine()?.ToUpperInvariant();
            } while (string.IsNullOrWhiteSpace(khuVuc) || !khuVucHopLe.Contains(khuVuc));

            return khuVuc;
        }

        private static int NhapDoiTuongUuTien()
        {
            int doiTuong;
            Console.Write("Nhập đối tượng ưu tiên (0/1/2): ");
            while (!int.TryParse(Console.ReadLine(), out doiTuong) || doiTuong < 0 || doiTuong > 2)
            {
                Console.Write("Sai định dạng! Nhập lại đối tượng ưu tiên (0/1/2): ");
            }

            return doiTuong;
        }
    }
}
