using System;
using System.Globalization;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public sealed class NgayThangNam : IEquatable<NgayThangNam>
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

        public static NgayThangNam FromDateTime(DateTime value)
        {
            return new NgayThangNam(value.Day, value.Month, value.Year);
        }

        public static NgayThangNam Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Ngày sinh không được để trống.");
            }

            if (!DateTime.TryParseExact(value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                throw new FormatException("Ngày sinh phải theo định dạng dd/MM/yyyy.");
            }

            return FromDateTime(date);
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Nam, Thang, Ngay);
        }

        public override string ToString()
        {
            return $"{Ngay:D2}/{Thang:D2}/{Nam:D4}";
        }

        public bool Equals(NgayThangNam other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Ngay == other.Ngay && Thang == other.Thang && Nam == other.Nam;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NgayThangNam);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ngay, Thang, Nam);
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
