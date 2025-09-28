using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiC : ThongTinThiSinh, IThiKhoi
    {
        public DiemKhoiC Diem { get; set; }
        public ThiSinhKhoiC()
        {
            Diem = new DiemKhoiC();
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
        public double TinhDiemUuTien() => TinhTongDiemUuTien(TinhDiem());
        public double TongDiem()
        {
            var tongDiemBaMon = TinhDiem();
            return tongDiemBaMon + TinhTongDiemUuTien(tongDiemBaMon);
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối C ===");
            base.InThongTin();
            Diem.InDiem();
            var tongDiemBaMon = TinhDiem();
            var diemUuTienCoBan = Math.Round(TinhDiemCongKhuVuc() + TinhDiemCongUuTien(), 2, MidpointRounding.AwayFromZero);
            var diemUuTienApDung = TinhTongDiemUuTien(tongDiemBaMon);
            Console.WriteLine($"Tổng điểm 3 môn: {tongDiemBaMon}");
            Console.WriteLine($"Điểm ưu tiên cơ bản (KV + UT): {diemUuTienCoBan}");
            Console.WriteLine($"Điểm ưu tiên áp dụng: {diemUuTienApDung}");
            Console.WriteLine($"Tổng điểm khối C: {tongDiemBaMon + diemUuTienApDung}");
        }
    }
}
