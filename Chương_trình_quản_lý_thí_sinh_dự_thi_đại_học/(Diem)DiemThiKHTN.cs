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
            Console.Write("Nhập điểm Lý: ");
            Ly = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm Hóa: ");
            Hoa = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm Sinh: ");
            Sinh = double.Parse(Console.ReadLine());
        }
        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHTN =====");
            base.InDiem();
            Console.WriteLine($"Lý: {Ly} | Hóa: {Hoa} | Sinh: {Sinh}");
        }
    }
}
