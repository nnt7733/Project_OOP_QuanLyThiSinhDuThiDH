using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class NguyenVong
    {
        public int ThuTu { get; set; } 
        public string Truong { get; set; } 
        public string Nganh { get; set; }   
        public string ToHop { get; set; }   
        public double DiemChuan { get; set; }
        public NguyenVong(int thuTu, string truong, string nganh, string toHop, double diemChuan)
        {
            ThuTu = thuTu;
            Truong = truong;
            Nganh = nganh;
            ToHop = toHop;
            DiemChuan = diemChuan;
        }
        public void InNguyenVong()
        {
            Console.WriteLine($"NV{ThuTu}: {Truong} - {Nganh} ({ToHop}), Điểm chuẩn: {DiemChuan}");
        }
    }
}
