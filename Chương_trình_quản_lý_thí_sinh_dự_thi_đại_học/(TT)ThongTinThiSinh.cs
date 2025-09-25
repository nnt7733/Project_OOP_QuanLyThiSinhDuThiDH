using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class ThongTinThiSinh
    {
        private static readonly Regex EmailRegex =
            new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex PhoneRegex =
            new Regex(@"^(0|\+84)(\d){8,10}$", RegexOptions.Compiled);

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

        public ThongTinThiSinh(string soBD, string hoTen, DateTime ngaySinh, string danToc, string gioiTinh,
            string noiSinh, string diaChi, string soCanCuoc, string soDienThoai, string email, string khuVuc,
            int doiTuongUuTien, string hoiDongThi)
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
            HoiDongThi = hoiDongThi;
        }

        public virtual void NhapThongTin()
        {
            Console.Write("Nhập số báo danh: ");
            SoBD = DocThongTinKhongRong();

            Console.Write("Nhập họ và tên: ");
            HoTen = DocThongTinKhongRong();

            Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
            NgaySinh = DocNgayThang();

            Console.Write("Nhập dân tộc: ");
            DanToc = DocThongTinKhongRong();

            Console.Write("Nhập giới tính (Nam/Nữ): ");
            GioiTinh = DocGioiTinh();

            Console.Write("Nhập nơi sinh: ");
            NoiSinh = DocThongTinKhongRong();

            Console.Write("Nhập địa chỉ: ");
            DiaChi = DocThongTinKhongRong();

            Console.Write("Nhập số căn cước: ");
            SoCanCuoc = DocThongTinKhongRong();

            Console.Write("Nhập số điện thoại: ");
            SoDienThoai = DocSoDienThoai();

            Console.Write("Nhập email: ");
            Email = DocEmail();

            Console.Write("Nhập khu vực (KV1/KV2/KV2-NT/KV3): ");
            KhuVuc = DocKhuVuc();

            Console.Write("Nhập đối tượng ưu tiên (0/1/2): ");
            DoiTuongUuTien = DocSoNguyenTrongKhoang(0, 2);

            Console.Write("Nhập hội đồng thi: ");
            HoiDongThi = DocThongTinKhongRong();
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

        protected void SaoChepThongTinChungSang(ThongTinThiSinh dich)
        {
            dich.SoBD = SoBD;
            dich.HoTen = HoTen;
            dich.NgaySinh = NgaySinh;
            dich.DanToc = DanToc;
            dich.GioiTinh = GioiTinh;
            dich.NoiSinh = NoiSinh;
            dich.DiaChi = DiaChi;
            dich.SoCanCuoc = SoCanCuoc;
            dich.SoDienThoai = SoDienThoai;
            dich.Email = Email;
            dich.KhuVuc = KhuVuc;
            dich.DoiTuongUuTien = DoiTuongUuTien;
            dich.HoiDongThi = HoiDongThi;
        }

        public void CapNhatThongTinChungTu(ThongTinThiSinh nguon)
        {
            SoBD = nguon.SoBD;
            HoTen = nguon.HoTen;
            NgaySinh = nguon.NgaySinh;
            DanToc = nguon.DanToc;
            GioiTinh = nguon.GioiTinh;
            NoiSinh = nguon.NoiSinh;
            DiaChi = nguon.DiaChi;
            SoCanCuoc = nguon.SoCanCuoc;
            SoDienThoai = nguon.SoDienThoai;
            Email = nguon.Email;
            KhuVuc = nguon.KhuVuc;
            DoiTuongUuTien = nguon.DoiTuongUuTien;
            HoiDongThi = nguon.HoiDongThi;
        }

        public abstract ThongTinThiSinh SaoChep();

        private static string DocThongTinKhongRong()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.Write("Trường này không được bỏ trống. Vui lòng nhập lại: ");
            }
        }

        private static DateTime DocNgayThang()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var result))
                {
                    return result;
                }

                Console.Write("Sai định dạng! Nhập lại (dd/MM/yyyy): ");
            }
        }

        private static int DocSoNguyenTrongKhoang(int min, int max)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out var value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.Write($"Giá trị không hợp lệ! Vui lòng nhập số trong khoảng {min}-{max}: ");
            }
        }

        private static string DocSoDienThoai()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && PhoneRegex.IsMatch(input.Trim()))
                {
                    return input.Trim();
                }

                Console.Write("Số điện thoại không hợp lệ! Nhập lại (hỗ trợ 0xxxxxxxxx hoặc +84xxxxxxxxx): ");
            }
        }

        private static string DocEmail()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && EmailRegex.IsMatch(input.Trim()))
                {
                    return input.Trim();
                }

                Console.Write("Email không hợp lệ! Vui lòng nhập lại: ");
            }
        }

        private static string DocKhuVuc()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Khu vực không được bỏ trống! Nhập lại: ");
                    continue;
                }

                var value = input.Trim().ToUpperInvariant();
                if (value == "KV1" || value == "KV2" || value == "KV2-NT" || value == "KV3")
                {
                    return value;
                }

                Console.Write("Khu vực chỉ chấp nhận KV1/KV2/KV2-NT/KV3. Nhập lại: ");
            }
        }

        private static string DocGioiTinh()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Giới tính không được bỏ trống. Nhập lại (Nam/Nữ): ");
                    continue;
                }

                var value = input.Trim();
                if (string.Equals(value, "Nam", StringComparison.OrdinalIgnoreCase))
                {
                    return "Nam";
                }

                if (string.Equals(value, "Nữ", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(value, "Nu", StringComparison.OrdinalIgnoreCase))
                {
                    return "Nữ";
                }

                Console.Write("Giới tính chỉ chấp nhận Nam hoặc Nữ. Nhập lại: ");
            }
        }
    }
}
