using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GIAODIEN
{
    public class LT_BANG
    {
        //public static string Chuoi_ket_noi = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB\\QLNS.mdb";
        public static string Chuoi_ket_noi = "Data Source=Elod;Initial Catalog=QLBH;Integrated Security=True;";

        public static DataTable Doc(string Chuoi_lenh)
        {
            DataTable Kq = new DataTable();
            using (SqlConnection connection = new SqlConnection(Chuoi_ket_noi))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Chuoi_lenh, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.FillSchema(Kq, SchemaType.Source);
                adapter.Fill(Kq);
            }
            return Kq;
        }

        public static DataTable Doc_cau_truc(string Ten_bang)
        {
            DataTable Kq = new DataTable();
            string Chuoi_lenh = "SELECT * FROM " + Ten_bang;
            using (SqlConnection connection = new SqlConnection(Chuoi_ket_noi))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Chuoi_lenh, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.FillSchema(Kq, SchemaType.Source);
            }
            return Kq;
        }

        public static int Ghi(DataTable Bang, string Ten_bang)
        {
            int Kq = 0;
            string Chuoi_lenh = "SELECT * FROM " + Ten_bang;
            using (SqlConnection connection = new SqlConnection(Chuoi_ket_noi))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(Chuoi_lenh, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                Kq = adapter.Update(Bang);
            }
            return Kq;
        }

    }
}
