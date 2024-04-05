using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        DataTable dssp;
        DataTable dsspm;
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
        }
    }
}
