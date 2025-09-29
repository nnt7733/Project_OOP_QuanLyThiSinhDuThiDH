using System;
using System.Globalization;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    internal static class DiemThiInputHelper
    {
        private static readonly CultureInfo[] SupportedCultures =
        {
            CultureInfo.InvariantCulture,
            CultureInfo.GetCultureInfo("vi-VN")
        };

        public static double ReadScoreFromConsole(string subjectName)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (TryParseScore(input, out var score) && score >= 0 && score <= 10)
                {
                    return Math.Round(score, 2, MidpointRounding.AwayFromZero);
                }

                Console.Write($"Điểm không hợp lệ! Nhập lại điểm {subjectName} (0-10): ");
            }
        }

        private static bool TryParseScore(string value, out double score)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                foreach (var culture in SupportedCultures)
                {
                    if (double.TryParse(value, NumberStyles.Float, culture, out score))
                    {
                        return true;
                    }
                }
            }

            score = default;
            return false;
        }
    }
}
