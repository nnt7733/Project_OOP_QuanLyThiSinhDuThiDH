using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiC : DiemThiBase
    {
        public double Van { get; set; }
        public double Su { get; set; }
        public double Dia { get; set; }

        protected override (string TenMon, Func<double> Getter, Action<double> Setter)[] MonHoc => new[]
        {
            ("Văn", () => Van, value => Van = value),
            ("Sử", () => Su, value => Su = value),
            ("Địa", () => Dia, value => Dia = value),
        };
    }
}
