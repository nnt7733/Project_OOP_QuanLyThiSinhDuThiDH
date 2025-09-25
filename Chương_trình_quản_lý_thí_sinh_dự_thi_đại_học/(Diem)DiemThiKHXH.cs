using System;
using System.Collections.Generic;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemThiKHXH : DiemThiBacBuoc, IDiemThi
    {
        public double Su { get; set; }
        public double Dia { get; set; }
        public double GDCD { get; set; }

        public override void NhapDiem()
        {
            base.NhapDiem();
            Su = DocDiemMon("Sử");
            Dia = DocDiemMon("Địa");
            GDCD = DocDiemMon("GDCD");
        }

        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHXH =====");
            base.InDiem();
            Console.WriteLine($"Sử: {Su} | Địa: {Dia} | GDCD: {GDCD}");
        }

        public DiemThiKHXH SaoChep()
        {
            var clone = new DiemThiKHXH();
            SaoChepDiemCoBanSang(clone);
            clone.Su = Su;
            clone.Dia = Dia;
            clone.GDCD = GDCD;
            return clone;
        }

        public Dictionary<string, double> LayTatCaDiemMon()
        {
            return new Dictionary<string, double>
            {
                ["Toan"] = Toan,
                ["Van"] = Van,
                ["Anh"] = Anh,
                ["Su"] = Su,
                ["Dia"] = Dia,
                ["GDCD"] = GDCD
            };
        }

        public void GanDiemChuyenSau(double su, double dia, double gdcd)
        {
            if (!KiemTraDiem(su) || !KiemTraDiem(dia) || !KiemTraDiem(gdcd))
            {
                throw new ArgumentException("Điểm chuyên sâu phải nằm trong khoảng 0-10.");
            }

            Su = Math.Round(su, 2);
            Dia = Math.Round(dia, 2);
            GDCD = Math.Round(gdcd, 2);
        }
    }
}
