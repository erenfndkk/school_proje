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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection(@"Data Source=LAPTOP-OH7D98U9\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.ÖğrenciListesi();
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLKULUPLER", baglantı);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            CmbKulup.DisplayMember = "KULUPAD";
            CmbKulup.ValueMember = "KULUPID";
            CmbKulup.DataSource = dt;
            baglantı.Close();
        }

        public string c = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked==true)
            {
                c = "KADIN";
            }
            if(radioButton2.Checked==true)
            {
                c = "ERKEK";
            }
            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulup.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.ÖğrenciListesi();
        }

        private void CmbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Txtid.Text = CmbKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSilme(Convert.ToUInt16(Txtid.Text));
            MessageBox.Show("Öğrenci Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulup.SelectedValue.ToString()), c, int.Parse(Txtid.Text));
            MessageBox.Show("Öğrenci güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "KADIN")
            {
                radioButton1.Checked = true;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() =="ERKEK")
            {
                radioButton2.Checked = true;
            }
             CmbKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c = "KADIN";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton2.Checked == true)
            {
                c = "ERKEK";
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource = ds.OgrenciArama(TxtAra.Text);
        }
    }
}
