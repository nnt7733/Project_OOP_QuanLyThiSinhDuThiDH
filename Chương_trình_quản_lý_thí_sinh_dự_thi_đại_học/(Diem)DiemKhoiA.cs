using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiA : DiemThiBase
    {
        public double Toan { get; set; }
        public double Ly { get; set; }
        public double Hoa { get; set; }

        protected override MonHocDescriptor[] MonHoc
        {
            get
            {
                return new MonHocDescriptor[]
                {
                    new MonHocDescriptor("Toán", () => Toan, value => Toan = value),
                    new MonHocDescriptor("Lý", () => Ly, value => Ly = value),
                    new MonHocDescriptor("Hóa", () => Hoa, value => Hoa = value),
                };
            }
        }
    }
}
