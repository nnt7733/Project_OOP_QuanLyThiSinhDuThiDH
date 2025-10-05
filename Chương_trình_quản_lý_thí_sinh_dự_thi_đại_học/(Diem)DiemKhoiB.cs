using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiB : DiemThiBase
    {
        public double Toan { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        public override MoTaMonHoc[] DanhSachMonHoc
        {
            get
            {
                return new MoTaMonHoc[]
                {
                    new MoTaMonHoc("Toán", () => Toan, value => Toan = value),
                    new MoTaMonHoc("Hóa", () => Hoa, value => Hoa = value),
                    new MoTaMonHoc("Sinh", () => Sinh, value => Sinh = value),
                };
            }
        }
    }
}
