using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiA : ThongTinThiSinh, IThiKhoi
    {
        public DiemThiKHTN Diem { get; set; }
        public ThiSinhKhoiA()
        {
            Diem = new DiemThiKHTN();
        }
        public void Nhap()
        {
            Console.WriteLine("=== Nhập thông tin thí sinh khối A ===");
            base.NhapThongTin();
            Diem.NhapDiem();
        }
        public double TinhDiem()
        {
            return Diem.Toan + Diem.Ly + Diem.Hoa;
        }
        public double TinhDiemVung() => TinhDiemCongKhuVuc();
        public double TinhDiemUuTien() => TinhDiemCongUuTien();
        public double TongDiem()
        {
            return TinhDiem() + TinhDiemVung() + TinhDiemUuTien();
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối A ===");
            base.InThongTin();
            Diem.InDiem();
            Console.WriteLine($"Tổng điểm khối A:{TongDiem()}");
        }
    }
}
