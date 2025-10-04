using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class DiemThiBase : IDiemThi
    {
        protected class MoTaMonHoc
        {
            public MoTaMonHoc(string tenMon, Func<double> layGiaTri, Action<double> ganGiaTri)
            {
                TenMon = tenMon;
                LayGiaTri = layGiaTri;
                GanGiaTri = ganGiaTri;
            }

            public string TenMon { get; }
            public Func<double> LayGiaTri { get; }
            public Action<double> GanGiaTri { get; }
        }

        protected abstract MoTaMonHoc[] DanhSachMonHoc { get; }

        public void NhapDiem()
        {
            foreach (var mon in DanhSachMonHoc)
            {
                var diem = NhapDiemMon(mon.TenMon);
                mon.GanGiaTri(diem);
            }
        }

        public void InDiem()
        {
            var monHoc = DanhSachMonHoc;
            var thongTin = string.Join(" | ", Array.ConvertAll(monHoc, mon => $"{mon.TenMon}: {mon.LayGiaTri()}"));
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
