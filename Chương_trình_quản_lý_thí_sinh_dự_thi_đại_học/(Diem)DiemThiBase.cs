using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class DiemThiBase : IDiemThi
    {
        protected abstract (string TenMon, Func<double> Getter, Action<double> Setter)[] MonHoc { get; }

        public void NhapDiem()
        {
            foreach (var mon in MonHoc)
            {
                var diem = NhapDiemMon(mon.TenMon);
                mon.Setter(diem);
            }
        }

        public void InDiem()
        {
            var monHoc = MonHoc;
            var thongTin = string.Join(" | ", Array.ConvertAll(monHoc, mon => $"{mon.TenMon}: {mon.Getter()}"));
            Console.WriteLine(thongTin);
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
