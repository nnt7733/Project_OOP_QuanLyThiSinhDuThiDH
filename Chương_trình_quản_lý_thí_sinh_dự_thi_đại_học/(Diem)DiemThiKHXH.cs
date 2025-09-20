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
            Console.Write("Nhập điểm Sử: ");
            Su = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm Địa: ");
            Dia = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm GDCD: ");
            GDCD = double.Parse(Console.ReadLine());
        }
        public override void InDiem()
        {
            Console.WriteLine("===== ĐIỂM THI MÔN KHXH =====");
            base.InDiem();
            Console.WriteLine($"Sử: {Su} | Địa: {Dia} | GDCD: {GDCD}");
        }
    }
}
