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
    /// Interaction logic for frmSanPhamHetHang.xaml
    /// </summary>
    public partial class frmSanPhamHetHang : Window
    {
        DataTable dssp;
        public frmSanPhamHetHang(DataTable dataTable)
        {
            this.dssp = dataTable;
            InitializeComponent();
        }

        private void dgvSPHH_Loaded(object sender, RoutedEventArgs e)
        {
            int soluong = 5;
            string chuoi = string.Format("Select * from SanPham Where SoLuong <= {0}", soluong);
            DataTable dssp = LT_BANG.Doc(chuoi);
            dgvDSSanPhamHetHang.ItemsSource = dssp.DefaultView;
        }
    }
}
