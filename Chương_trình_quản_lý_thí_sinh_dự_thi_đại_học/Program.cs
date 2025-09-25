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
            ql.ThemThiSinh(tsA);
            ThiSinhKhoiC tsC = new ThiSinhKhoiC();
            tsC.Nhap();
            ql.ThemThiSinh(tsC);
            ql.InDanhSach();
            ql.ThongKeTheoKhoi();
            ql.TimThuKhoa();

        }
    }
}
