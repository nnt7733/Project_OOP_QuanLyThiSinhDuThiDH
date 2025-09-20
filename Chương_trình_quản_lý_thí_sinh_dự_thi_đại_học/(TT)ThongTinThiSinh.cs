using System;
using System.Collections.Generic;
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
        public List<NguyenVong> NguyenVongs { get; set; }
        public ThongTinThiSinh()
        {
            NguyenVongs = new List<NguyenVong>();
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
            NguyenVongs = new List<NguyenVong>();
        }
        public virtual void NhapThongTin()
        {

            Console.Write("Nhập số báo danh: ");
            SoBD = Console.ReadLine();
            Console.Write("Nhập họ và tên: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
            DateTime ngaySinh;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out ngaySinh))
            {
                Console.Write("Sai định dạng! Nhập lại (dd/MM/yyyy): ");
            }
            NgaySinh = ngaySinh;
            Console.Write("Nhập dân tộc: ");
            DanToc = Console.ReadLine();
            Console.Write("Nhập giới tính (Nam/Nữ): ");
            GioiTinh = Console.ReadLine();
            Console.Write("Nhập nơi sinh: ");
            NoiSinh = Console.ReadLine();
            Console.Write("Nhập địa chỉ: ");
            DiaChi = Console.ReadLine();
            Console.Write("Nhập số căn cước: ");
            SoCanCuoc = Console.ReadLine();
            Console.Write("Nhập số điện thoại: ");
            SoDienThoai = Console.ReadLine();
            Console.Write("Nhập email: ");
            string emailInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(emailInput) || !emailInput.Contains("@"))
            {
                Console.Write("Email không hợp lệ! Vui lòng nhập lại: ");
                emailInput = Console.ReadLine();
            }
            Email = emailInput;
            Console.Write("Nhập khu vực (KV1/KV2/KV2-NT/KV3): ");
            KhuVuc = Console.ReadLine();
            Console.Write("Nhập đối tượng ưu tiên (0/1/2): ");
            int doiTuong;
            while (!int.TryParse(Console.ReadLine(), out doiTuong) || doiTuong < 0 || doiTuong > 2)
            {
                Console.Write("Sai định dạng! Nhập lại đối tượng ưu tiên (0/1/2)");
            }
            DoiTuongUuTien = doiTuong;

            Console.Write("Nhập hội đồng thi: ");
            HoiDongThi = Console.ReadLine();
        }
        public void ThemNguyenVong(NguyenVong nv)
        {
            NguyenVongs.Add(nv);
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
        public void InNguyenVong()
        {
            Console.WriteLine("=== Danh sách nguyện vọng ===");
            if (NguyenVongs.Count == 0)
            {
                Console.WriteLine("Chưa có nguyện vọng nào.");
            }
            else
            {
                foreach (var nv in NguyenVongs)
                {
                    nv.InNguyenVong();
                }
            }
        }
    }
}
