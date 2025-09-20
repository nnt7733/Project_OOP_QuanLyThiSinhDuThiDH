using System;
using System.Collections.Generic;
using System.Linq;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class QuanLyThiSinh
    {
        private List<ThongTinThiSinh> danhSachThiSinh;
        public QuanLyThiSinh()
        {
            danhSachThiSinh = new List<ThongTinThiSinh>();
        }
        public void ThemThiSinh(ThongTinThiSinh ts)
        {
            danhSachThiSinh.Add(ts);
        }
        public void InDanhSach()
        {
            Console.WriteLine("===== DANH SÁCH THÍ SINH =====");
            foreach (var ts in danhSachThiSinh)
            {
                ts.InThongTin();
                ts.InNguyenVong();
                Console.WriteLine();
            }
        }
        public ThongTinThiSinh TimTheoSoBD(string soBD)
        {
            return danhSachThiSinh.FirstOrDefault(ts => ts.SoBD == soBD);
        }
        public void TimThuKhoa()
        {
            ThiSinhKhoiA thuKhoaA = null;
            ThiSinhKhoiB thuKhoaB = null;
            ThiSinhKhoiC thuKhoaC = null;
            double diemCaoNhatA = double.MinValue;
            double diemCaoNhatB = double.MinValue;
            double diemCaoNhatC = double.MinValue;
            foreach (var ts in danhSachThiSinh)
            {
                if (ts is ThiSinhKhoiA khoiA)
                {
                    double tongDiem = khoiA.TongDiem();
                    if (tongDiem > diemCaoNhatA)
                    {
                        diemCaoNhatA = tongDiem;
                        thuKhoaA = khoiA;
                    }
                }
                if (ts is ThiSinhKhoiB khoiB)
                {
                    double tongDiem = khoiB.TongDiem();
                    if (tongDiem > diemCaoNhatB)
                    {
                        diemCaoNhatB = tongDiem;
                        thuKhoaB = khoiB;
                    }
                }
                if (ts is ThiSinhKhoiC khoiC)
                {
                    double tongDiem = khoiC.TongDiem();
                    if (tongDiem > diemCaoNhatC)
                    {
                        diemCaoNhatC = tongDiem;
                        thuKhoaC = khoiC;
                    }
                }
            }
            if (thuKhoaA == null) Console.WriteLine("Không có thủ khoa khối A");
            else
            {
                thuKhoaA.InThongTin();
                Console.WriteLine($"Tổng điểm:{diemCaoNhatA}");
            }
            if (thuKhoaB == null) Console.WriteLine("Không có thủ khoa khối B");
            else
            {
                thuKhoaB.InThongTin();
                Console.WriteLine($"Tổng điểm:{diemCaoNhatB}");
            }
            if (thuKhoaC == null) Console.WriteLine("Không có thủ khoa khối C");
            else
            {
                thuKhoaC.InThongTin();
                Console.WriteLine($"Tổng điểm:{diemCaoNhatC}");
            }
        }
        public void ThongKeTheoKhoi()
        {
            int soKhoiA = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiA);
            int soKhoiB = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiB);
            int soKhoiC = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiC);

            Console.WriteLine($"Tổng số thí sinh khối A: {soKhoiA}");
            Console.WriteLine($"Tổng số thí sinh khối B: {soKhoiB}");
            Console.WriteLine($"Tổng số thí sinh khối C: {soKhoiC}");
        }
    }
}
