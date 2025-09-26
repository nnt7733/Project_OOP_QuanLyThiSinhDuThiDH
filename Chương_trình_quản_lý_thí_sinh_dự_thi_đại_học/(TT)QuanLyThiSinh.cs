using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using ClosedXML.Excel;
namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class QuanLyThiSinh
    {
        private List<ThongTinThiSinh> danhSachThiSinh;
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

        public void LuuVaoExcel(string filePath)
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

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("DanhSach");
                    var headers = new[]
                    {
                        "Khoi", "SoBD", "HoTen", "NgaySinh", "DanToc", "GioiTinh", "NoiSinh", "DiaChi",
                        "SoCanCuoc", "SoDienThoai", "Email", "KhuVuc", "DoiTuongUuTien", "HoiDongThi",
                        "Toan", "Van", "Anh", "Ly", "Hoa", "Sinh", "Su", "Dia", "GDCD"
                    };

                    for (var i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = headers[i];
                    }

                    for (var index = 0; index < danhSachThiSinh.Count; index++)
                    {
                        var ts = danhSachThiSinh[index];
                        var row = index + 2;

                        worksheet.Cell(row, 1).Value = ts switch
                        {
                            ThiSinhKhoiA _ => "A",
                            ThiSinhKhoiB _ => "B",
                            ThiSinhKhoiC _ => "C",
                            _ => string.Empty
                        };

                        worksheet.Cell(row, 2).Value = ts.SoBD;
                        worksheet.Cell(row, 3).Value = ts.HoTen;
                        worksheet.Cell(row, 4).Value = ts.NgaySinh.ToString("dd/MM/yyyy");
                        worksheet.Cell(row, 5).Value = ts.DanToc;
                        worksheet.Cell(row, 6).Value = ts.GioiTinh;
                        worksheet.Cell(row, 7).Value = ts.NoiSinh;
                        worksheet.Cell(row, 8).Value = ts.DiaChi;
                        worksheet.Cell(row, 9).Value = ts.SoCanCuoc;
                        worksheet.Cell(row, 10).Value = ts.SoDienThoai;
                        worksheet.Cell(row, 11).Value = ts.Email;
                        worksheet.Cell(row, 12).Value = ts.KhuVuc;
                        worksheet.Cell(row, 13).Value = ts.DoiTuongUuTien;
                        worksheet.Cell(row, 14).Value = ts.HoiDongThi;

                        double? toan = null;
                        double? van = null;
                        double? anh = null;
                        double? ly = null;
                        double? hoa = null;
                        double? sinh = null;
                        double? su = null;
                        double? dia = null;
                        double? gdcd = null;

                        switch (ts)
                        {
                            case ThiSinhKhoiA khoiA:
                                toan = khoiA.Diem.Toan;
                                van = khoiA.Diem.Van;
                                anh = khoiA.Diem.Anh;
                                ly = khoiA.Diem.Ly;
                                hoa = khoiA.Diem.Hoa;
                                sinh = khoiA.Diem.Sinh;
                                break;
                            case ThiSinhKhoiB khoiB:
                                toan = khoiB.Diem.Toan;
                                van = khoiB.Diem.Van;
                                anh = khoiB.Diem.Anh;
                                hoa = khoiB.Diem.Hoa;
                                sinh = khoiB.Diem.Sinh;
                                break;
                            case ThiSinhKhoiC khoiC:
                                toan = khoiC.Diem.Toan;
                                van = khoiC.Diem.Van;
                                anh = khoiC.Diem.Anh;
                                su = khoiC.Diem.Su;
                                dia = khoiC.Diem.Dia;
                                gdcd = khoiC.Diem.GDCD;
                                break;
                        }

                        worksheet.Cell(row, 15).Value = toan ?? string.Empty;
                        worksheet.Cell(row, 16).Value = van ?? string.Empty;
                        worksheet.Cell(row, 17).Value = anh ?? string.Empty;
                        worksheet.Cell(row, 18).Value = ly ?? string.Empty;
                        worksheet.Cell(row, 19).Value = hoa ?? string.Empty;
                        worksheet.Cell(row, 20).Value = sinh ?? string.Empty;
                        worksheet.Cell(row, 21).Value = su ?? string.Empty;
                        worksheet.Cell(row, 22).Value = dia ?? string.Empty;
                        worksheet.Cell(row, 23).Value = gdcd ?? string.Empty;
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }

                Console.WriteLine($"Đã lưu danh sách thí sinh vào: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu file Excel: {ex.Message}");
            }
        }

        public void TaiTuExcel(string filePath)
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
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheets
                        .FirstOrDefault(ws => string.Equals(ws.Name, "DanhSach", StringComparison.OrdinalIgnoreCase))
                        ?? workbook.Worksheets.FirstOrDefault();

                    if (worksheet == null)
                    {
                        Console.WriteLine("Không tìm thấy bảng tính hợp lệ trong tệp Excel.");
                        return;
                    }

                    var lastRow = worksheet.LastRowUsed()?.RowNumber();
                    if (lastRow == null || lastRow.Value < 2)
                    {
                        danhSachThiSinh.Clear();
                        Console.WriteLine("Không có dữ liệu thí sinh để tải.");
                        return;
                    }

                    danhSachThiSinh.Clear();

                    for (var rowNumber = 2; rowNumber <= lastRow.Value; rowNumber++)
                    {
                        try
                        {
                            var khoi = worksheet.Cell(rowNumber, 1).GetValue<string>().Trim();
                            if (string.IsNullOrEmpty(khoi))
                            {
                                throw new FormatException("Thiếu thông tin khối");
                            }

                            var soBD = worksheet.Cell(rowNumber, 2).GetValue<string>().Trim();
                            var hoTen = worksheet.Cell(rowNumber, 3).GetValue<string>().Trim();
                            var ngaySinhStr = worksheet.Cell(rowNumber, 4).GetValue<string>().Trim();
                            var danToc = worksheet.Cell(rowNumber, 5).GetValue<string>().Trim();
                            var gioiTinh = worksheet.Cell(rowNumber, 6).GetValue<string>().Trim();
                            var noiSinh = worksheet.Cell(rowNumber, 7).GetValue<string>().Trim();
                            var diaChi = worksheet.Cell(rowNumber, 8).GetValue<string>().Trim();
                            var soCanCuoc = worksheet.Cell(rowNumber, 9).GetValue<string>().Trim();
                            var soDienThoai = worksheet.Cell(rowNumber, 10).GetValue<string>().Trim();
                            var email = worksheet.Cell(rowNumber, 11).GetValue<string>().Trim();
                            var khuVuc = worksheet.Cell(rowNumber, 12).GetValue<string>().Trim();
                            var doiTuongStr = worksheet.Cell(rowNumber, 13).GetValue<string>().Trim();
                            var hoiDongThi = worksheet.Cell(rowNumber, 14).GetValue<string>().Trim();

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

                            var toan = ParseDiem(worksheet.Cell(rowNumber, 15), "điểm Toán");
                            var van = ParseDiem(worksheet.Cell(rowNumber, 16), "điểm Văn");
                            var anh = ParseDiem(worksheet.Cell(rowNumber, 17), "điểm Anh");

                            ThongTinThiSinh thiSinh = null;

                            switch (khoi.Trim().ToUpperInvariant())
                            {
                                case "A":
                                    var thiSinhA = new ThiSinhKhoiA();
                                    thiSinhA.Diem.Toan = toan;
                                    thiSinhA.Diem.Van = van;
                                    thiSinhA.Diem.Anh = anh;
                                    thiSinhA.Diem.Ly = ParseDiem(worksheet.Cell(rowNumber, 18), "điểm Lý");
                                    thiSinhA.Diem.Hoa = ParseDiem(worksheet.Cell(rowNumber, 19), "điểm Hóa");
                                    thiSinhA.Diem.Sinh = ParseDiem(worksheet.Cell(rowNumber, 20), "điểm Sinh");
                                    thiSinh = thiSinhA;
                                    break;
                                case "B":
                                    var thiSinhB = new ThiSinhKhoiB();
                                    thiSinhB.Diem.Toan = toan;
                                    thiSinhB.Diem.Van = van;
                                    thiSinhB.Diem.Anh = anh;
                                    thiSinhB.Diem.Hoa = ParseDiem(worksheet.Cell(rowNumber, 19), "điểm Hóa");
                                    thiSinhB.Diem.Sinh = ParseDiem(worksheet.Cell(rowNumber, 20), "điểm Sinh");
                                    thiSinh = thiSinhB;
                                    break;
                                case "C":
                                    var thiSinhC = new ThiSinhKhoiC();
                                    thiSinhC.Diem.Toan = toan;
                                    thiSinhC.Diem.Van = van;
                                    thiSinhC.Diem.Anh = anh;
                                    thiSinhC.Diem.Su = ParseDiem(worksheet.Cell(rowNumber, 21), "điểm Sử");
                                    thiSinhC.Diem.Dia = ParseDiem(worksheet.Cell(rowNumber, 22), "điểm Địa");
                                    thiSinhC.Diem.GDCD = ParseDiem(worksheet.Cell(rowNumber, 23), "điểm GDCD");
                                    thiSinh = thiSinhC;
                                    break;
                                default:
                                    throw new FormatException($"Khối không hợp lệ: {khoi}");
                            }

                            thiSinh.SoBD = soBD;
                            thiSinh.HoTen = hoTen;
                            thiSinh.NgaySinh = ngaySinh;
                            thiSinh.DanToc = danToc;
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
                            Console.WriteLine($"Bỏ qua hàng {rowNumber} do lỗi: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải file Excel: {ex.Message}");
            }
        }

        private static double ParseDiem(IXLCell cell, string tenMon)
        {
            var giaTri = cell.GetValue<string>().Trim();
            if (string.IsNullOrEmpty(giaTri))
            {
                throw new FormatException($"Thiếu {tenMon}");
            }

            return double.Parse(giaTri, CultureInfo.InvariantCulture);
        }
    }
}
