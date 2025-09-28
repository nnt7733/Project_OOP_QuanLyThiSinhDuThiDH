using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class ThiSinhKhoiA : ThongTinThiSinh, IThiKhoi
    {
        public DiemKhoiA Diem { get; set; }
        public ThiSinhKhoiA()
        {
            Diem = new DiemKhoiA();
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
        public double TinhDiemUuTien() => TinhTongDiemUuTien(TinhDiem());
        public double TongDiem()
        {
            var tongDiemBaMon = TinhDiem();
            return tongDiemBaMon + TinhTongDiemUuTien(tongDiemBaMon);
        }
        public void In()
        {
            Console.WriteLine("=== Thí sinh khối A ===");
            base.InThongTin();
            Diem.InDiem();
            var tongDiemBaMon = TinhDiem();
            var diemUuTienCoBan = Math.Round(TinhDiemCongKhuVuc() + TinhDiemCongUuTien(), 2, MidpointRounding.AwayFromZero);
            var diemUuTienApDung = TinhTongDiemUuTien(tongDiemBaMon);
            Console.WriteLine($"Tổng điểm 3 môn: {tongDiemBaMon}");
            Console.WriteLine($"Điểm ưu tiên cơ bản (KV + UT): {diemUuTienCoBan}");
            Console.WriteLine($"Điểm ưu tiên áp dụng: {diemUuTienApDung}");
            Console.WriteLine($"Tổng điểm khối A: {tongDiemBaMon + diemUuTienApDung}");
        }
    }
}
