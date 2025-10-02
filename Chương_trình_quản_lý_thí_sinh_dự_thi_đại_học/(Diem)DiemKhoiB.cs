using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class DiemKhoiB : DiemThiBase
    {
        public double Toan { get; set; }
        public double Hoa { get; set; }
        public double Sinh { get; set; }

        protected override MonHocDescriptor[] MonHoc
        {
            get
            {
                return new MonHocDescriptor[]
                {
                    new MonHocDescriptor("Toán", () => Toan, value => Toan = value),
                    new MonHocDescriptor("Hóa", () => Hoa, value => Hoa = value),
                    new MonHocDescriptor("Sinh", () => Sinh, value => Sinh = value),
                };
            }
        }
    }
}
