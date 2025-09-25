using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public class CandidateDataService
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private const string CsvHeader =
            "Khoi;SoBD;HoTen;NgaySinh;DanToc;GioiTinh;NoiSinh;DiaChi;SoCanCuoc;SoDienThoai;Email;KhuVuc;DoiTuongUuTien;HoiDongThi;Toan;Van;Anh;Ly;Hoa;Sinh;Su;Dia;GDCD";

        public void XuatCsv(IEnumerable<ThongTinThiSinh> danhSach, string duongDan)
        {
            if (danhSach == null) throw new ArgumentNullException(nameof(danhSach));
            if (string.IsNullOrWhiteSpace(duongDan)) throw new ArgumentNullException(nameof(duongDan));

            var builder = new StringBuilder();
            builder.AppendLine(CsvHeader);
            foreach (var thiSinh in danhSach)
            {
                var dto = CandidateDto.FromEntity(thiSinh);
                builder.AppendLine(dto.ToCsvLine());
            }

            File.WriteAllText(duongDan, builder.ToString(), Encoding.UTF8);
        }

        public List<ThongTinThiSinh> NhapCsv(string duongDan)
        {
            if (!File.Exists(duongDan))
            {
                throw new FileNotFoundException("Không tìm thấy file CSV", duongDan);
            }

            var dong = File.ReadAllLines(duongDan, Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            if (dong.Count <= 1)
            {
                return new List<ThongTinThiSinh>();
            }

            var ketQua = new List<ThongTinThiSinh>();
            for (var i = 1; i < dong.Count; i++)
            {
                var dto = CandidateDto.FromCsvLine(dong[i]);
                ketQua.Add(dto.ToEntity());
            }

            return ketQua;
        }

        public void XuatJson(IEnumerable<ThongTinThiSinh> danhSach, string duongDan)
        {
            if (danhSach == null) throw new ArgumentNullException(nameof(danhSach));
            if (string.IsNullOrWhiteSpace(duongDan)) throw new ArgumentNullException(nameof(duongDan));

            var dtoList = danhSach.Select(CandidateDto.FromEntity).ToList();
            var json = JsonSerializer.Serialize(dtoList, JsonOptions);
            File.WriteAllText(duongDan, json, Encoding.UTF8);
        }

        public List<ThongTinThiSinh> NhapJson(string duongDan)
        {
            if (!File.Exists(duongDan))
            {
                throw new FileNotFoundException("Không tìm thấy file JSON", duongDan);
            }

            var json = File.ReadAllText(duongDan, Encoding.UTF8);
            var dtoList = JsonSerializer.Deserialize<List<CandidateDto>>(json, JsonOptions) ??
                          new List<CandidateDto>();

            return dtoList.Select(dto => dto.ToEntity()).ToList();
        }

        private class CandidateDto
        {
            public string Khoi { get; set; }
            public string SoBD { get; set; }
            public string HoTen { get; set; }
            public DateTime NgaySinh { get; set; }
            public string DanToc { get; set; }
            public string GioiTinh { get; set; }
            public string NoiSinh { get; set; }
            public string DiaChi { get; set; }
            public string SoCanCuoc { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
            public string KhuVuc { get; set; }
            public int DoiTuongUuTien { get; set; }
            public string HoiDongThi { get; set; }
            public Dictionary<string, double> DiemMon { get; set; } = new Dictionary<string, double>();

            public static CandidateDto FromEntity(ThongTinThiSinh thiSinh)
            {
                var dto = new CandidateDto
                {
                    SoBD = thiSinh.SoBD,
                    HoTen = thiSinh.HoTen,
                    NgaySinh = thiSinh.NgaySinh,
                    DanToc = thiSinh.DanToc,
                    GioiTinh = thiSinh.GioiTinh,
                    NoiSinh = thiSinh.NoiSinh,
                    DiaChi = thiSinh.DiaChi,
                    SoCanCuoc = thiSinh.SoCanCuoc,
                    SoDienThoai = thiSinh.SoDienThoai,
                    Email = thiSinh.Email,
                    KhuVuc = thiSinh.KhuVuc,
                    DoiTuongUuTien = thiSinh.DoiTuongUuTien,
                    HoiDongThi = thiSinh.HoiDongThi
                };

                switch (thiSinh)
                {
                    case ThiSinhKhoiA khoiA:
                        dto.Khoi = "A";
                        dto.DiemMon = khoiA.LayTatCaDiemMon();
                        break;
                    case ThiSinhKhoiB khoiB:
                        dto.Khoi = "B";
                        dto.DiemMon = khoiB.LayTatCaDiemMon();
                        break;
                    case ThiSinhKhoiC khoiC:
                        dto.Khoi = "C";
                        dto.DiemMon = khoiC.LayTatCaDiemMon();
                        break;
                    default:
                        throw new InvalidOperationException("Không xác định được khối của thí sinh.");
                }

                return dto;
            }

            public ThongTinThiSinh ToEntity()
            {
                ThongTinThiSinh thiSinh = Khoi switch
                {
                    "A" => new ThiSinhKhoiA(),
                    "B" => new ThiSinhKhoiB(),
                    "C" => new ThiSinhKhoiC(),
                    _ => throw new InvalidOperationException($"Khối không hợp lệ: {Khoi}")
                };

                thiSinh.SoBD = SoBD;
                thiSinh.HoTen = HoTen;
                thiSinh.NgaySinh = NgaySinh;
                thiSinh.DanToc = DanToc;
                thiSinh.GioiTinh = GioiTinh;
                thiSinh.NoiSinh = NoiSinh;
                thiSinh.DiaChi = DiaChi;
                thiSinh.SoCanCuoc = SoCanCuoc;
                thiSinh.SoDienThoai = SoDienThoai;
                thiSinh.Email = Email;
                thiSinh.KhuVuc = KhuVuc;
                thiSinh.DoiTuongUuTien = DoiTuongUuTien;
                thiSinh.HoiDongThi = HoiDongThi;

                switch (thiSinh)
                {
                    case ThiSinhKhoiA khoiA:
                        khoiA.Diem.GanDiemCoBan(LayDiem("Toan"), LayDiem("Van"), LayDiem("Anh"));
                        khoiA.Diem.GanDiemChuyenSau(LayDiem("Ly"), LayDiem("Hoa"), LayDiem("Sinh"));
                        break;
                    case ThiSinhKhoiB khoiB:
                        khoiB.Diem.GanDiemCoBan(LayDiem("Toan"), LayDiem("Van"), LayDiem("Anh"));
                        khoiB.Diem.GanDiemChuyenSau(LayDiem("Ly"), LayDiem("Hoa"), LayDiem("Sinh"));
                        break;
                    case ThiSinhKhoiC khoiC:
                        khoiC.Diem.GanDiemCoBan(LayDiem("Toan"), LayDiem("Van"), LayDiem("Anh"));
                        khoiC.Diem.GanDiemChuyenSau(LayDiem("Su"), LayDiem("Dia"), LayDiem("GDCD"));
                        break;
                }

                return thiSinh;
            }

            public string ToCsvLine()
            {
                var values = new List<string>
                {
                    Khoi,
                    SoBD,
                    HoTen,
                    NgaySinh.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DanToc,
                    GioiTinh,
                    NoiSinh,
                    DiaChi,
                    SoCanCuoc,
                    SoDienThoai,
                    Email,
                    KhuVuc,
                    DoiTuongUuTien.ToString(),
                    HoiDongThi,
                    FormatDiem("Toan"),
                    FormatDiem("Van"),
                    FormatDiem("Anh"),
                    FormatDiem("Ly"),
                    FormatDiem("Hoa"),
                    FormatDiem("Sinh"),
                    FormatDiem("Su"),
                    FormatDiem("Dia"),
                    FormatDiem("GDCD")
                };

                return string.Join(";", values.Select(EscapeCsv));
            }

            public static CandidateDto FromCsvLine(string line)
            {
                var columns = ParseCsvLine(line);
                if (columns.Length < 23)
                {
                    throw new FormatException("Dòng dữ liệu CSV không hợp lệ.");
                }

                var dto = new CandidateDto
                {
                    Khoi = columns[0],
                    SoBD = columns[1],
                    HoTen = columns[2],
                    NgaySinh = DateTime.ParseExact(columns[3], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DanToc = columns[4],
                    GioiTinh = columns[5],
                    NoiSinh = columns[6],
                    DiaChi = columns[7],
                    SoCanCuoc = columns[8],
                    SoDienThoai = columns[9],
                    Email = columns[10],
                    KhuVuc = columns[11],
                    DoiTuongUuTien = int.Parse(columns[12], CultureInfo.InvariantCulture),
                    HoiDongThi = columns[13],
                    DiemMon = new Dictionary<string, double>()
                };

                dto.DiemMon["Toan"] = ParseDouble(columns[14]);
                dto.DiemMon["Van"] = ParseDouble(columns[15]);
                dto.DiemMon["Anh"] = ParseDouble(columns[16]);
                dto.DiemMon["Ly"] = ParseDouble(columns[17]);
                dto.DiemMon["Hoa"] = ParseDouble(columns[18]);
                dto.DiemMon["Sinh"] = ParseDouble(columns[19]);
                dto.DiemMon["Su"] = ParseDouble(columns[20]);
                dto.DiemMon["Dia"] = ParseDouble(columns[21]);
                dto.DiemMon["GDCD"] = ParseDouble(columns[22]);

                return dto;
            }

            private static double ParseDouble(string value)
            {
                return double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : 0;
            }

            private double LayDiem(string mon)
            {
                return DiemMon.TryGetValue(mon, out var value) ? value : 0;
            }

            private string FormatDiem(string mon)
            {
                return LayDiem(mon).ToString("0.##", CultureInfo.InvariantCulture);
            }

            private static string EscapeCsv(string value)
            {
                if (value == null)
                {
                    return string.Empty;
                }

                if (value.Contains('"'))
                {
                    value = value.Replace("\"", "\"\"");
                }

                if (value.Contains(';') || value.Contains('\n') || value.Contains('\r'))
                {
                    return $"\"{value}\"";
                }

                return value;
            }

            private static string[] ParseCsvLine(string line)
            {
                var values = new List<string>();
                var builder = new StringBuilder();
                var inQuotes = false;

                for (var i = 0; i < line.Length; i++)
                {
                    var ch = line[i];
                    if (ch == '"')
                    {
                        if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                        {
                            builder.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = !inQuotes;
                        }
                    }
                    else if (ch == ';' && !inQuotes)
                    {
                        values.Add(builder.ToString());
                        builder.Clear();
                    }
                    else
                    {
                        builder.Append(ch);
                    }
                }

                values.Add(builder.ToString());
                return values.ToArray();
            }
        }
    }
}
