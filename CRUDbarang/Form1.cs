using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDbarang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txt_cari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13)
            {
                if(string.IsNullOrEmpty(txt_cari.Text))
                {
                    dataGridView.DataSource = tblbarangBindingSource;
                }
                else
                {
                    var query = from o in this.dataBarang.tbl_barang
                                where o.NamaBarang.Contains(txt_cari.Text) || o.Jumlah.Contains(txt_cari.Text) || o.Harga.Contains(txt_cari.Text) || o.TanggalMasuk.Contains(txt_cari.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Apakah kamu ingin menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    tblbarangBindingSource.RemoveCurrent();
            }
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            try
            {
                tblbarangBindingSource.EndEdit();
                tbl_barangTableAdapter.Update(this.dataBarang.tbl_barang);
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblbarangBindingSource.ResetBindings(false);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txt_namabrg.Focus();
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah kamu ingin menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                tblbarangBindingSource.RemoveCurrent();
        }

        private void btn_baru_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                txt_namabrg.Focus();
                this.dataBarang.tbl_barang.Addtbl_barangRow(this.dataBarang.tbl_barang.Newtbl_barangRow());
                tblbarangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblbarangBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataBarang.tbl_barang' table. You can move, or remove it, as needed.
            this.tbl_barangTableAdapter.Fill(this.dataBarang.tbl_barang);
            tblbarangBindingSource.DataSource = this.dataBarang.tbl_barang;
        }
    }
}
