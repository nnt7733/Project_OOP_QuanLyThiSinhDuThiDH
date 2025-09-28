using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class QuanLyThiSinh
    {
        private List<ThongTinThiSinh> danhSachThiSinh;
        private const string FileStructureGuidance = "Định dạng tệp:\n" +
            "Khoi|SoBD|HoTen|NgaySinh|DanToc|TonGiao|GioiTinh|NoiSinh|DiaChi|SoCanCuoc|SoDienThoai|Email|KhuVuc|DoiTuongUuTien|HoiDongThi|Mon1|Mon2|Mon3\n" +
            "Khối A: Mon1=Toán, Mon2=Lý, Mon3=Hóa\n" +
            "Khối B: Mon1=Toán, Mon2=Hóa, Mon3=Sinh\n" +
            "Khối C: Mon1=Văn, Mon2=Sử, Mon3=Địa";
        public QuanLyThiSinh()
        {
            danhSachThiSinh = new List<ThongTinThiSinh>();
        }
        public bool ThemThiSinh(ThongTinThiSinh ts)
        {
            if (ts == null)
            {
                throw new ArgumentNullException(nameof(ts));
            }

            if (TimTheoSoBD(ts.SoBD) != null)
            {
                return false;
            }

            danhSachThiSinh.Add(ts);
            return true;
        }
        public void InDanhSach()
        {
            Console.WriteLine("===== DANH SÁCH THÍ SINH =====");
            foreach (var ts in danhSachThiSinh)
            {
                if (ts is IThiKhoi thiKhoi)
                {
                    thiKhoi.In();
                }
                else
                {
                    ts.InThongTin();
                }
                Console.WriteLine();
            }
        }
        public ThongTinThiSinh TimTheoSoBD(string soBD)
        {
            return danhSachThiSinh.FirstOrDefault(ts => ts.SoBD == soBD);
        }

        public bool CapNhatThiSinh(string soBD, ThongTinThiSinh thongTinCapNhat)
        {
            if (string.IsNullOrWhiteSpace(soBD) || thongTinCapNhat == null)
            {
                return false;
            }

            var thiSinhHienTai = TimTheoSoBD(soBD);
            if (thiSinhHienTai == null)
            {
                return false;
            }

            if (!string.Equals(thiSinhHienTai.SoBD, thongTinCapNhat.SoBD, StringComparison.Ordinal))
            {
                var thiSinhTrung = TimTheoSoBD(thongTinCapNhat.SoBD);
                if (thiSinhTrung != null && !ReferenceEquals(thiSinhTrung, thiSinhHienTai))
                {
                    return false;
                }
            }

            CapNhatThongTinCoBan(thiSinhHienTai, thongTinCapNhat);

            if (thiSinhHienTai is ThiSinhKhoiA thiSinhKhoiA && thongTinCapNhat is ThiSinhKhoiA capNhatKhoiA)
            {
                CapNhatDiemKhoiA(thiSinhKhoiA, capNhatKhoiA);
                return true;
            }

            if (thiSinhHienTai is ThiSinhKhoiB thiSinhKhoiB && thongTinCapNhat is ThiSinhKhoiB capNhatKhoiB)
            {
                CapNhatDiemKhoiB(thiSinhKhoiB, capNhatKhoiB);
                return true;
            }

            if (thiSinhHienTai is ThiSinhKhoiC thiSinhKhoiC && thongTinCapNhat is ThiSinhKhoiC capNhatKhoiC)
            {
                CapNhatDiemKhoiC(thiSinhKhoiC, capNhatKhoiC);
                return true;
            }

            return false;
        }

        public bool XoaThiSinh(string soBD)
        {
            if (string.IsNullOrWhiteSpace(soBD))
            {
                return false;
            }

            return danhSachThiSinh.RemoveAll(ts => ts.SoBD == soBD) > 0;
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

        public IEnumerable<ThongTinThiSinh> TimTheoHoTen(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                return Enumerable.Empty<ThongTinThiSinh>();
            }

            return danhSachThiSinh.Where(ts => ts.HoTen.IndexOf(hoTen, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void LuuVaoTxt(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Đường dẫn tệp không hợp lệ.");
                return;
            }

            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var headers = new[]
                {
                    "Khoi", "SoBD", "HoTen", "NgaySinh", "DanToc", "TonGiao", "GioiTinh", "NoiSinh", "DiaChi",
                    "SoCanCuoc", "SoDienThoai", "Email", "KhuVuc", "DoiTuongUuTien", "HoiDongThi",
                    "Mon1", "Mon2", "Mon3"
                };

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine(string.Join("|", headers));

                    foreach (var ts in danhSachThiSinh)
                    {
                        var fields = new string[headers.Length];

                        fields[0] = ts switch
                        {
                            ThiSinhKhoiA _ => "A",
                            ThiSinhKhoiB _ => "B",
                            ThiSinhKhoiC _ => "C",
                            _ => string.Empty
                        };

                        fields[1] = NormalizeText(ts.SoBD);
                        fields[2] = NormalizeText(ts.HoTen);
                        fields[3] = ts.NgaySinh.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        fields[4] = NormalizeText(ts.DanToc);
                        fields[5] = NormalizeText(ts.TonGiao);
                        fields[6] = NormalizeText(ts.GioiTinh);
                        fields[7] = NormalizeText(ts.NoiSinh);
                        fields[8] = NormalizeText(ts.DiaChi);
                        fields[9] = NormalizeText(ts.SoCanCuoc);
                        fields[10] = NormalizeText(ts.SoDienThoai);
                        fields[11] = NormalizeText(ts.Email);
                        fields[12] = NormalizeText(ts.KhuVuc);
                        fields[13] = ts.DoiTuongUuTien.ToString(CultureInfo.InvariantCulture);
                        fields[14] = NormalizeText(ts.HoiDongThi);

                        double? mon1 = null;
                        double? mon2 = null;
                        double? mon3 = null;

                        switch (ts)
                        {
                            case ThiSinhKhoiA khoiA:
                                mon1 = khoiA.Diem.Toan;
                                mon2 = khoiA.Diem.Ly;
                                mon3 = khoiA.Diem.Hoa;
                                break;
                            case ThiSinhKhoiB khoiB:
                                mon1 = khoiB.Diem.Toan;
                                mon2 = khoiB.Diem.Hoa;
                                mon3 = khoiB.Diem.Sinh;
                                break;
                            case ThiSinhKhoiC khoiC:
                                mon1 = khoiC.Diem.Van;
                                mon2 = khoiC.Diem.Su;
                                mon3 = khoiC.Diem.Dia;
                                break;
                        }

                        fields[15] = FormatScore(mon1);
                        fields[16] = FormatScore(mon2);
                        fields[17] = FormatScore(mon3);

                        writer.WriteLine(string.Join("|", fields));
                    }
                }

                Console.WriteLine($"Đã lưu danh sách thí sinh vào: {filePath}");
                Console.WriteLine(FileStructureGuidance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu file văn bản: {ex.Message}");
            }
        }

        public void TaiTuTxt(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Đường dẫn tệp không hợp lệ.");
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Không tìm thấy tệp: {filePath}");
                return;
            }

            try
            {
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    Console.WriteLine(FileStructureGuidance);

                    var header = reader.ReadLine();
                    if (header == null)
                    {
                        Console.WriteLine("Tệp không chứa dữ liệu.");
                        return;
                    }

                    danhSachThiSinh.Clear();
                    var lineNumber = 1;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        try
                        {
                            var parts = line.Split('|');
                            if (parts.Length < 18)
                            {
                                throw new FormatException("Không đủ cột dữ liệu");
                            }

                            for (var i = 0; i < parts.Length; i++)
                            {
                                parts[i] = parts[i].Trim();
                            }

                            var khoi = parts[0];
                            if (string.IsNullOrEmpty(khoi))
                            {
                                throw new FormatException("Thiếu thông tin khối");
                            }

                            var soBD = parts[1];
                            var hoTen = parts[2];
                            var ngaySinhStr = parts[3];
                            var danToc = parts[4];
                            var tonGiao = parts[5];
                            var gioiTinh = parts[6];
                            var noiSinh = parts[7];
                            var diaChi = parts[8];
                            var soCanCuoc = parts[9];
                            var soDienThoai = parts[10];
                            var email = parts[11];
                            var khuVuc = parts[12];
                            var doiTuongStr = parts[13];
                            var hoiDongThi = parts[14];

                            if (string.IsNullOrEmpty(ngaySinhStr))
                            {
                                throw new FormatException("Thiếu ngày sinh");
                            }

                            if (string.IsNullOrEmpty(doiTuongStr))
                            {
                                throw new FormatException("Thiếu đối tượng ưu tiên");
                            }

                            var ngaySinh = DateTime.ParseExact(ngaySinhStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            var doiTuongUuTien = int.Parse(doiTuongStr, CultureInfo.InvariantCulture);

                            ThongTinThiSinh thiSinh = null;

                            var khoiTrim = khoi.Trim().ToUpperInvariant();
                            switch (khoiTrim)
                            {
                                case "A":
                                    var thiSinhA = new ThiSinhKhoiA();
                                    thiSinhA.Diem.Toan = ParseDiem(parts[15], "điểm Toán", khoiTrim);
                                    thiSinhA.Diem.Ly = ParseDiem(parts[16], "điểm Lý", khoiTrim);
                                    thiSinhA.Diem.Hoa = ParseDiem(parts[17], "điểm Hóa", khoiTrim);
                                    thiSinh = thiSinhA;
                                    break;
                                case "B":
                                    var thiSinhB = new ThiSinhKhoiB();
                                    thiSinhB.Diem.Toan = ParseDiem(parts[15], "điểm Toán", khoiTrim);
                                    thiSinhB.Diem.Hoa = ParseDiem(parts[16], "điểm Hóa", khoiTrim);
                                    thiSinhB.Diem.Sinh = ParseDiem(parts[17], "điểm Sinh", khoiTrim);
                                    thiSinh = thiSinhB;
                                    break;
                                case "C":
                                    var thiSinhC = new ThiSinhKhoiC();
                                    thiSinhC.Diem.Van = ParseDiem(parts[15], "điểm Văn", khoiTrim);
                                    thiSinhC.Diem.Su = ParseDiem(parts[16], "điểm Sử", khoiTrim);
                                    thiSinhC.Diem.Dia = ParseDiem(parts[17], "điểm Địa", khoiTrim);
                                    thiSinh = thiSinhC;
                                    break;
                                default:
                                    throw new FormatException($"Khối không hợp lệ: {khoi}");
                            }

                            thiSinh.SoBD = soBD;
                            thiSinh.HoTen = hoTen;
                            thiSinh.NgaySinh = ngaySinh;
                            thiSinh.DanToc = danToc;
                            thiSinh.TonGiao = tonGiao;
                            thiSinh.GioiTinh = gioiTinh;
                            thiSinh.NoiSinh = noiSinh;
                            thiSinh.DiaChi = diaChi;
                            thiSinh.SoCanCuoc = soCanCuoc;
                            thiSinh.SoDienThoai = soDienThoai;
                            thiSinh.Email = email;
                            thiSinh.KhuVuc = khuVuc;
                            thiSinh.DoiTuongUuTien = doiTuongUuTien;
                            thiSinh.HoiDongThi = hoiDongThi;

                            danhSachThiSinh.Add(thiSinh);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Bỏ qua dòng {lineNumber} do lỗi: {ex.Message}");
                        }
                    }

                    Console.WriteLine($"Đã tải {danhSachThiSinh.Count} thí sinh từ: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải file văn bản: {ex.Message}");
            }
        }

        private static string FormatScore(double? score)
        {
            return score.HasValue ? score.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        private static string NormalizeText(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Replace("|", "/");
        }

        private static void CapNhatThongTinCoBan(ThongTinThiSinh dich, ThongTinThiSinh nguon)
        {
            dich.SoBD = nguon.SoBD;
            dich.HoTen = nguon.HoTen;
            dich.NgaySinh = nguon.NgaySinh;
            dich.DanToc = nguon.DanToc;
            dich.TonGiao = nguon.TonGiao;
            dich.GioiTinh = nguon.GioiTinh;
            dich.NoiSinh = nguon.NoiSinh;
            dich.DiaChi = nguon.DiaChi;
            dich.SoCanCuoc = nguon.SoCanCuoc;
            dich.SoDienThoai = nguon.SoDienThoai;
            dich.Email = nguon.Email;
            dich.KhuVuc = nguon.KhuVuc;
            dich.DoiTuongUuTien = nguon.DoiTuongUuTien;
            dich.HoiDongThi = nguon.HoiDongThi;
        }

        private static void CapNhatDiemKhoiA(ThiSinhKhoiA dich, ThiSinhKhoiA nguon)
        {
            dich.Diem.Toan = nguon.Diem.Toan;
            dich.Diem.Ly = nguon.Diem.Ly;
            dich.Diem.Hoa = nguon.Diem.Hoa;
        }

        private static void CapNhatDiemKhoiB(ThiSinhKhoiB dich, ThiSinhKhoiB nguon)
        {
            dich.Diem.Toan = nguon.Diem.Toan;
            dich.Diem.Hoa = nguon.Diem.Hoa;
            dich.Diem.Sinh = nguon.Diem.Sinh;
        }

        private static void CapNhatDiemKhoiC(ThiSinhKhoiC dich, ThiSinhKhoiC nguon)
        {
            dich.Diem.Van = nguon.Diem.Van;
            dich.Diem.Su = nguon.Diem.Su;
            dich.Diem.Dia = nguon.Diem.Dia;
        }

        private static double ParseDiem(string giaTri, string tenMon, string khoi)
        {
            if (string.IsNullOrWhiteSpace(giaTri))
            {
                throw new FormatException($"Thiếu {tenMon}. {FileStructureGuidance}");
            }

            if (double.TryParse(giaTri, NumberStyles.Float, CultureInfo.InvariantCulture, out var diem))
            {
                return diem;
            }

            if (double.TryParse(giaTri, NumberStyles.Float, CultureInfo.GetCultureInfo("vi-VN"), out diem))
            {
                return diem;
            }

            throw new FormatException($"Không thể phân tích {tenMon} cho khối {khoi}. {FileStructureGuidance}");
        }
    }
}
