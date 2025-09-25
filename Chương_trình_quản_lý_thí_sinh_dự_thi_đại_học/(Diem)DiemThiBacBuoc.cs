using System;
using System.Globalization;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public abstract class DiemThiBacBuoc : IDiemThi
    {
        public double Toan { get; set; }
        public double Van { get; set; }
        public double Anh { get; set; }

        public virtual void NhapDiem()
        {
            Toan = DocDiemMon("Toán");
            Van = DocDiemMon("Văn");
            Anh = DocDiemMon("Anh");
        }

        protected static double DocDiemMon(string mon)
        {
            Console.Write($"Nhập điểm {mon}: ");
            while (true)
            {
                var input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var diem))
                {
                    if (diem >= 0 && diem <= 10)
                    {
                        return Math.Round(diem, 2);
                    }
                }
                Console.Write($"Giá trị không hợp lệ (0-10, tối đa 2 chữ số thập phân). Nhập lại điểm {mon}: ");
            }
        }

        public virtual void InDiem()
        {
            Console.WriteLine($"Toán: {Toan} | Văn: {Van} | Anh: {Anh}");
        }

        protected void SaoChepDiemCoBanSang(DiemThiBacBuoc dich)
        {
            dich.Toan = Toan;
            dich.Van = Van;
            dich.Anh = Anh;
        }

        public virtual void GanDiemCoBan(double toan, double van, double anh)
        {
            if (!KiemTraDiem(toan) || !KiemTraDiem(van) || !KiemTraDiem(anh))
            {
                throw new ArgumentException("Điểm bắt buộc phải nằm trong khoảng 0-10.");
            }

            Toan = Math.Round(toan, 2);
            Van = Math.Round(van, 2);
            Anh = Math.Round(anh, 2);
        }

        protected static bool KiemTraDiem(double diem)
        {
            return diem >= 0 && diem <= 10;
        }
    }
}
