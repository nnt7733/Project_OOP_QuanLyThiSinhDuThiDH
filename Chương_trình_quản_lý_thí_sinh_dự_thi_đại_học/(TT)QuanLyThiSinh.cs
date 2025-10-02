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
            "Khối C: Mon1=Văn, Mon2=Sử, Mon3=Địa\n" +
            "Điểm mỗi môn: giá trị từ 0 đến 10, dùng dấu chấm hoặc dấu phẩy cho phần thập phân (ví dụ: 8.5 hoặc 8,5).";
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

        public bool CapNhatThiSinh(string soBD, Action<ThongTinThiSinh> capNhatAction)
        {
            if (string.IsNullOrWhiteSpace(soBD) || capNhatAction == null)
            {
                return false;
            }

            var thiSinhHienTai = TimTheoSoBD(soBD);
            if (thiSinhHienTai == null)
            {
                return false;
            }

            var soBDTruocKhiCapNhat = thiSinhHienTai.SoBD;

            capNhatAction(thiSinhHienTai);

            if (string.IsNullOrWhiteSpace(thiSinhHienTai.SoBD))
            {
                thiSinhHienTai.SoBD = soBDTruocKhiCapNhat;
                return false;
            }

            if (!string.Equals(soBDTruocKhiCapNhat, thiSinhHienTai.SoBD, StringComparison.Ordinal))
            {
                var thiSinhTrung = TimTheoSoBD(thiSinhHienTai.SoBD);
                if (thiSinhTrung != null && !ReferenceEquals(thiSinhTrung, thiSinhHienTai))
                {
                    thiSinhHienTai.SoBD = soBDTruocKhiCapNhat;
                    return false;
                }
            }

            return true;
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

                // Ghi kèm BOM để Notepad và các trình soạn thảo tương tự hiển thị đúng chữ có dấu.
                using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
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
                        fields[3] = ts.NgaySinh.ToString();
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

                            var ngaySinh = DocNgaySinhTuChuoi(ngaySinhStr);
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

        private static NgayThangNam DocNgaySinhTuChuoi(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri))
            {
                throw new FormatException("Thiếu ngày sinh.");
            }

            var parts = giaTri
                .Split(new[] { '/', '-', '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                throw new FormatException("Ngày sinh phải gồm 3 phần: ngày/tháng/năm (ví dụ 01/02/2003).");
            }

            if (!int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var ngay) ||
                !int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var thang) ||
                !int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var nam))
            {
                throw new FormatException("Ngày sinh chỉ được phép chứa chữ số.");
            }

            try
            {
                return new NgayThangNam(ngay, thang, nam);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FormatException("Giá trị ngày sinh không hợp lệ.");
            }
        }

        private static double ParseDiem(string giaTri, string tenMon, string khoi)
        {
            if (string.IsNullOrWhiteSpace(giaTri))
            {
                throw new FormatException($"Thiếu {tenMon}. {FileStructureGuidance}");
            }

            if (double.TryParse(giaTri, NumberStyles.Float, CultureInfo.InvariantCulture, out var diem) ||
                double.TryParse(giaTri, NumberStyles.Float, CultureInfo.GetCultureInfo("vi-VN"), out diem))
            {
                if (diem < 0 || diem > 10)
                {
                    throw new FormatException($"{tenMon} cho khối {khoi} phải nằm trong khoảng 0 đến 10. {FileStructureGuidance}");
                }

                return diem;
            }

            throw new FormatException($"Không thể phân tích {tenMon} cho khối {khoi}. {FileStructureGuidance}");
        }
    }
}
