using System;
using System.Collections.Generic;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemThiKHTN : DiemThiBacBuoc, IDiemThi
    {
        public double Ly { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        public override void NhapDiem()
        {
            base.NhapDiem();
            Ly = DocDiemMon("Lý");
            Hoa = DocDiemMon("Hóa");
            Sinh = DocDiemMon("Sinh");
        }

        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHTN =====");
            base.InDiem();
            Console.WriteLine($"Lý: {Ly} | Hóa: {Hoa} | Sinh: {Sinh}");
        }

        public DiemThiKHTN SaoChep()
        {
            var clone = new DiemThiKHTN();
            SaoChepDiemCoBanSang(clone);
            clone.Ly = Ly;
            clone.Hoa = Hoa;
            clone.Sinh = Sinh;
            return clone;
        }

        public Dictionary<string, double> LayTatCaDiemMon()
        {
            return new Dictionary<string, double>
            {
                ["Toan"] = Toan,
                ["Van"] = Van,
                ["Anh"] = Anh,
                ["Ly"] = Ly,
                ["Hoa"] = Hoa,
                ["Sinh"] = Sinh
            };
        }

        public void GanDiemChuyenSau(double ly, double hoa, double sinh)
        {
            if (!KiemTraDiem(ly) || !KiemTraDiem(hoa) || !KiemTraDiem(sinh))
            {
                throw new ArgumentException("Điểm chuyên sâu phải nằm trong khoảng 0-10.");
            }

            Ly = Math.Round(ly, 2);
            Hoa = Math.Round(hoa, 2);
            Sinh = Math.Round(sinh, 2);
        }
    }
}
