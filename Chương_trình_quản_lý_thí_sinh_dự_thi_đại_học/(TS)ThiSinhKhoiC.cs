using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiC : ThongTinThiSinh, IThiKhoi
    {
        public DiemThiKHXH Diem { get; set; }
        public ThiSinhKhoiC()
        {
            Diem = new DiemThiKHXH();
        }
        public void Nhap()
        {
            Console.WriteLine("=== Nhập thông tin thí sinh khối C ===");
            base.NhapThongTin();
            Diem.NhapDiem();
        }
        public double TinhDiem()
        {
            return Diem.Van + Diem.Su + Diem.Dia;
        }
        public double TinhDiemVung() => TinhDiemCongKhuVuc();
        public double TinhDiemUuTien() => TinhDiemCongUuTien();
        public double TongDiem()
        {
            return TinhDiem() + TinhDiemVung() + TinhDiemUuTien();
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối C ===");
            base.InThongTin();
            Diem.InDiem();
            Console.WriteLine($"Tổng điểm khối C: {TongDiem()}");
        }
    }
}
