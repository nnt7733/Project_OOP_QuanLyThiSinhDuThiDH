using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiC:ThongTinThiSinh
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
        public double TinhDiemVung()
        {
            double diem = TinhDiem();
            if (base.KhuVuc == "KV1")
            {
                return (30 - diem) / 7.5 * 0.75;
            }
            else if (base.KhuVuc == "KV2")
            {
                return (30 - diem) / 7.5 * 0.5;
            }
            else if (base.KhuVuc == "KV2-NT")
            {
                return (30 - diem) / 7.5 * 0.25;
            }
            else
            {
                return 0;
            }
        }
        public double TinhDiemUuTien()
        {
            if (base.DoiTuongUuTien == 1) { return 2; }
            if (base.DoiTuongUuTien == 2) { return 1; }
            else { return 0; }
        }
        public double TongDiem()
        {
            return TinhDiem() + TinhDiemVung() + TinhDiemUuTien();
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối C ===");
            base.InThongTin();
            Diem.InDiem();
            Console.WriteLine($"Tổng điểm khối A:{TongDiem()}");
        }
    }
}
