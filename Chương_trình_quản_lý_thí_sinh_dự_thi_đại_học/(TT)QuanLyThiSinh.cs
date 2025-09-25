using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chương_trình_quản_lý_thí_sinh_dự_thi_đại_học
{
    public enum TieuChiSapXep
    {
        TongDiem,
        HoTen,
        NgaySinh
    }

    internal enum LoaiThaoTac
    {
        Them,
        CapNhat,
        Xoa
    }

    internal class LichSuThaoTac
    {
        public LoaiThaoTac Loai { get; }
        public ThongTinThiSinh Truoc { get; }
        public ThongTinThiSinh Sau { get; }

        public LichSuThaoTac(LoaiThaoTac loai, ThongTinThiSinh truoc, ThongTinThiSinh sau)
        {
            Loai = loai;
            Truoc = truoc?.SaoChep();
            Sau = sau?.SaoChep();
        }
    }

    public class ThongKeMonHoc
    {
        public string Mon { get; set; }
        public double DiemCaoNhat { get; set; }
        public double DiemThapNhat { get; set; }
        public double DiemTrungBinh { get; set; }
        public int SoLuong { get; set; }
    }

    public class ThongKeNangCaoResult
    {
        public Dictionary<string, double> DiemTrungBinhTheoKhoi { get; } = new();
        public Dictionary<string, ThongKeMonHoc> ThongKeTheoMon { get; } = new();
        public int TongSoThiSinh { get; set; }
    }

    public class QuanLyThiSinh
    {
        private readonly List<ThongTinThiSinh> danhSachThiSinh;
        private readonly Stack<LichSuThaoTac> undoStack;
        private readonly Stack<LichSuThaoTac> redoStack;
        private readonly List<string> nhatKy;

        public QuanLyThiSinh()
        {
            danhSachThiSinh = new List<ThongTinThiSinh>();
            undoStack = new Stack<LichSuThaoTac>();
            redoStack = new Stack<LichSuThaoTac>();
            nhatKy = new List<string>();
        }

        public ReadOnlyCollection<ThongTinThiSinh> DanhSach => danhSachThiSinh.AsReadOnly();
        public IReadOnlyList<string> NhatKy => nhatKy.AsReadOnly();
        public bool CoTheUndo => undoStack.Count > 0;
        public bool CoTheRedo => redoStack.Count > 0;

        public bool KiemTraSoBDTonTai(string soBD)
        {
            return danhSachThiSinh.Any(ts => string.Equals(ts.SoBD, soBD, StringComparison.OrdinalIgnoreCase));
        }

        public void ThemThiSinh(ThongTinThiSinh ts)
        {
            if (ts == null) throw new ArgumentNullException(nameof(ts));
            if (string.IsNullOrWhiteSpace(ts.SoBD))
            {
                throw new ArgumentException("Thí sinh phải có số báo danh.");
            }

            if (KiemTraSoBDTonTai(ts.SoBD))
            {
                throw new InvalidOperationException($"Số báo danh {ts.SoBD} đã tồn tại.");
            }

            danhSachThiSinh.Add(ts);
            GhiLog($"Thêm thí sinh {ts.SoBD} - {ts.HoTen}");
            LuuThaoTac(LoaiThaoTac.Them, null, ts);
        }

        public void ThemHoacCapNhat(ThongTinThiSinh ts)
        {
            var tonTai = TimTheoSoBD(ts.SoBD);
            if (tonTai == null)
            {
                ThemThiSinh(ts);
            }
            else
            {
                CapNhatThiSinh(tonTai.SoBD, ts);
            }
        }

        public void InDanhSach()
        {
            Console.WriteLine("===== DANH SÁCH THÍ SINH =====");
            foreach (var ts in danhSachThiSinh)
            {
                switch (ts)
                {
                    case ThiSinhKhoiA khoiA:
                        khoiA.In();
                        break;
                    case ThiSinhKhoiB khoiB:
                        khoiB.In();
                        break;
                    case ThiSinhKhoiC khoiC:
                        khoiC.In();
                        break;
                    default:
                        ts.InThongTin();
                        break;
                }
                Console.WriteLine();
            }
        }

        public ThongTinThiSinh TimTheoSoBD(string soBD)
        {
            return danhSachThiSinh.FirstOrDefault(ts =>
                string.Equals(ts.SoBD, soBD, StringComparison.OrdinalIgnoreCase));
        }

        public bool CapNhatThiSinh(string soBDCu, ThongTinThiSinh thongTinMoi)
        {
            if (thongTinMoi == null) throw new ArgumentNullException(nameof(thongTinMoi));
            var index = danhSachThiSinh.FindIndex(ts =>
                string.Equals(ts.SoBD, soBDCu, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
            {
                return false;
            }

            if (!string.Equals(soBDCu, thongTinMoi.SoBD, StringComparison.OrdinalIgnoreCase) &&
                KiemTraSoBDTonTai(thongTinMoi.SoBD))
            {
                throw new InvalidOperationException($"Số báo danh {thongTinMoi.SoBD} đã tồn tại.");
            }

            var truoc = danhSachThiSinh[index].SaoChep();
            danhSachThiSinh[index] = thongTinMoi;
            GhiLog($"Cập nhật thí sinh {soBDCu} -> {thongTinMoi.SoBD}");
            LuuThaoTac(LoaiThaoTac.CapNhat, truoc, thongTinMoi);
            return true;
        }

        public bool XoaThiSinh(string soBD)
        {
            var index = danhSachThiSinh.FindIndex(ts =>
                string.Equals(ts.SoBD, soBD, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
            {
                return false;
            }

            var truoc = danhSachThiSinh[index].SaoChep();
            danhSachThiSinh.RemoveAt(index);
            GhiLog($"Xóa thí sinh {truoc.SoBD} - {truoc.HoTen}");
            LuuThaoTac(LoaiThaoTac.Xoa, truoc, null);
            return true;
        }

        public IEnumerable<ThongTinThiSinh> TimKiemNangCao(
            string tuKhoaHoTen = null,
            string khuVuc = null,
            string gioiTinh = null,
            Dictionary<string, double> diemToiThieu = null,
            double? tongDiemToiThieu = null)
        {
            IEnumerable<ThongTinThiSinh> query = danhSachThiSinh;

            if (!string.IsNullOrWhiteSpace(tuKhoaHoTen))
            {
                var keyword = tuKhoaHoTen.Trim().ToLowerInvariant();
                query = query.Where(ts => ts.HoTen?.ToLowerInvariant().Contains(keyword) == true);
            }

            if (!string.IsNullOrWhiteSpace(khuVuc))
            {
                var value = khuVuc.Trim().ToUpperInvariant();
                query = query.Where(ts => string.Equals(ts.KhuVuc?.ToUpperInvariant(), value, StringComparison.Ordinal));
            }

            if (!string.IsNullOrWhiteSpace(gioiTinh))
            {
                var value = gioiTinh.Trim().ToLowerInvariant();
                query = query.Where(ts => ts.GioiTinh?.ToLowerInvariant() == value);
            }

            if (diemToiThieu != null && diemToiThieu.Count > 0)
            {
                query = query.Where(ts =>
                {
                    var diemMon = LayTatCaDiemMon(ts);
                    foreach (var dieuKien in diemToiThieu)
                    {
                        if (!diemMon.TryGetValue(dieuKien.Key, out var diem) || diem < dieuKien.Value)
                        {
                            return false;
                        }
                    }

                    return true;
                });
            }

            if (tongDiemToiThieu.HasValue)
            {
                query = query.Where(ts => LayTongDiem(ts) >= tongDiemToiThieu.Value);
            }

            return query.ToList();
        }

        public IEnumerable<ThongTinThiSinh> SapXepTheo(TieuChiSapXep tieuChi, bool tangDan = true)
        {
            IEnumerable<ThongTinThiSinh> ketQua = tieuChi switch
            {
                TieuChiSapXep.TongDiem => tangDan
                    ? danhSachThiSinh.OrderBy(LayTongDiem).ThenBy(ts => ts.HoTen)
                    : danhSachThiSinh.OrderByDescending(LayTongDiem).ThenByDescending(ts => ts.HoTen),
                TieuChiSapXep.HoTen => tangDan
                    ? danhSachThiSinh.OrderBy(ts => ts.HoTen)
                    : danhSachThiSinh.OrderByDescending(ts => ts.HoTen),
                TieuChiSapXep.NgaySinh => tangDan
                    ? danhSachThiSinh.OrderBy(ts => ts.NgaySinh)
                    : danhSachThiSinh.OrderByDescending(ts => ts.NgaySinh),
                _ => danhSachThiSinh
            };

            return ketQua.ToList();
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
                thuKhoaA.In();
                Console.WriteLine($"Tổng điểm: {diemCaoNhatA:0.00}");
            }
            if (thuKhoaB == null) Console.WriteLine("Không có thủ khoa khối B");
            else
            {
                thuKhoaB.In();
                Console.WriteLine($"Tổng điểm: {diemCaoNhatB:0.00}");
            }
            if (thuKhoaC == null) Console.WriteLine("Không có thủ khoa khối C");
            else
            {
                thuKhoaC.In();
                Console.WriteLine($"Tổng điểm: {diemCaoNhatC:0.00}");
            }
        }

        public void ThongKeTheoKhoi()
        {
            int soKhoiA = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiA);
            int soKhoiB = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiB);
            int soKhoiC = danhSachThiSinh.Count(ts => ts is ThiSinhKhoiC);
            int tong = danhSachThiSinh.Count;

            Console.WriteLine($"Tổng số thí sinh: {tong}");
            if (tong == 0)
            {
                return;
            }

            Console.WriteLine($"Khối A: {soKhoiA} ({(double)soKhoiA / tong:P1})");
            Console.WriteLine($"Khối B: {soKhoiB} ({(double)soKhoiB / tong:P1})");
            Console.WriteLine($"Khối C: {soKhoiC} ({(double)soKhoiC / tong:P1})");
        }

        public ThongKeNangCaoResult ThongKeNangCao()
        {
            var result = new ThongKeNangCaoResult
            {
                TongSoThiSinh = danhSachThiSinh.Count
            };

            if (result.TongSoThiSinh == 0)
            {
                return result;
            }

            var nhomKhoi = danhSachThiSinh
                .GroupBy(ts => ts switch
                {
                    ThiSinhKhoiA => "Khối A",
                    ThiSinhKhoiB => "Khối B",
                    ThiSinhKhoiC => "Khối C",
                    _ => "Khác"
                });

            foreach (var group in nhomKhoi)
            {
                var diemTrungBinh = group
                    .Select(LayTongDiem)
                    .DefaultIfEmpty(0)
                    .Average();
                result.DiemTrungBinhTheoKhoi[group.Key] = Math.Round(diemTrungBinh, 2);
            }

            var thongKeTheoMon = new Dictionary<string, List<double>>();
            foreach (var ts in danhSachThiSinh)
            {
                foreach (var kv in LayTatCaDiemMon(ts))
                {
                    if (!thongKeTheoMon.TryGetValue(kv.Key, out var list))
                    {
                        list = new List<double>();
                        thongKeTheoMon[kv.Key] = list;
                    }
                    list.Add(kv.Value);
                }
            }

            foreach (var kv in thongKeTheoMon)
            {
                var danhSachDiem = kv.Value;
                if (danhSachDiem.Count == 0)
                {
                    continue;
                }

                var thongKe = new ThongKeMonHoc
                {
                    Mon = kv.Key,
                    SoLuong = danhSachDiem.Count,
                    DiemCaoNhat = Math.Round(danhSachDiem.Max(), 2),
                    DiemThapNhat = Math.Round(danhSachDiem.Min(), 2),
                    DiemTrungBinh = Math.Round(danhSachDiem.Average(), 2)
                };
                result.ThongKeTheoMon[kv.Key] = thongKe;
            }

            return result;
        }

        public bool Undo()
        {
            if (!CoTheUndo)
            {
                return false;
            }

            var thaoTac = undoStack.Pop();
            ThucHienUndo(thaoTac);
            redoStack.Push(thaoTac);
            return true;
        }

        public bool Redo()
        {
            if (!CoTheRedo)
            {
                return false;
            }

            var thaoTac = redoStack.Pop();
            ThucHienRedo(thaoTac);
            undoStack.Push(thaoTac);
            return true;
        }

        private void ThucHienUndo(LichSuThaoTac thaoTac)
        {
            switch (thaoTac.Loai)
            {
                case LoaiThaoTac.Them:
                    if (thaoTac.Sau != null)
                    {
                        XoaThiSinhNoTracking(thaoTac.Sau.SoBD);
                        GhiLog($"[UNDO] Hủy thêm thí sinh {thaoTac.Sau.SoBD}");
                    }
                    break;
                case LoaiThaoTac.CapNhat:
                    if (thaoTac.Truoc != null)
                    {
                        ThayTheThiSinhNoTracking(thaoTac.Sau?.SoBD ?? thaoTac.Truoc.SoBD, thaoTac.Truoc.SaoChep());
                        GhiLog($"[UNDO] Khôi phục dữ liệu thí sinh {thaoTac.Truoc.SoBD}");
                    }
                    break;
                case LoaiThaoTac.Xoa:
                    if (thaoTac.Truoc != null)
                    {
                        ThemThiSinhNoTracking(thaoTac.Truoc.SaoChep());
                        GhiLog($"[UNDO] Khôi phục thí sinh {thaoTac.Truoc.SoBD}");
                    }
                    break;
            }
        }

        private void ThucHienRedo(LichSuThaoTac thaoTac)
        {
            switch (thaoTac.Loai)
            {
                case LoaiThaoTac.Them:
                    if (thaoTac.Sau != null)
                    {
                        ThemThiSinhNoTracking(thaoTac.Sau.SaoChep());
                        GhiLog($"[REDO] Thêm lại thí sinh {thaoTac.Sau.SoBD}");
                    }
                    break;
                case LoaiThaoTac.CapNhat:
                    if (thaoTac.Sau != null)
                    {
                        ThayTheThiSinhNoTracking(thaoTac.Truoc?.SoBD ?? thaoTac.Sau.SoBD, thaoTac.Sau.SaoChep());
                        GhiLog($"[REDO] Áp dụng lại dữ liệu thí sinh {thaoTac.Sau.SoBD}");
                    }
                    break;
                case LoaiThaoTac.Xoa:
                    if (thaoTac.Truoc != null)
                    {
                        XoaThiSinhNoTracking(thaoTac.Truoc.SoBD);
                        GhiLog($"[REDO] Xóa lại thí sinh {thaoTac.Truoc.SoBD}");
                    }
                    break;
            }
        }

        private void ThemThiSinhNoTracking(ThongTinThiSinh ts)
        {
            danhSachThiSinh.Add(ts);
        }

        private void XoaThiSinhNoTracking(string soBD)
        {
            var index = danhSachThiSinh.FindIndex(ts =>
                string.Equals(ts.SoBD, soBD, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                danhSachThiSinh.RemoveAt(index);
            }
        }

        private void ThayTheThiSinhNoTracking(string soBD, ThongTinThiSinh thiSinhMoi)
        {
            var index = danhSachThiSinh.FindIndex(ts =>
                string.Equals(ts.SoBD, soBD, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                danhSachThiSinh[index] = thiSinhMoi;
            }
            else
            {
                var indexTheoSoMoi = danhSachThiSinh.FindIndex(ts =>
                    string.Equals(ts.SoBD, thiSinhMoi.SoBD, StringComparison.OrdinalIgnoreCase));
                if (indexTheoSoMoi >= 0)
                {
                    danhSachThiSinh[indexTheoSoMoi] = thiSinhMoi;
                }
                else
                {
                    danhSachThiSinh.Add(thiSinhMoi);
                }
            }
        }

        private void LuuThaoTac(LoaiThaoTac loai, ThongTinThiSinh truoc, ThongTinThiSinh sau)
        {
            undoStack.Push(new LichSuThaoTac(loai, truoc, sau));
            redoStack.Clear();
        }

        private void GhiLog(string message)
        {
            nhatKy.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {message}");
        }

        private static Dictionary<string, double> LayTatCaDiemMon(ThongTinThiSinh ts)
        {
            return ts switch
            {
                ThiSinhKhoiA khoiA => khoiA.LayTatCaDiemMon(),
                ThiSinhKhoiB khoiB => khoiB.LayTatCaDiemMon(),
                ThiSinhKhoiC khoiC => khoiC.LayTatCaDiemMon(),
                _ => new Dictionary<string, double>()
            };
        }

        private static double LayTongDiem(ThongTinThiSinh ts)
        {
            return ts is IThiKhoi thiKhoi ? thiKhoi.TongDiem() : 0;
        }
    }
}
