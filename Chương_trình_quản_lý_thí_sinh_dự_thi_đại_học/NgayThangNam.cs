using System;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public sealed class NgayThangNam
    {
        public int Ngay { get; }
        public int Thang { get; }
        public int Nam { get; }

        public NgayThangNam(int ngay, int thang, int nam)
        {
            if (!LaHopLe(ngay, thang, nam))
            {
                throw new ArgumentOutOfRangeException(nameof(ngay), "Ngày sinh không hợp lệ.");
            }

            Ngay = ngay;
            Thang = thang;
            Nam = nam;
        }

        public override string ToString()
        {
            return $"{Ngay:D2}/{Thang:D2}/{Nam:D4}";
        }

        private static bool LaHopLe(int ngay, int thang, int nam)
        {
            if (nam < DateTime.MinValue.Year || nam > DateTime.MaxValue.Year)
            {
                return false;
            }

            if (thang < 1 || thang > 12)
            {
                return false;
            }

            if (ngay < 1 || ngay > DateTime.DaysInMonth(nam, thang))
            {
                return false;
            }

            return true;
        }
    }
}
