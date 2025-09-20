using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; 
            Console.InputEncoding=Encoding.UTF8;
            QuanLyThiSinh ql = new QuanLyThiSinh();
            ThiSinhKhoiA tsA = new ThiSinhKhoiA();
            tsA.Nhap();
            tsA.ThemNguyenVong(new NguyenVong(1, "ĐH Bách Khoa", "CNTT", "A00", 27.5));
            ql.ThemThiSinh(tsA);
            ThiSinhKhoiC tsC = new ThiSinhKhoiC();
            tsC.Nhap();
            tsC.ThemNguyenVong(new NguyenVong(1, "ĐH KHXHNV", "Ngữ Văn", "C00", 24.0));
            ql.ThemThiSinh(tsC);
            ql.InDanhSach();
            ql.ThongKeTheoKhoi();
            ql.TimThuKhoa();
        }
    }
}
