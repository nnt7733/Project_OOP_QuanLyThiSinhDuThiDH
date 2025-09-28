using System;
using System.Collections.Generic;
using System.Globalization;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public sealed class NgayThangNam : IEquatable<NgayThangNam>
    {
        private static readonly string[] SupportedFormats = { "dd/MM/yyyy", "d/M/yyyy" };
        private static readonly CultureInfo[] SupportedCultures = CreateSupportedCultures();

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

            if (TryParse(value, out var ketQua))
            {
                return ketQua;
            }

            throw new FormatException("Ngày sinh phải theo định dạng dd/MM/yyyy và là một ngày hợp lệ.");
        }

        public static bool TryParse(string value, out NgayThangNam result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var trimmed = value.Trim();
            foreach (var culture in SupportedCultures)
            {
                if (DateTime.TryParseExact(trimmed, SupportedFormats, culture, DateTimeStyles.None, out var date))
                {
                    result = FromDateTime(date);
                    return true;
                }
            }

            return false;
        }

        private static CultureInfo[] CreateSupportedCultures()
        {
            var cultures = new List<CultureInfo> { CultureInfo.InvariantCulture };

            try
            {
                cultures.Add(CultureInfo.GetCultureInfo("vi-VN"));
            }
            catch (CultureNotFoundException)
            {
                // Bỏ qua nếu môi trường không hỗ trợ mã văn hóa vi-VN.
            }

            return cultures.ToArray();
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Nam, Thang, Ngay);
        }

        public override string ToString()
        {
            return $"{Ngay:D2}/{Thang:D2}/{Nam:D4}";
        }

        public static implicit operator DateTime(NgayThangNam value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToDateTime();
        }

        public static implicit operator NgayThangNam(DateTime value)
        {
            return FromDateTime(value);
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
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + Ngay;
                hash = (hash * 23) + Thang;
                hash = (hash * 23) + Nam;
                return hash;
            }
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
