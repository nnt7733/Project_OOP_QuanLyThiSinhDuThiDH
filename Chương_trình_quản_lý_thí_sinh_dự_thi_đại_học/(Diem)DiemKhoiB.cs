using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiB : IDiemThi
    {
        public double Toan { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        public void NhapDiem()
        {
            Toan = NhapDiemMon("Toán");
            Hoa = NhapDiemMon("Hóa");
            Sinh = NhapDiemMon("Sinh");
        }

        public void InDiem()
        {
            Console.WriteLine($"Toán: {Toan} | Hóa: {Hoa} | Sinh: {Sinh}");
        }

        private static double NhapDiemMon(string tenMon)
        {
            Console.Write($"Nhập điểm {tenMon}: ");
            return DiemThiInputHelper.ReadScoreFromConsole(tenMon);
        }
    }
}
