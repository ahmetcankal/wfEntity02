using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfEntity02
{//test
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NorthwindEntities db = new NorthwindEntities();
            dataGridView1.DataSource = db.Urunler.ToList();

            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";
            comboBox1.DataSource = db.Kategoriler.ToList();

            comboBox2.DisplayMember = "SirketAdi";
            comboBox2.ValueMember = "TedarikciID";
            comboBox2.DataSource = db.Tedarikciler.ToList();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            NorthwindEntities db = new NorthwindEntities();
            Urunler urun = new Urunler();
            urun.UrunAdi = txturunad.Text;
            urun.TedarikciID = Convert.ToInt32(comboBox2.SelectedValue);
            urun.KategoriID = Convert.ToInt32(comboBox1.SelectedValue);
            urun.BirimFiyati = numericUpDown1.Value;

            db.Urunler.Add(urun);
            db.SaveChanges();

            dataGridView1.DataSource = db.Urunler.ToList();
        }       

        private void Search_Click(object sender, EventArgs e)
        {
            NorthwindEntities db = new NorthwindEntities();
            if (txturunad.Text != null)
            {
                var sonuc = db.Urunler.Where(x => x.UrunAdi.Contains(txturunad.Text)).ToList();
                dataGridView1.DataSource = sonuc;

            }
            else
            {
                dataGridView1.DataSource = db.Urunler.ToList();
            }


        }

        private void Delete_Click(object sender, EventArgs e)
        {
            NorthwindEntities db = new NorthwindEntities();
            int vurunid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Urunler silinecek = db.Urunler.FirstOrDefault(x => x.UrunID == vurunid);

            db.Urunler.Remove(silinecek);
            db.SaveChanges();
            dataGridView1.DataSource = db.Urunler.ToList();


        }

        private void btnjoin_Click(object sender, EventArgs e)
        {
            NorthwindEntities db = new NorthwindEntities();
            var sonuc = from k in db.Kategoriler
                        join u in db.Urunler on k.KategoriID equals u.KategoriID
                        join t in db.Tedarikciler on u.TedarikciID equals t.TedarikciID
                        where k.KategoriAdi==comboBox1.Text
                        select new
                        {
                            k.KategoriAdi,
                            u.UrunID,
                            u.UrunAdi,
                            u.BirimFiyati,
                            t.SirketAdi
                        };
            dataGridView1.DataSource = sonuc.ToList();


        }
    }
}
