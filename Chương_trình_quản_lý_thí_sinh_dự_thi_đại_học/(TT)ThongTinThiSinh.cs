using System;
using System.Collections.Generic;
using System.Globalization;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class ThongTinThiSinh
    {
        public string SoBD { get; set; }
        public string HoTen { get; set; }
        public NgayThangNam NgaySinh { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
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

        public ThongTinThiSinh(
            string soBD,
            string hoTen,
            NgayThangNam ngaySinh,
            string danToc,
            string tonGiao,
            string gioiTinh,
            string noiSinh,
            string diaChi,
            string soCanCuoc,
            string soDienThoai,
            string email,
            string khuVuc,
            int doiTuongUuTien,
            string hoiDongThi)
        {
            SoBD = soBD;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DanToc = danToc;
            TonGiao = tonGiao;
            GioiTinh = gioiTinh;
            NoiSinh = noiSinh;
            DiaChi = diaChi;
            SoCanCuoc = soCanCuoc;
            SoDienThoai = soDienThoai;
            Email = email;
            KhuVuc = khuVuc;
            DoiTuongUuTien = doiTuongUuTien;
            HoiDongThi = hoiDongThi;
        }

        public virtual void NhapThongTin(Func<string, bool> kiemTraSoBaoDanhTonTai = null)
        {
            SoBD = NhapSoBaoDanhHopLe(kiemTraSoBaoDanhTonTai);
            HoTen = NhapChuoiKhongRong("Nhập họ và tên: ");

            NgaySinh = NhapNgaySinh();

            DanToc = NhapChuoiKhongRong("Nhập dân tộc: ");
            TonGiao = NhapChuoiKhongRong("Nhập tôn giáo: ");
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
            Console.WriteLine("Số báo danh     : {0}", SoBD);
            Console.WriteLine("Họ và tên       : {0}", HoTen);
            Console.WriteLine("Ngày sinh       : {0}", NgaySinh);
            Console.WriteLine("Giới tính       : {0}", GioiTinh);
            Console.WriteLine("Dân tộc         : {0}", DanToc);
            Console.WriteLine("Tôn giáo        : {0}", TonGiao);
            Console.WriteLine("Nơi sinh        : {0}", NoiSinh);
            Console.WriteLine("Địa chỉ         : {0}", DiaChi);
            Console.WriteLine("Số căn cước     : {0}", SoCanCuoc);
            Console.WriteLine("Số điện thoại   : {0}", SoDienThoai);
            Console.WriteLine("Email           : {0}", Email);
            Console.WriteLine("Khu vực         : {0}", KhuVuc);
            Console.WriteLine("Đối tượng UT    : {0}", DoiTuongUuTien);
            Console.WriteLine("Hội đồng thi    : {0}", HoiDongThi);
            Console.WriteLine("====================================");
        }

        public double TinhDiemCongKhuVuc()
        {
            switch (KhuVuc)
            {
                case "KV1":
                    return 0.75;
                case "KV2":
                    return 0.5;
                case "KV2-NT":
                    return 0.25;
                default:
                    return 0;
            }
        }

        public double TinhDiemCongUuTien()
        {
            switch (DoiTuongUuTien)
            {
                case 1:
                    return 2;
                case 2:
                    return 1;
                default:
                    return 0;
            }
        }

        public double TinhTongDiemUuTien(double tongDiemBaMon)
        {
            var diemUuTienCoBan = TinhDiemCongKhuVuc() + TinhDiemCongUuTien();
            if (diemUuTienCoBan <= 0)
            {
                return 0;
            }

            if (tongDiemBaMon < 22.5)
            {
                return Math.Round(diemUuTienCoBan, 2, MidpointRounding.AwayFromZero);
            }

            var heSo = (30 - tongDiemBaMon) / 7.5;
            if (heSo <= 0)
            {
                return 0;
            }

            if (heSo > 1)
            {
                heSo = 1;
            }

            var diemUuTien = diemUuTienCoBan * heSo;
            return Math.Round(diemUuTien, 2, MidpointRounding.AwayFromZero);
        }

        private static string NhapChuoiKhongRong(string thongDiep)
        {
            string ketQua;
            do
            {
                Console.Write(thongDiep);
                ketQua = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(ketQua));

            return ketQua.Trim();
        }

        private static string NhapSoBaoDanhHopLe(Func<string, bool> kiemTraSoBaoDanhTonTai)
        {
            while (true)
            {
                var soBaoDanh = NhapChuoiKhongRong("Nhập số báo danh: ");

                if (kiemTraSoBaoDanhTonTai != null && kiemTraSoBaoDanhTonTai(soBaoDanh))
                {
                    Console.WriteLine("Số báo danh đã tồn tại. Vui lòng nhập lại.");
                    continue;
                }

                return soBaoDanh;
            }
        }

        private static NgayThangNam NhapNgaySinh()
        {
            while (true)
            {
                Console.WriteLine("Nhập ngày tháng năm sinh");
                var ngay = NhapSoNguyen("Nhập ngày: ");
                var thang = NhapSoNguyen("Nhập tháng: ");
                var nam = NhapSoNguyen("Nhập năm: ");

                try
                {
                    return new NgayThangNam(ngay, thang, nam);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Ngày sinh không hợp lệ, vui lòng nhập lại.");
                }
            }
        }

        private static int NhapSoNguyen(string thongDiep)
        {
            while (true)
            {
                Console.Write(thongDiep);
                var duLieu = Console.ReadLine();
                if (int.TryParse(duLieu, NumberStyles.Integer, CultureInfo.InvariantCulture, out var giaTri))
                {
                    return giaTri;
                }

                Console.WriteLine("Giá trị phải là số nguyên. Vui lòng nhập lại.");
            }
        }

        private static string NhapGioiTinh()
        {
            string gioiTinh;
            while (true)
            {
                Console.Write("Nhập giới tính (Nam/Nữ): ");
                gioiTinh = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gioiTinh))
                {
                    continue;
                }

                gioiTinh = gioiTinh.Trim();
                if (string.Equals(gioiTinh, "Nam", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(gioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
                {
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    return textInfo.ToTitleCase(gioiTinh.ToLowerInvariant());
                }
            }
        }

        private static string NhapSoDienThoai()
        {
            string soDienThoai;
            while (true)
            {
                Console.Write("Nhập số điện thoại (10-11 số): ");
                soDienThoai = Console.ReadLine();
                long kiemTra;
                if (!string.IsNullOrWhiteSpace(soDienThoai) &&
                    soDienThoai.Length >= 10 &&
                    soDienThoai.Length <= 11 &&
                    long.TryParse(soDienThoai, out kiemTra))
                {
                    return soDienThoai;
                }
            }
        }

        private static string NhapEmail()
        {
            string email;
            while (true)
            {
                Console.Write("Nhập email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains("."))
                {
                    return email.Trim();
                }
            }
        }

        private static string NhapKhuVuc()
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
                Console.Write("Nhập khu vực (KV1/KV2/KV2-NT/KV3): ");
                string khuVuc = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(khuVuc))
                {
                    khuVuc = khuVuc.Trim().ToUpperInvariant();
                    if (khuVucHopLe.Contains(khuVuc))
                    {
                        return khuVuc;
                    }
                }
            }
        }

        private static int NhapDoiTuongUuTien()
        {
            Console.Write("Nhập đối tượng ưu tiên (0/1/2): ");
            int doiTuong;
            while (true)
            {
                string giaTri = Console.ReadLine();
                if (int.TryParse(giaTri, out doiTuong) && doiTuong >= 0 && doiTuong <= 2)
                {
                    return doiTuong;
                }

                Console.Write("Sai định dạng! Nhập lại đối tượng ưu tiên (0/1/2): ");
            }
        }
    }
}
