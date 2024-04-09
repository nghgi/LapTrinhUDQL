using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIAODIEN
{
    /// <summary>
    /// Interaction logic for frmBanHang.xaml
    /// </summary>
    public partial class frmBanHang : UserControl
    {
        public string Title { get; set; }
        string tennv;
        DataTable dssp;
        DataTable dsspm;
        decimal tongtien;

        public frmBanHang(DataTable dataTable, string tennv)
        {
            this.dssp = dataTable; 
            this.tennv = tennv;
            InitializeComponent();
        }

        public frmBanHang(DataTable dataTable)
        {
            this.dssp = dataTable;
            InitializeComponent();
        }

        private void frmBanHang_Loaded(object sender, RoutedEventArgs e)
        {
            //dssp = LT_BANG.Doc("Select * from SanPham, LoaiSanPham, KhuVuc where MaLoaiSanPham = LoaiSanPham.Ma and MaKhuVuc = KhuVuc.Ma");

            // Gán DataTable làm ItemsSource cho DataGrid
            dgDSSanPham.ItemsSource = dssp.DefaultView;

            // Ẩn các cột không cần thiết
            dgDSSanPham.Columns[0].Visibility = Visibility.Collapsed;
            dgDSSanPham.Columns[2].Visibility = Visibility.Collapsed;
            dgDSSanPham.Columns[5].Visibility = Visibility.Collapsed;
            dgDSSanPham.Columns[6].Visibility = Visibility.Collapsed;
            dgDSSanPham.Columns[8].Visibility = Visibility.Collapsed;

            // Đặt tiêu đề cho các cột
            dgDSSanPham.Columns[0].Header = "Mã";
            dgDSSanPham.Columns[1].Header = "Tên";
            dgDSSanPham.Columns[3].Header = "Số lượng";
            dgDSSanPham.Columns[4].Header = "Đơn giá";
            dgDSSanPham.Columns[7].Header = "Loại";
            
            dsspm = new DataTable();
            dsspm.Columns.Add("Ma", typeof(int));
            dsspm.Columns.Add("Ten");
            dsspm.Columns.Add("SoLuong", typeof(int));
            dsspm.Columns.Add("DonGia", typeof(decimal));
            dsspm.Columns.Add("ThanhTien", typeof(decimal));
            dgDSSanPhamMua.ItemsSource = dsspm.DefaultView;
            dgDSSanPhamMua.Columns[0].Visibility = Visibility.Collapsed;

            dgDSSanPhamMua.Columns[1].Header = "Tên";
            dgDSSanPhamMua.Columns[2].Header = "Số lượng";
            dgDSSanPhamMua.Columns[3].Header = "Đơn giá";
            dgDSSanPhamMua.Columns[4].Header = "Thành tiền";

            ngay.SelectedDate = DateTime.Now;

        }


        private int ConvertStrToNum(string value)
        {
            int n;
            bool isNumeric = int.TryParse(value, out n);
            if (isNumeric)
            {
                return n;
            }
            return -1;
        }

        private int tkMasp(string masp, DataTable ds)
        {
            foreach (DataRow item in ds.Rows)
            {
                if (string.Equals(item["Ma"].ToString(), masp))
                    return ds.Rows.IndexOf(item);
            }
            return -1;
        }

        private void tinhTongTien()
        {
            tongtien = 0;
            foreach (DataRow item in dsspm.Rows)
            {
                tongtien += decimal.Parse(item["ThanhTien"].ToString());
            }
            tbTongTien.Text = tongtien.ToString("#.000");
        }

        private void increaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dgDSSanPham.SelectedCells.Count > 0)
            {
                int vitri = dgDSSanPham.SelectedIndex;
                DataRow sp = dssp.Rows[vitri];

                if (soLuongBox.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng");
                    return; 
                }
                
                int soLuongMua = ConvertStrToNum(soLuongBox.Text);
                if(soLuongMua <= 0)
                {
                    MessageBox.Show("Số lượng phải nằm trong đoạn [1; 50]!");
                    return ;
                }

                int soLuongTon = int.Parse(sp["SoLuong"].ToString());

                if (soLuongMua <= soLuongTon )
                {
                    DataRow spm = null;
                    string masp = sp["Ma"].ToString();
                    int idx = tkMasp(masp, dsspm);
                    if (idx == -1)
                    {
                        spm = dsspm.NewRow();

                        spm["Ma"] = sp["Ma"];
                        spm["Ten"] = sp["Ten"];
                        spm["SoLuong"] = soLuongMua;
                        spm["DonGia"] = sp["DonGia"];
                        spm["ThanhTien"] = soLuongMua * decimal.Parse(sp["DonGia"].ToString());
                        dsspm.Rows.Add(spm);

                        // Giảm số lượng trong danh sách cũ
                        sp["SoLuong"] = soLuongTon - soLuongMua;
                    }
                    else
                    {
                        spm = dsspm.Rows[idx];
                        int soLuongCu = int.Parse(spm["SoLuong"].ToString());
                        // Giảm số lượng trong danh sách cũ
                        sp["SoLuong"] = soLuongTon - soLuongMua;
                        // Cập nhập số lượng mua hiện tại
                        soLuongMua += soLuongCu;
                        spm["SoLuong"] = soLuongMua;
                        spm["ThanhTien"] = soLuongMua * decimal.Parse(sp["DonGia"].ToString());
                    }

                    // Tính tổng tiền
                    tinhTongTien();
                }    
                else
                {
                    MessageBox.Show("Số lượng tồn không đủ");
                }    
            }
            else
            {
                MessageBox.Show("Chưa chọn sản phẩm");
            }
        }
        private void decrease_Click(object sender, RoutedEventArgs e)
        {
            if (dgDSSanPhamMua.SelectedCells.Count > 0)
            {
                int vitri = dgDSSanPhamMua.SelectedIndex;
                DataRow spm = dsspm.Rows[vitri];

                string masp = spm["Ma"].ToString();

                // Tìm sản phẩm trả trong kho
                int idx = tkMasp(masp, dssp);
                DataRow sp = dssp.Rows[idx];

                int soLuongTra = ConvertStrToNum(soLuongBox.Text);
                int soLuongMua = int.Parse(spm["SoLuong"].ToString());

                if (soLuongTra <= 0)
                {
                    MessageBox.Show("Số lượng phải nằm trong đoạn [1; 50]!");
                    return;
                }

                if (soLuongTra >= soLuongMua)
                {
                    int soLuongTon = int.Parse(sp["SoLuong"].ToString());
                    sp["SoLuong"] = soLuongTon + soLuongMua;
                    dsspm.Rows.RemoveAt(vitri);
                }
                else
                {
                    int soLuongTon = int.Parse(sp["SoLuong"].ToString());
                    sp["SoLuong"] = soLuongTon + soLuongTra;
                    spm["SoLuong"] = soLuongMua - soLuongTra;
                    spm["ThanhTien"] = (soLuongMua - soLuongTra) * decimal.Parse(sp["DonGia"].ToString());
                }
                //Tinh lai tong tien
                tinhTongTien();
            }
            else
            {
                MessageBox.Show("Chưa chọn sản phẩm");
            }
        }

        private void tbTienKhach_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal tienkhach;
            if (decimal.TryParse(tbTienKhach.Text, out tienkhach))
            {
                if (tienkhach >= tongtien)
                    tbTienThoi.Text = (tienkhach - tongtien).ToString();
                else tbTienThoi.Text = "0";
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số!");
            }
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            LT_BANG.Ghi(dssp, "SanPham");

            DataTable dshd = LT_BANG.Doc_cau_truc("HoaDon");
            DataRow hd = dshd.NewRow();

            hd["TenKH"] = tbTenKhachHang.Text;
            hd["Ngay"] = ngay.Text;
            hd["TongTien"] = tongtien;
            hd["NhanVien"] = tennv;

            dshd.Rows.Add(hd);
            LT_BANG.Ghi(dshd, "HoaDon");

            int mahd = int.Parse(hd["Ma"].ToString());
            DataTable dshdct = LT_BANG.Doc_cau_truc("HoaDonCT");

            foreach(DataRow item in dsspm.Rows) 
            {
                DataRow hdct = dshdct.NewRow();
                hdct["MaHoaDon"] = mahd;
                hdct["SanPham"] = item["Ten"];
                hdct["SoLuong"] = item["SoLuong"];
                hdct["DonGia"] = item["DonGia"];
                dshdct.Rows.Add(hdct);
            }

            LT_BANG.Ghi(dshdct, "HoaDonCT");

            dsspm.Rows.Clear();
        }
    }
}
