using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiA : IDiemThi
    {
        public double Toan { get; set; }
        public double Ly { get; set; }
        public double Hoa { get; set; }

        public void NhapDiem()
        {
            Toan = NhapDiemMon("Toán");
            Ly = NhapDiemMon("Lý");
            Hoa = NhapDiemMon("Hóa");
        }

        public void InDiem()
        {
            Console.WriteLine($"Toán: {Toan} | Lý: {Ly} | Hóa: {Hoa}");
        }

        private static double NhapDiemMon(string tenMon)
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
