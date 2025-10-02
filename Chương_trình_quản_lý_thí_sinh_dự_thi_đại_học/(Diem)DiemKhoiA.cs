using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiA : DiemThiBase
    {
        public double Toan { get; set; }
        public double Ly { get; set; }
        public double Hoa { get; set; }

        protected override (string TenMon, Func<double> Getter, Action<double> Setter)[] MonHoc => new[]
        {
            ("Toán", () => Toan, value => Toan = value),
            ("Lý", () => Ly, value => Ly = value),
            ("Hóa", () => Hoa, value => Hoa = value),
        };
    }
}
