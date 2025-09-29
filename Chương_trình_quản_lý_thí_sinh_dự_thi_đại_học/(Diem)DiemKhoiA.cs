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
            Console.Write($"Nhập điểm {tenMon}: ");
            return DiemThiInputHelper.ReadScoreFromConsole(tenMon);
        }
    }
}
