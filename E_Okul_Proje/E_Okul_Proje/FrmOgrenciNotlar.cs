using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_Okul_Proje
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=LAPTOP-OH7D98U9\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");

        public string numara;
        public string numara1;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select DERSAD,SINAV1,SINAV2,SINAV3,PROJE,ORTALAMA,DURUM" +
                " FROM TBLNOTLAR INNER JOIN TBLDERSLER ON TBLNOTLAR.DERSID=TBLDERSLER.DERSID WHERE OGRID=@p1", baglantı);
            cmd.Parameters.AddWithValue("@p1", numara);
            //this.Text = numara.ToString();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            baglantı.Open();
            SqlCommand cmd2 = new SqlCommand("select OGRAD,OGRSOYAD from TBLOGRENCILER where OGRID= @p1", baglantı);
            cmd2.Parameters.AddWithValue("@p1", numara1);
            SqlDataReader dr = cmd2.ExecuteReader();
            while(dr.Read())
            {
                this.Text = dr[0] + " " + dr[1];
            }
            baglantı.Close();
        }
    }
}
