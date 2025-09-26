using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemThiKHXH:DiemThiBacBuoc,IDiemThi
    {
        public double Su { get; set; }
        public double Dia { get; set; }
        public double GDCD { get; set; }
        public override void NhapDiem()
        {
            base.NhapDiem();
            Su = NhapDiemMon("Sử");
            Dia = NhapDiemMon("Địa");
            GDCD = NhapDiemMon("GDCD");
        }
        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHXH =====");
            base.InDiem();
            Console.WriteLine($"Sử: {Su} | Địa: {Dia} | GDCD: {GDCD}");
        }
    }
}
