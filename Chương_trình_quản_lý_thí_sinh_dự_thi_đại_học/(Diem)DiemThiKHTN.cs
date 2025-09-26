using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemThiKHTN : DiemThiBacBuoc,IDiemThi
    {
        public double Ly { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }
        public override void NhapDiem()
        {
            base.NhapDiem();
            Ly = NhapDiemMon("Lý");
            Hoa = NhapDiemMon("Hóa");
            Sinh = NhapDiemMon("Sinh");
        }
        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHTN =====");
            base.InDiem();
            Console.WriteLine($"Lý: {Ly} | Hóa: {Hoa} | Sinh: {Sinh}");
        }
    }
}
