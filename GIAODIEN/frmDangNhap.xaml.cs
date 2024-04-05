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
using System.Windows.Shapes;

namespace GIAODIEN
{
    /// <summary>
    /// Interaction logic for frmDangNhap.xaml
    /// </summary>
    public partial class frmDangNhap : Window
    {

        public string tendangnhap = "";
        public int loai = 0;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void bThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void bDangNhap_Click(object sender, RoutedEventArgs e)
        {
            string chuoi = string.Format("Select * from NguoiDung where TEN = '{0}' and MATKHAU = '{1}'",
                tbTenDangNhap.Text, tbMatKhau.Password);
            DataTable dsnd = LT_BANG.Doc(chuoi);
            if (dsnd.Rows.Count > 0 ) {
                tendangnhap = dsnd.Rows[0]["TEN"].ToString();
                loai = Convert.ToInt32(dsnd.Rows[0]["LOAI"]);
                Close(); 
            }
            else { MessageBox.Show("Ten dang nhap va mat khau khong chinh xac!"); }
        }
    }
}
