using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiA : DiemThiBase
    {
        public double Toan { get; set; }
        public double Ly { get; set; }
        public double Hoa { get; set; }

        public override MoTaMonHoc[] DanhSachMonHoc
        {
            get
            {
                return new MoTaMonHoc[]
                {
                    new MoTaMonHoc("Toán", () => Toan, value => Toan = value),
                    new MoTaMonHoc("Lý", () => Ly, value => Ly = value),
                    new MoTaMonHoc("Hóa", () => Hoa, value => Hoa = value),
                };
            }
        }
    }
}
