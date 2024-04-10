using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        DataTable dssp = LT_BANG.Doc("Select * from SanPham, LoaiSanPham, KhuVuc where MaLoaiSanPham = LoaiSanPham.Ma and MaKhuVuc = KhuVuc.Ma");
        string ten;
        int loai;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DisplayKhuVuc(string title, UserControl content)
        {
            foreach (TabItem tabItemExist in mdiContainer.Items)
            {
                if (tabItemExist.Header.ToString() == title)
                {
                    // Nếu tồn tại, chọn TabItem đó và thoát phương thức
                    mdiContainer.SelectedItem = tabItemExist;
                    return;
                }
            }

            var child = new frmKhuVuc { Title = title };
            child.Content = content;

            //mdiContainer.Children.Add(child);
            var tabItem = new TabItem { Header = title };
            tabItem.Content = child;

            mdiContainer.Items.Add(tabItem);
            mdiContainer.SelectedItem = tabItem;
        }

        private void DisplaySanPham(string title, UserControl content)
        {
            foreach (TabItem tabItemExist in mdiContainer.Items)
            {
                if (tabItemExist.Header.ToString() == title)
                {
                    // Nếu tồn tại, chọn TabItem đó và thoát phương thức
                    mdiContainer.SelectedItem = tabItemExist;
                    return;
                }
            }

            var child = new frmSanPham(dssp) { Title = title };
            child.Content = content;

            //mdiContainer.Children.Add(child);
            var tabItem = new TabItem { Header = title };
            tabItem.Content = child;

            mdiContainer.Items.Add(tabItem);
            mdiContainer.SelectedItem = tabItem;
        }

        private void DisplayBanHang(string title, UserControl content)
        {
            foreach (TabItem tabItemExist in mdiContainer.Items)
            {
                if (tabItemExist.Header.ToString() == title)
                {
                    // Nếu tồn tại, chọn TabItem đó và thoát phương thức
                    mdiContainer.SelectedItem = tabItemExist;
                    return;
                }
            }

            var child = new frmBanHang(dssp) { Title = title };
            child.Content = content;

            //mdiContainer.Children.Add(child);
            var tabItem = new TabItem { Header = title };
            tabItem.Content = child;

            mdiContainer.Items.Add(tabItem);
            mdiContainer.SelectedItem = tabItem;
        }

        private void KhuVuc_Click(object sender, RoutedEventArgs e)
        {
            DisplayKhuVuc("KhuVuc", new frmKhuVuc { Title = "KhuVuc" });
        }

        private void SanPham_Click(object sender, RoutedEventArgs e)
        {
            DisplaySanPham("SanPham", new frmSanPham(dssp) { Title = "SanPham" });
        }

        private void BanHang_Click(object sender, RoutedEventArgs e)
        {
            DisplayBanHang("BanHang", new frmBanHang(dssp) { Title = "BanHang" });
        }

        private void SPHH_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            // Thực hiện các thao tác để lấy dữ liệu vào DataTable (ví dụ: query từ CSDL)

            // Tạo instance của frmSanPhamHetHang và truyền DataTable vào
            frmSanPhamHetHang frm = new frmSanPhamHetHang(dataTable);

            // Hiển thị frmSanPhamHetHang
            frm.Show();
        }

        private void dtThang_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            frmDoanhThu frm = new frmDoanhThu(dataTable);
            frm.Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();

            loai = frm.loai;
            ten = frm.tendangnhap;


            if (loai != 0)
            {
                mDangNhap.IsEnabled = false;
                mDangXuat.IsEnabled = true;
            };
            if (loai == 1)
            {
                mKhuVuc.IsEnabled = true;
                mSanPham.IsEnabled = true;

                mBanHang.IsEnabled = true;
                mNhapHang.IsEnabled = true;

                mSPHH.IsEnabled = true;
                mdtThang.IsEnabled = true;
            };
            if (loai == 2)
            {
                mKhuVuc.IsEnabled = false;
                mSanPham.IsEnabled = false;

                mBanHang.IsEnabled = true;
                mNhapHang.IsEnabled = false;

                mSPHH.IsEnabled = false;
                mdtThang.IsEnabled = false;
            };

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            loai = 0;
            ten = "";

            mDangNhap.IsEnabled = true;
            mDangXuat.IsEnabled = false;

            mKhuVuc.IsEnabled = false;
            mSanPham.IsEnabled = false;

            mBanHang.IsEnabled = false;
            mNhapHang.IsEnabled = false;

            mSPHH.IsEnabled = false;
            mdtThang.IsEnabled = false;

            while (mdiContainer.Items.Count > 0)
            {
                mdiContainer.Items.RemoveAt(0);
            }
        }

        private void mdiContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
