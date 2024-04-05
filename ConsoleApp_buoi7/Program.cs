using ConsoleApp_buoi7;
using System.Data;

DataTable dskv = LT_BANG.Doc("Select * from KhuVuc");

DataTable dssp = LT_BANG.Doc("Select * from SanPham");

foreach (DataRow sp in dskv.Rows)
{
    Console.WriteLine("{0}-{1}-{2}", sp[0], sp["Ten"], sp["GhiChu"]);
}

DataRow spm = dssp.NewRow(); // spm có 6 cột
spm["MaSP"] = "SP004";
spm["TenSP"] = "Thu nghiem";
dssp.Rows.Add(spm);

DataRow kvm = dskv.NewRow(); // kvm có 3 cột

LT_BANG.Ghi(dssp, "SanPham");