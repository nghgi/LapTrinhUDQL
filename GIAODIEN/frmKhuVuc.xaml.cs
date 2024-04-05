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
    /// Interaction logic for frmKhuVuc.xaml
    /// </summary>
    public partial class frmKhuVuc : UserControl
    {
        public string Title { get; set; }
        DataTable dskv;
        DataTable dskvtk;

        public frmKhuVuc()
        {
            InitializeComponent();
        }

        private void lvDSKhuVuc_Loaded(object sender, RoutedEventArgs e)
        {
            dskv = LT_BANG.Doc("Select * from KhuVuc");

            // Clear existing items
            lvDSKhuVuc.ItemsSource = dskv.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void lvDSKhuVuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*            if (lvDSKhuVuc.SelectedItems.Count > 0)
                        {
                            DataRowView selectedRow = (DataRowView)lvDSKhuVuc.SelectedItems[0];
                            int makv = (int)selectedRow.Row.ItemArray[0];

                            string query = string.Format("Select * from sanpham where MakhuVuc = {0}", makv);

                            DataTable dssp = LT_BANG.Doc(query);

                            lbDSSanPham.Items.Clear();
                            foreach (DataRow sp in dssp.Rows)
                            {
                                lbDSSanPham.Items.Add(sp["TenSP"]);
                            }
                        }*/

            if (lvDSKhuVuc.SelectedItems.Count > 0)
            {
                // Lấy chỉ mục của mục đầu tiên được chọn
                int vitri = lvDSKhuVuc.SelectedIndex;

                int makv = 0;

                if (dskvtk == null)
                {
                    makv = Convert.ToInt32(dskv.Rows[vitri][0]);
                }
                else
                {
                    makv = Convert.ToInt32(dskvtk.Rows[vitri][0]);
                }

                string cautruyvan = string.Format("Select * from sanpham where MakhuVuc = {0}", makv);

                DataTable dssp = LT_BANG.Doc(cautruyvan);

                lbDSSanPham.Items.Clear();
                for (int i = 0; i < dssp.Rows.Count; i++)
                {
                    DataRow sp = dssp.Rows[i];
                    lbDSSanPham.Items.Add(sp["TenSP"]);
                }
            }
        }

        private void btTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string chuoi = tbTimKiem.Text;
            if (dskvtk != null)
            {
                dskvtk.Dispose();
            }

            dskvtk = dskv.Clone();

            lvDSKhuVuc.ItemsSource = null;
            lvDSKhuVuc.Items.Clear();

            for (int i = 0; i < dskv.Rows.Count; i++)
            {
                DataRow kv = dskv.Rows[i];
                if (kv["Ten"].ToString().Contains(chuoi))
                {
                    dskvtk.ImportRow(kv);
                }
            }
            lvDSKhuVuc.ItemsSource = dskvtk.DefaultView;
        }

        private void bTatCa_Click(object sender, RoutedEventArgs e)
        {
            if (dskvtk != null)
            {
                dskvtk.Dispose();
            }

            dskvtk = null;

            lvDSKhuVuc.ItemsSource = dskv.DefaultView;
        }
    }
}
