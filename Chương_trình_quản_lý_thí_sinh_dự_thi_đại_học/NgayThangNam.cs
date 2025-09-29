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
            if (!IsValid(ngay, thang, nam))
            {
                throw new ArgumentOutOfRangeException(nameof(ngay), "Ngày sinh không hợp lệ.");
            }

            Ngay = ngay;
            Thang = thang;
            Nam = nam;
        }

        public static NgayThangNam Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Ngày sinh không được để trống.");
            }

            var parts = value.Trim().Split('/');
            if (parts.Length != 3)
            {
                throw new FormatException("Ngày sinh phải theo định dạng dd/MM/yyyy.");
            }

            var ngayPhan = parts[0].Trim();
            var thangPhan = parts[1].Trim();
            var namPhan = parts[2].Trim();

            if (!int.TryParse(ngayPhan, out var ngay) || !int.TryParse(thangPhan, out var thang) || !int.TryParse(namPhan, out var nam))
            {
                throw new FormatException("Ngày sinh phải theo định dạng dd/MM/yyyy.");
            }

            return new NgayThangNam(ngay, thang, nam);
        }

        public override string ToString()
        {
            return $"{Ngay:D2}/{Thang:D2}/{Nam:D4}";
        }

        private static bool IsValid(int ngay, int thang, int nam)
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
