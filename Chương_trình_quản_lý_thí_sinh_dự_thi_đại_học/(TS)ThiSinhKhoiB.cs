using System;
using System.Collections.Generic;

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

        public double TinhDiemVung()
        {
            double diem = TinhDiem();
            return KhuVuc switch
            {
                "KV1" => (30 - diem) / 7.5 * 0.75,
                "KV2" => (30 - diem) / 7.5 * 0.5,
                "KV2-NT" => (30 - diem) / 7.5 * 0.25,
                _ => 0
            };
        }

        public double TinhDiemUuTien()
        {
            return DoiTuongUuTien switch
            {
                1 => 2,
                2 => 1,
                _ => 0
            };
        }

        public double TongDiem()
        {
            return TinhDiem() + TinhDiemVung() + TinhDiemUuTien();
        }

        public void In()
        {
            Console.WriteLine("=== Thí sinh khối B ===");
            base.InThongTin();
            Diem.InDiem();
            Console.WriteLine($"Tổng điểm khối B: {TongDiem():0.00}");
        }

        public Dictionary<string, double> LayTatCaDiemMon() => Diem.LayTatCaDiemMon();

        public void CapNhatDiemTu(DiemThiKHTN diemMoi)
        {
            Diem.GanDiemCoBan(diemMoi.Toan, diemMoi.Van, diemMoi.Anh);
            Diem.GanDiemChuyenSau(diemMoi.Ly, diemMoi.Hoa, diemMoi.Sinh);
        }

        public override ThongTinThiSinh SaoChep()
        {
            var clone = new ThiSinhKhoiB();
            SaoChepThongTinChungSang(clone);
            clone.Diem = Diem.SaoChep();
            return clone;
        }
    }
}
