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
    }
}
