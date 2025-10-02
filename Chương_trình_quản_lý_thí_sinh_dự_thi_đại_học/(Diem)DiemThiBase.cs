using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class DiemThiBase : IDiemThi
    {
        protected class MonHocDescriptor
        {
            public MonHocDescriptor(string tenMon, Func<double> getter, Action<double> setter)
            {
                TenMon = tenMon;
                Getter = getter;
                Setter = setter;
            }

            public string TenMon { get; }
            public Func<double> Getter { get; }
            public Action<double> Setter { get; }
        }

        protected abstract MonHocDescriptor[] MonHoc { get; }

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
