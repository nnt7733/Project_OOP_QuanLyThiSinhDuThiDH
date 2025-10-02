using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiB : DiemThiBase
    {
        public double Toan { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        protected override (string TenMon, Func<double> Getter, Action<double> Setter)[] MonHoc => new[]
        {
            ("Toán", () => Toan, value => Toan = value),
            ("Hóa", () => Hoa, value => Hoa = value),
            ("Sinh", () => Sinh, value => Sinh = value),
        };
    }
}
