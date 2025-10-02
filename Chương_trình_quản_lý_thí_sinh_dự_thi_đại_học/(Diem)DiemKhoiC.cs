using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiC : DiemThiBase
    {
        public double Van { get; set; }
        public double Su { get; set; }
        public double Dia { get; set; }

        protected override MonHocDescriptor[] MonHoc
        {
            get
            {
                return new MonHocDescriptor[]
                {
                    new MonHocDescriptor("Văn", () => Van, value => Van = value),
                    new MonHocDescriptor("Sử", () => Su, value => Su = value),
                    new MonHocDescriptor("Địa", () => Dia, value => Dia = value),
                };
            }
        }
    }
}
