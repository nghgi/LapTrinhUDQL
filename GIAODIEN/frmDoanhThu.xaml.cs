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
    /// Interaction logic for frmDoanhThu.xaml
    /// </summary>
    public partial class frmDoanhThu : Window
    {
        DataTable dssp;
        public frmDoanhThu(DataTable dataTable)
        {
            this.dssp = dataTable;
            InitializeComponent();
        }

        private void doanhthu_Loaded(object sender, RoutedEventArgs e)
        {
            int nambd = 0;
            int namkt = 0;

            string chuoi = "Select Min(year(Ngay)) from HoaDon";
            DataTable a = LT_BANG.Doc(chuoi);

            if (a != null && a.Rows.Count > 0)
            {
                // Lấy giá trị từ ô đầu tiên của hàng đầu tiên của DataTable a
                object rawValueA = a.Rows[0][0];

                if (rawValueA != DBNull.Value)
                {
                    nambd = Convert.ToInt32(rawValueA);
                }
            }

            chuoi = "Select Max(year(Ngay)) from HoaDon";
            DataTable b = LT_BANG.Doc(chuoi);

            if (b != null && b.Rows.Count > 0)
            {
                // Lấy giá trị từ ô đầu tiên của hàng đầu tiên của DataTable b
                object rawValueB = b.Rows[0][0];

                if (rawValueB != DBNull.Value)
                {
                    namkt = Convert.ToInt32(rawValueB);
                }
            }

            for (int i = nambd; i <= namkt; i++)
            {
                cbNam.Items.Add(i);
            }
        }

        /*        private void Button_Click(object sender, RoutedEventArgs e)
                {
                    if (cbNam.SelectedItem != null && cbThang.SelectedItem != null)
                    {
                        int nam = (int)cbNam.SelectedItem;
                        int thang = (int)cbThang.SelectedItem;

                        // Tạo ngày bắt đầu (ngaybd)
                        DateTime ngaybd = new DateTime(nam, thang, 1);

                        // Tạo ngày kết thúc (ngaykt)
                        DateTime ngaykt = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));

                        // Chuỗi truy vấn SQL
                        string chuoi = string.Format("Select * from HoaDon Where Ngay between '{0}' and '{1}'", ngaybd.ToString("yyyy-MM-dd"), ngaykt.ToString("yyyy-MM-dd"));

                        // Thực hiện truy vấn và gán dữ liệu vào DataGridView
                        DataTable dshd = LT_BANG.Doc(chuoi);
                        dgvDSHoaDon.ItemsSource = dshd.DefaultView;
                    }
                }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int nam = (int)cbNam.SelectedItem;

            ComboBoxItem selectedThangItem = (ComboBoxItem)cbThang.SelectedItem;
            int thang = Convert.ToInt32(selectedThangItem.Content);

            // Tạo ngày bắt đầu (ngaybd)
            DateTime ngaybd = new DateTime(nam, thang, 1);

            // Tạo ngày kết thúc (ngaykt)
            DateTime ngaykt = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));

            // Chuỗi truy vấn SQL
            string chuoi = string.Format("Select * from HoaDon Where Ngay between '{0}' and '{1}'", ngaybd.ToString("yyyy-MM-dd"), ngaykt.ToString("yyyy-MM-dd"));

            // Thực hiện truy vấn và gán dữ liệu vào DataGridView
            DataTable dshd = LT_BANG.Doc(chuoi);
            dgvDSHoaDon.ItemsSource = dshd.DefaultView;

            int tongtien = 0;

            foreach (DataRow hd in dshd.Rows)
            {
                tongtien += Convert.ToInt32(hd["TongTien"]);
            }

            lbTongTien.Content = "Tổng tiền: " + tongtien.ToString();
        }


    }
}
