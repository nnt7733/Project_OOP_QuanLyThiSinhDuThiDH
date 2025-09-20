using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class DiemThiBacBuoc:IDiemThi
    {
        public double Toan { get; set; }
        public double Van { get; set; }
        public double Anh { get; set; }
        public virtual void NhapDiem()
        {
            Console.Write("Nhập điểm Toán: ");
            Toan = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm Văn: ");
            Van = double.Parse(Console.ReadLine());
            Console.Write("Nhập điểm Anh: ");
            Anh = double.Parse(Console.ReadLine());
        }
        public virtual void InDiem()
        {
            Console.WriteLine($"Toán: +{Toan} +| Văn: {Van} | Anh: {Anh}");
        }
    }
}
