using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiC : IDiemThi
    {
        public double Van { get; set; }
        public double Su { get; set; }
        public double Dia { get; set; }

        public void NhapDiem()
        {
            Van = NhapDiemMon("Văn");
            Su = NhapDiemMon("Sử");
            Dia = NhapDiemMon("Địa");
        }

        public void InDiem()
        {
            Console.WriteLine($"Văn: {Van} | Sử: {Su} | Địa: {Dia}");
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
