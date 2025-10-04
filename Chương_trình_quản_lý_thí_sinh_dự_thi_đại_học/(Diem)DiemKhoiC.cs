using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiC : DiemThiBase
    {
        public double Van { get; set; }
        public double Su { get; set; }
        public double Dia { get; set; }

        protected override MoTaMonHoc[] DanhSachMonHoc
        {
            get
            {
                return new MoTaMonHoc[]
                {
                    new MoTaMonHoc("Văn", () => Van, value => Van = value),
                    new MoTaMonHoc("Sử", () => Su, value => Su = value),
                    new MoTaMonHoc("Địa", () => Dia, value => Dia = value),
                };
            }
        }
    }
}
