using System;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemThiBacBuoc : IDiemThi
    {
        public double Toan { get; set; }
        public double Van { get; set; }
        public double Anh { get; set; }
        public virtual void NhapDiem()
        {
            Toan = NhapDiemMon("Toán");
            Van = NhapDiemMon("Văn");
            Anh = NhapDiemMon("Anh");
        }
        public virtual void InDiem()
        {
            Console.WriteLine($"Toán: {Toan} | Văn: {Van} | Anh: {Anh}");
        }
        protected static double NhapDiemMon(string tenMon)
        {
            double diem;
            Console.Write($"Nhập điểm {tenMon}: ");
            while (!double.TryParse(Console.ReadLine(), out diem) || diem < 0 || diem > 10)
            {
                Console.Write($"Điểm không hợp lệ! Nhập lại điểm {tenMon} (0-10): ");
            }

            return Math.Round(diem, 2);
        }
    }
}
