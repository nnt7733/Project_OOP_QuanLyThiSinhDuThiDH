using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiB : ThongTinThiSinh, IThiKhoi
    {
        public DiemThiKHTN Diem { get; set; }
        public ThiSinhKhoiB()
        {
            Diem = new DiemThiKHTN();
        }
        public void Nhap()
        {
            Console.WriteLine("=== Nhập thông tin thí sinh khối B ===");
            base.NhapThongTin();
            Diem.NhapDiem();
        }
        public double TinhDiem()
        {
            return Diem.Toan + Diem.Hoa + Diem.Sinh;
        }
        public double TinhDiemVung() => TinhDiemCongKhuVuc();
        public double TinhDiemUuTien() => TinhDiemCongUuTien();
        public double TongDiem()
        {
            return TinhDiem() + TinhDiemVung() + TinhDiemUuTien();
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối B ===");
            base.InThongTin();
            Diem.InDiem();
            Console.WriteLine($"Tổng điểm khối B: {TongDiem()}");
        }
    }
}
