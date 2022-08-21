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
using System.Data.SqlClient;

namespace E_Okul_Proje
{
    public partial class FrmKulup : Form
    {
        public FrmKulup()
        {
            InitializeComponent();
        }

        public void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLKULUPLER", baglanti);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-OH7D98U9\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKulupid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKulupAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLKULUPLER (KULUPAD) values (@p1)", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtKulupAd.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kulup eklendi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update TBLKULUPLER Set KULUPAD=@P1 WHERE KULUPID=@P2", baglanti);
            cmd.Parameters.AddWithValue("@P1", TxtKulupAd.Text);
            cmd.Parameters.AddWithValue("@P2", TxtKulupid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulup güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Delete From TBLKULUPLER WHERE KULUPID=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtKulupid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulup silindi.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
