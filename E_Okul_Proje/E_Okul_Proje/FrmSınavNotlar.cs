using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Okul_Proje
{
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }

        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();
        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(Txtid.Text));
        }

        SqlConnection baglantı = new SqlConnection(@"Data Source=LAPTOP-OH7D98U9\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");

        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLDERSLER", baglantı);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            CmbDers.DisplayMember = "DERSAD";
            CmbDers.ValueMember = "DERSID";
            CmbDers.DataSource = dt;
            baglantı.Close();
        }

        int notid;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            notid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            Txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSınav_1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSınav_2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSınav_3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //int sinav1, sinav2, sinav3, proje;
        //double ortalama;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            int sinav1, sinav2, sinav3, proje;
            double ortalama;
            //string durum;

            sinav1 = int.Parse(TxtSınav_1.Text);
            sinav2 = int.Parse(TxtSınav_2.Text);
            sinav3 = int.Parse(TxtSınav_3.Text);
            proje = int.Parse(TxtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();

            if(ortalama >= 50)
            {
                TxtDurum.Text ="true";
            }
            else
            {
                TxtDurum.Text = "False";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(CmbDers.SelectedValue.ToString()), int.Parse(Txtid.Text), 
                byte.Parse(TxtSınav_1.ToString()), byte.Parse(TxtSınav_2.ToString()), 
                byte.Parse(TxtSınav_3.ToString()), byte.Parse(TxtProje.ToString()),
                decimal.Parse(TxtOrtalama.ToString()), bool.Parse(TxtDurum.Text), notid);
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {

        }
    }
}
