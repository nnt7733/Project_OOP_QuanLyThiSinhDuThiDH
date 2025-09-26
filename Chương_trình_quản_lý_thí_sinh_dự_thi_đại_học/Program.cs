using System;
using System.Text;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            QuanLyThiSinh ql = new QuanLyThiSinh();
            ThiSinhKhoiA tsA = new ThiSinhKhoiA();
            tsA.Nhap();
            if (!ql.ThemThiSinh(tsA))
            {
                Console.WriteLine("Số báo danh đã tồn tại, không thể thêm thí sinh khối A.");
            }
            ThiSinhKhoiC tsC = new ThiSinhKhoiC();
            tsC.Nhap();
            if (!ql.ThemThiSinh(tsC))
            {
                Console.WriteLine("Số báo danh đã tồn tại, không thể thêm thí sinh khối C.");
            }
            ql.InDanhSach();
            ql.ThongKeTheoKhoi();
            ql.TimThuKhoa();

            Console.Write("Nhập tên cần tìm kiếm: ");
            string tuKhoa = Console.ReadLine();
            var ketQua = ql.TimTheoHoTen(tuKhoa);
            Console.WriteLine("===== KẾT QUẢ TÌM KIẾM =====");
            foreach (var ts in ketQua)
            {
                ts.InThongTin();
                Console.WriteLine();
            }

        }
    }
}
